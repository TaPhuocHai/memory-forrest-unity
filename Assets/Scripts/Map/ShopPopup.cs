using UnityEngine;
using System.Collections;

public class ShopPopup : PHPopup 
{
	#region Singleton
	public static ShopPopup Instance { get; private set;}
	#endregion

	public PHPanel panel;

	void Awake () 
	{
		ShopPopup.Instance = this;		
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
