﻿using UnityEngine;
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
		this.panel.Show (new Vector3(0,0,this.panel.gameObject.transform.position.z), second);
	}
	
	#endregion

	void HandleResetButtonClick () 
	{
		this.Hide (0.5f);
		SceneScript.Instance.ResetRound ();
	}

	/*
	public void EnterGameOver () 
	{
		TweenParms parms = new TweenParms().Prop("position", new Vector3(this.transform.position.x,0,0)).Ease(EaseType.EaseOutBack).OnComplete(CheckCurrentMission);
		HOTween.To (this.transform, 0.65f, parms);
	}

	public void ExitGameOver () 
	{
		TweenParms parms = new TweenParms().Prop("position", new Vector3(this.transform.position.x,2,0)).Ease(EaseType.EaseOutBack);
		HOTween.To (this.transform, 0.65f, parms);
	}

	private void CheckCurrentMission () 
	{
		// Get current region
		RegionType regionType = Player.currentRegionPlay;
		Region region = Region.Instance(regionType);
		region.UpdateCurrentMission ();

		// Test complete mission
		Mission temp1 = region.currentMissions [0];
		temp1.SetCompleteTask (true);
		Mission temp2 = region.currentMissions [1];
		temp2.SetCompleteTask (true);

		// Show complete mission 
		foreach (Mission mission in region.currentMissions) {
			if (mission.isTaskFinish) {
				Debug.Log ("mission : id " + mission.id.ToString() + " finish");
				// Show complete mission 
				Transform completeMission = this.transform.parent.FindChild("CompleteMission");
				if (completeMission != null) {
					CompleteMissionScript script = completeMission.GetComponent<CompleteMissionScript> ();
					script.mission = mission;
					script.EnterCompleteMission();
				} else {
					Debug.Log ("can't find complete mission");
				}
				break;
			}
		}
	}
	*/
}