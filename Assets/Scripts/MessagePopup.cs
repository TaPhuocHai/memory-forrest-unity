﻿using UnityEngine;
using System.Collections;

public class MessagePopup : PHPopup 
{
	public delegate void MessagePopupDelegate ();

	#region Singleton
	public static MessagePopup Instance { get; private set;}
	#endregion

	public PHPanel  panel;
	public TextMesh messageText;
	public TextMesh closeButtonText;

	public string message {
		set {
			if (this.messageText) {
				this.messageText.text = PHUtility.FormatStringMultiLine (value,50);
			}
		}
	}
	public string buttonTitle {
		set {
			if (this.closeButtonText) {
				this.closeButtonText.text = value;
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