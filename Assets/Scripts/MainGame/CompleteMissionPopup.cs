﻿using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class CompleteMissionPopup : PHPopup 
{
	#region Singleton
	public static CompleteMissionPopup Instance { get; private set;}
	#endregion

	private bool inCompleteMissionMode = false;

	private Mission _mission;
	public Mission mission {
		get { return _mission; } 
		set {
			_mission = value;
		}
	}

	public PHPanel    panel;
	public PHButton   claimButton;

	public Transform thumbnail;
	public TextMesh  descriptionText;
	public TextMesh  rewardText;

	void Awake () 
	{
		CompleteMissionPopup.Instance = this;
	}
	
	void Start () 
	{
		this.Init ();
		
		if (this.claimButton != null) {
			this.claimButton.onClickHandle += HandleClaimButtonClick;
		}
		if (this.panel != null) {
			this.panel.hideCompleteHandle += HandlePanelHideComplete;
		}
	}

	void Update () {}

	public void CheckAndShowCompleteMission () 
	{
		inCompleteMissionMode = true;

		// Get current region
		RegionType regionType = Player.currentRegionPlay;
		Region region = Region.Instance(regionType);
		region.UpdateCurrentMission ();

		// Test complete mission
		Mission temp1 = region.currentMissions [0];
		temp1.SetCompleteTask (true);
		Mission temp2 = region.currentMissions [1];
		temp2.SetCompleteTask (true);

		// Get complete mission
		Mission mission = this.GetCompleteMission ();
		if (mission != null) {
			this.mission = mission;
			this.Show (Constant.kPopupAnimationDuraction);
		} 
		// Show game over
		else {
			inCompleteMissionMode = false;
			GameOverPopup.Instance.Show (Constant.kPopupAnimationDuraction);
		}
	}

	#region Animation

	override public void Hide (float second) 
	{
		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Right,second);
	}
	
	override public void Show (float second) 
	{
		base.Show (second);
		// Show panel
		this.panel.Show (new Vector3(0,0,this.panel.gameObject.transform.position.z), second);
	}
	
	#endregion

	#region Event

	void HandleClaimButtonClick ()
	{
		// Get current region
		RegionType regionType = Player.currentRegionPlay;
		Region region = Region.Instance(regionType);
		region.GetRewardAndCompleteCurrentMission (this.mission.id);

		// Exit 
		this.Hide (Constant.kPopupAnimationDuraction);
		// Khi exit finish ham HandlePanelHideComplete duoc goi, tai day check complete mission tiep tuc
	}

	void HandlePanelHideComplete ()
	{
		this.panel.HideToDirection (PHPanelDirection.Left, 0);

		// Vi 1 level co the co nhieu mission duoc complete cung luc
		// Moi khi complete mission popup hide, tu dong check tiep xem con mission nao da complete nhung chua popup ?
		if (inCompleteMissionMode == true) {
			// Get complete mission
			Mission mission = this.GetCompleteMission ();
			if (mission != null) {
				this.mission = mission;
				this.Show (Constant.kPopupAnimationDuraction);
			} 
			// Show game over
			else {
				inCompleteMissionMode = false;
				GameOverPopup.Instance.Show (Constant.kPopupAnimationDuraction);
			}
		}
	}

	private Mission GetCompleteMission ()
	{
		// Get current region
		RegionType regionType = Player.currentRegionPlay;
		Region region = Region.Instance(regionType);
		
		// Check mission complete
		foreach (Mission mission in region.currentMissions) {
			if (mission.isTaskFinish) {
				return mission;
			}
		}
		return null;
	}

	#endregion 
}