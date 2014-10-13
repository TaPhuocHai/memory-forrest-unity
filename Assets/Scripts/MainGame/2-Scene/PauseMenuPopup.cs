using UnityEngine;
using System.Collections;

public class PauseMenuPopup : PHPopup 
{
	#region Singleton
	//private static PauseMenuPopup _instance;
	public static PauseMenuPopup Instance { get; private set;}
	#endregion

	public PHPanel    panel;

	void Awake () 
	{
		PauseMenuPopup.Instance = this;
	}

	// Use this for initialization
	void Start () 
	{
		this.Init ();
	}
	
	#region Animation

	override public void Hide (float second) 
	{
		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Top,second);
	}
	
	override public void Show (float second) 
	{
		base.Show (second);
		this.panel.Show (second);
	}

	#endregion
}
