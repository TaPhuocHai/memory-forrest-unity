using UnityEngine;
using System.Collections;

public class MessagePopup : PHPopup 
{
	#region Singleton
	public static MessagePopup Instance { get; private set;}
	#endregion

	public PHPanel  panel;
	public TextMesh messageText;

	private string _message;
	public string message {
		get { return _message; }
		set {
			_message = value;
			if (this.messageText) {
				this.messageText.text = PHUtility.FormatStringMultiLine (value,50);
			}
		}
	}

	void Awake () 
	{
		MessagePopup.Instance = this;		
		this.Init ();
	}
	
	void Update () {}

	#region Animation
	
	override public void Hide (float second) 
	{
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
}
