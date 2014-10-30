using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionPopup : PHPopup 
{
	#region Singleton
	public static MissionPopup Instance { get; private set;}
	#endregion
	
	public PHPanel    panel;

	public PHButton   playButton;

	public MissionScript mission1;
	public MissionScript mission2;
	public MissionScript mission3;
	
	private List<MissionScript> _listMission;
	
	void Awake () 
	{
		MissionPopup.Instance = this;
	}
	
	void Start () 
	{
		this.Init ();
		
		_listMission = new List<MissionScript> ();
		if (mission1) _listMission.Add (mission1);
		if (mission2) _listMission.Add (mission2);
		if (mission3) _listMission.Add (mission3);	

		if (playButton != null) {
			playButton.onClickHandle += HandlePlayButtonClick;
		}
	}
	
	#region Animation
	
	override public void Hide (float second) 
	{
		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Left,second);
	}
	
	override public void Show (float second) 
	{
		// Set mission
		Region region = Region.Instance (Player.currentRegionPlay);
		for (int i = 0 ; i < region.currentMissions.Count; i ++) {
			Mission mission = region.currentMissions[i];
			if (_listMission.Count > i) {
				MissionScript missionObject = _listMission[i];
				missionObject.mission = mission;
			}
		}
		
		base.Show (second);
		// Show panel
		this.panel.Show (second);
	}
	
	#endregion

	void HandlePlayButtonClick ()
	{
		Application.LoadLevel ("Main");
	}
}
