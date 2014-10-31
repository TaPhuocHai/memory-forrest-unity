using UnityEngine;
using System;
using System.Collections;

public class MapScript : MonoBehaviour 
{
	public PHButton kingdomOfRabbitsButton;
	public PHButton forrestButton;
	public PHButton stoneMountainButton;
	public PHButton wolfCampButton;

	public PHButton menuButton;

	void Awake () 
	{
		// Unlock card default cho moi region
		Card.Initialize ();

		// Xoa thong tin unlock map
		if (Constant.kDebugMode) {
			foreach (RegionType regionType in (RegionType[]) Enum.GetValues(typeof(RegionType))) {			
				for (int i = 0 ; i < 5 ; i ++) {
					Region.UnlockRound (regionType,i, false);
				}
			}
		}

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
		if (menuButton != null) {
			menuButton.onClickHandle += HandleMenuButtonClick;
		}
	}

	void Update () {}

	public void EnterMapKingdomOfRabbits () 
	{
		Debug.Log ("load KingdomOfRabbits");
		Player.currentRegionPlay = RegionType.KingdomOfRabbits;
		PlayerPrefs.Save ();
		MissionPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	public void EnterMapForest () 
	{	
		Region.UnlockMap (RegionType.Forest, true, true);

		Player.currentRegionPlay = RegionType.Forest;
		PlayerPrefs.Save ();
		MissionPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	public void EnterMapStoneMountain () 
	{
		Region.UnlockMap (RegionType.StoneMountain, true, true);

		Player.currentRegionPlay = RegionType.StoneMountain;
		PlayerPrefs.Save ();
		MissionPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	public void EnterMapWolfCamp () 
	{
		Region.UnlockMap (RegionType.WolfCamp, true, true);

		Player.currentRegionPlay = RegionType.WolfCamp;
		PlayerPrefs.Save ();
		MissionPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	public void ResetUnlockCard () 
	{
		//CardRandomCodeTest.ResertLockCardDefault ();
	}

	void HandleMenuButtonClick() 
	{
		Application.LoadLevel ("Menu");
	}
}
