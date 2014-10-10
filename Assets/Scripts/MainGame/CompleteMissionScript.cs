using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class CompleteMissionScript : MonoBehaviour {
	
	private Mission _mission;
	public Mission mission {
		get { return _mission; } 
		set {
			_mission = value;
		}
	}

	// Use this for initialization
	void Start () {
		this.transform.position = new Vector3 (this.transform.position.x, 2,0);
	}
	
	// Update is called once per frame
	void Update () {}

	#region Animation

	public void EnterCompleteMission () 
	{
		TweenParms parms = new TweenParms().Prop("position", new Vector3(this.transform.position.x,0,0)).Ease(EaseType.EaseOutBack);
		HOTween.To (this.transform, 0.65f, parms);
	}
	
	private void ExitCompleteMission () 
	{
		TweenParms parms = new TweenParms().Prop("position", new Vector3(this.transform.position.x,2,0)).Ease(EaseType.EaseOutBack).OnComplete(CheckCurrentMissionComplete);
		HOTween.To (this.transform, 0.65f, parms);
	}

	private void CheckCurrentMissionComplete ()
	{
		// Get current region
		RegionType regionType = Player.currentRegionPlay;
		Region region = Region.Instance(regionType);

		// Check mission complete
		foreach (Mission mission in region.currentMissions) {
			if (mission.isTaskFinish) {
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

	#endregion

	#region Event

	public void GetRewardButtonDidTouch () 
	{
		// Get current region
		RegionType regionType = Player.currentRegionPlay;
		Region region = Region.Instance(regionType);
		region.GetRewardAndCompleteCurrentMission (this.mission.id);

		// Exit complete mission
		this.ExitCompleteMission ();
	}

	#endregion 
}
