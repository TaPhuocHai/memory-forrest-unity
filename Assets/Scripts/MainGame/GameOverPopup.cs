using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GameOverPopup : PHPopup 
{	
	#region Singleton
	public static GameOverPopup Instance { get; private set;}
	#endregion

	public PHPanel    panel;
	public PHButton   resetButton;

	public GameOverPopup ()
	{
		GameOverPopup.Instance = this;
	}

	void Start ()
	{
		this.Init ();
		
		if (this.resetButton != null) {
			this.resetButton.onClickHandle += HandleResetButtonClick;
		}
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
		// Show panel
		this.panel.Show (second);
	}
	
	#endregion

	void HandleResetButtonClick () 
	{
		this.Hide (0.5f);
		SceneScript.Instance.ResetRound ();
	}
}
