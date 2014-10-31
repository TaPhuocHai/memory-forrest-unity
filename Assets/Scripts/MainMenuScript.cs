using UnityEngine;
using System.Collections;
using System;

public class MainMenuScript : MonoBehaviour 
{
	public PHButton playButton;
	public PHButton jungleBookButton;

	public PHSoundButton musicButton;
	public PHSoundButton soundEffectButton;

	public PHButton resetDataButton;

	void Start ()
	{
		if (playButton != null) {
			playButton.onClickHandle += HandlePlayButtonClick;
		}
		if (jungleBookButton != null) {
			jungleBookButton.onClickHandle += HandleJungleBookButtonClick;
		}

		musicButton.isOn = PHSetting.IsSoundBackgroud;
		soundEffectButton.isOn = PHSetting.IsSoundEffect;

		// Reset data button
		if (this.resetDataButton) {
			this.resetDataButton.onClickHandle += HandleResetDataButtonClick;
		}
	}

	void HandlePlayButtonClick ()
	{
		Application.LoadLevel ("Map");
	}

	void HandleJungleBookButtonClick ()
	{
		Application.LoadLevel ("Book");
	}

	void HandleResetDataButtonClick ()
	{
		Debug.Log ("Reset data player");

		Player.ResetData ();

		// Xoa  thong tin unlock round 
		foreach (RegionType regionType in (RegionType[]) Enum.GetValues(typeof(RegionType))) {			
			for (int i = 0 ; i < 5 ; i ++) {
				Region.UnlockRound (regionType,i, false);
			}
		}
		
		// Xoa thong tin unlock region
		Region.UnlockMap (RegionType.Forest,false,true);
		Region.UnlockMap (RegionType.StoneMountain,false,true);
		Region.UnlockMap (RegionType.WolfCamp,false,true);	

		// Id auto of mission 
		PlayerPrefs.SetInt ("AUTO_MISSION_CODE", 0);
		PlayerPrefs.Save ();

		/*
		// Clear reward data
		foreach (RegionType regionType in (RegionType[]) Enum.GetValues(typeof(RegionType))) {			
			Region region = Region.Instance (regionType);
			region.ClearRewardOfMission ();
		}
		*/

		// Xoa thong tin unlock card
		foreach (CardType cardType in (CardType[]) Enum.GetValues(typeof(CardType))) {			
			foreach (RegionType regionType in (RegionType[]) Enum.GetValues(typeof(RegionType))) {			
				Card.Unlock(cardType,regionType,false);
			}
		}

		Region.CleanMissionFiles ();
	}
}
