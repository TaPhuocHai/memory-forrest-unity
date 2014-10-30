using UnityEngine;
using System;
using System.Collections;

public class MapScript : MonoBehaviour 
{
	PHButton kingdomOfRabbitsButton;
	PHButton forrestButton;
	PHButton stoneMountainButton;
	PHButton wolfCampButton;

	void Awake () 
	{
		Card.Initialize ();

		// Clear reward data
		if (Constant.kClearRewardData) {
			// Id auto of mission 
			PlayerPrefs.SetInt ("AUTO_MISSION_CODE", 0);
			PlayerPrefs.Save ();

			foreach (RegionType regionType in (RegionType[]) Enum.GetValues(typeof(RegionType))) {			
				Region region = Region.Instance (regionType);
				region.ClearRewardOfMission ();
			}
		}
	}

	void Start () 
	{
//		CardRandomCodeTest.RunTestMap1 ();
//		CardRandomCodeTest.RunTestMap2 ();
//		CardRandomCodeTest.RunTestMap3 ();
//		CardRandomCodeTest.RunTestMap4 ();
//		CardRandomCodeTest.ResertLockCardDefault ();
		if (kingdomOfRabbitsButton != null) {
			kingdomOfRabbitsButton.onClickHandle += EnterMapKingdomOfRabbits;
		}
		if (forrestButton != null) {
			forrestButton.onClickHandle += EnterMapForest;
		}
		if (stoneMountainButton != null) {
			stoneMountainButton.onClickHandle += EnterMapStoneMountain;
		}
		if (wolfCampButton != null) {
			wolfCampButton.onClickHandle += EnterMapWolfCamp;
		}
	}

	void Update () {}

	public void EnterMapKingdomOfRabbits () 
	{
		Debug.Log ("load KingdomOfRabbits");
		Player.currentRegionPlay = RegionType.KingdomOfRabbits;
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMapForest () 
	{	
		Region.UnlockMap (RegionType.Forest, true, true);

		Player.currentRegionPlay = RegionType.Forest;
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMapStoneMountain () 
	{
		Region.UnlockMap (RegionType.StoneMountain, true, true);

		Player.currentRegionPlay = RegionType.StoneMountain;
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMapWolfCamp () 
	{
		Region.UnlockMap (RegionType.WolfCamp, true, true);

		Player.currentRegionPlay = RegionType.WolfCamp;
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void ResetUnlockCard () 
	{
		CardRandomCodeTest.ResertLockCardDefault ();
	}
}
