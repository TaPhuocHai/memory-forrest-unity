using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuPopup : PHPopup 
{
	#region Singleton
	public static PauseMenuPopup Instance { get; private set;}
	#endregion

	public PHPanel    panel;
	public PHButton   menuButton;
	
	public PHPanel    soundButton;
	public PHPanel    musicButton;

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

		if (this.menuButton != null) {
			this.menuButton.onClickHandle += HandleMenuButtonClick;
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
		this.panel.HideToDirection (PHPanelDirection.Left,second);

		this.soundButton.HideToDirection (PHPanelDirection.Top, second);
		this.musicButton.HideToDirection (PHPanelDirection.Top, second);
		this.menuButton.transform.GetComponent<PHPanel> ().HideToDirection (PHPanelDirection.Top, second);
	}
	
	override public void Show (float second) 
	{
		// Pause timer
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
		// Show panel
		this.panel.Show (second);

		this.soundButton.Show (second);
		this.musicButton.Show (second);
		this.menuButton.transform.GetComponent<PHPanel> ().Show (second);
	}

	#endregion}

	void HandleMenuButtonClick () 
	{
		this.Hide (Constant.kPopupAnimationDuraction);

	}
}
