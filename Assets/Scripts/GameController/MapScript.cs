using UnityEngine;
using System;
using System.Collections;

public class MapScript : MonoBehaviour {

	void Awake () 
	{
		Card.Initialize ();

		// Clear reward data
		if (Constant.kClearMissionData) {
			foreach (RegionType regionType in (RegionType[]) Enum.GetValues(typeof(RegionType))) {			
				Region region = Region.Instance (regionType);
				region.ClearDataFinishMission ();
			}
		}
	}

	void Start () {}

	void Update () {}

	public void EnterMap4x4 () 
	{
		Player.currentRegionPlay = RegionType.KingdomOfRabbits;
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMap5x5 () 
	{	
		Player.currentRegionPlay = RegionType.Forest;
		PlayerPrefs.Save ();
		//Application.LoadLevel ("Mission");
	}

	public void EnterMap5x5_2 () 
	{
		Player.currentRegionPlay = RegionType.StoneMountain;
		PlayerPrefs.Save ();
		//Application.LoadLevel ("Mission");
	}

	public void EnterMap6x6 () 
	{
		Player.currentRegionPlay = RegionType.WolfCamp;
		PlayerPrefs.Save ();
		//Application.LoadLevel ("Mission");
	}
}
