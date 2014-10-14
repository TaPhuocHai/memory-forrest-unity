﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuPopup : PHPopup 
{
	#region Singleton
	public static PauseMenuPopup Instance { get; private set;}
	#endregion

	public PHPanel    panel;
	public PHButton   resetButton;

	public MissionInPauseGame mission1;
	public MissionInPauseGame mission2;
	public MissionInPauseGame mission3;

	private List<MissionInPauseGame> _listMissionInPauseGame;

	void Awake () 
	{
		PauseMenuPopup.Instance = this;
	}
	
	void Start () 
	{
		this.Init ();

		if (this.resetButton != null) {
			this.resetButton.onClickHandle += HandleResetButtonClick;
		}

		_listMissionInPauseGame = new List<MissionInPauseGame> ();
		if (mission1) _listMissionInPauseGame.Add (mission1);
		if (mission2) _listMissionInPauseGame.Add (mission2);
		if (mission3) _listMissionInPauseGame.Add (mission3);	
	}
	
	#region Animation

	override public void Hide (float second) 
	{
		TimerManager.Instance.Resume ();

		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Top,second);

		float secondeToHideMission = 0;
		if (second != 0) {
			secondeToHideMission = 0.2f;
		}
		if (mission1 != null) {
			mission1.Hide (secondeToHideMission);
		}
		if (mission2 != null) {
			mission2.Hide (secondeToHideMission);
		}
		if (mission3 != null) {
			mission3.Hide (secondeToHideMission);
		}
	}
	
	override public void Show (float second) 
	{
		TimerManager.Instance.Pause ();

		// Set mission
		Region region = Region.Instance (Player.currentRegionPlay);
		for (int i = 0 ; i < region.currentMissions.Count; i ++) {
			Mission mission = region.currentMissions[i];
			if (_listMissionInPauseGame.Count > i) {
				MissionInPauseGame missionObject = _listMissionInPauseGame[i];
				missionObject.mission = mission;
			}
		}

		base.Show (second);
		this.panel.Show (second);

		StartCoroutine(Wait(second));
	}

	#endregion

	private IEnumerator Wait (float second) 
	{
		yield return new WaitForSeconds(second);

		if (mission1 != null) {
			mission1.Show (0.2f);
		}
		if (mission2 != null) {
			mission2.Show (0.2f);
		}
		if (mission3 != null) {
			mission3.Show (0.2f);
		}
	}

	void HandleResetButtonClick () 
	{
		this.Hide (0.5f);
		SceneScript.Instance.ResetRound ();
	}
}
