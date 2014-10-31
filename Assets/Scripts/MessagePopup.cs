using UnityEngine;
using System.Collections;

public class MessagePopup : PHPopup 
{
	public delegate void MessagePopupDelegate ();

	#region Singleton
	public static MessagePopup Instance { get; private set;}
	#endregion

	public PHPanel  panel;
	public PHButton button;

	public TextMesh messageText;
	public TextMesh buttonText;

	public string message {
		set {
			if (this.messageText) {
				this.messageText.text = PHUtility.FormatStringMultiLine (value,24);
			}
		}
	}
	public string buttonTitle {
		set {
			if (this.buttonText) {
				this.buttonText.text = value;
			}
		}
	}
	public MessagePopupDelegate onButtonClick;

	public bool enableCloseButton {
		set {
			this.closeButton.transform.renderer.enabled = value;
		}
	}

	void Awake () 
	{
		MessagePopup.Instance = this;		
		this.Init ();

		if (this.button) {
			this.button.onClickHandle += HandleButtonClick;
		}
	}
	
	void Update () {}

	#region Animation
	
	override public void Hide (float second) 
	{
		this.onButtonClick = null;

		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Top,second);
	}
	
	override public void Show (float second) 
	{
		base.Show (second);
		// Show panel
		this.panel.Show (second);
	}

	#endregion

	void HandleButtonClick ()
	{
		if (this.onButtonClick != null) {
			this.onButtonClick();
		}
		this.Hide (Constant.kPopupAnimationDuraction);
	}
}
