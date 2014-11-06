using UnityEngine;
using System;
using System.Collections;

public class MapScript : MonoBehaviour 
{
	public PHButton kingdomOfRabbitsButton;
	public PHButton forrestButton;
	public PHButton stoneMountainButton;
	public PHButton wolfCampButton;

	// Exit to menu button
	public PHButton menuButton;

	// Score
	public PHButton scoreButton;
	public TextMesh coinText;

	private RegionType regionTypeNeedUnlock;

	void Awake () 
	{
		// Unlock card default cho moi region
		Card.Initialize ();
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
		if (scoreButton != null) {
			scoreButton.onClickHandle += HandleScoreButtonClick;
		}
	}

	void Update () 
	{
		if (this.coinText) {
			this.coinText.text = Player.Coin.ToString ();
		}
	}

	public void EnterMapKingdomOfRabbits () 
	{
		Debug.Log ("load KingdomOfRabbits");
		Player.currentRegionPlay = RegionType.KingdomOfRabbits;
		PlayerPrefs.Save ();
		MissionPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	public void EnterMapForest () 
	{	
		// Chek is unlock region ?
		if (!Region.IsUnlockMap (RegionType.Forest)) {
			regionTypeNeedUnlock = RegionType.Forest;
				 
			MessagePopup.Instance.message = "You don't unlock Forest map. Do you wain to unlock ? You need 100 coin.";
			MessagePopup.Instance.buttonTitle = "Unlock";
			MessagePopup.Instance.onButtonClick += EnterUnlockRegion;
			MessagePopup.Instance.enableCloseButton = true;
			MessagePopup.Instance.Show (Constant.kPopupAnimationDuraction);
			return;
		}

		Player.currentRegionPlay = RegionType.Forest;
		MissionPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	public void EnterMapStoneMountain () 
	{
		if (!Region.IsUnlockMap (RegionType.StoneMountain)) {
			regionTypeNeedUnlock = RegionType.StoneMountain;
			
			MessagePopup.Instance.message = "You don't unlock StoneMountain map. Do you wain to unlock ? You need 200 coin.";
			MessagePopup.Instance.buttonTitle = "Unlock";
			MessagePopup.Instance.onButtonClick += EnterUnlockRegion;
			MessagePopup.Instance.enableCloseButton = true;
			MessagePopup.Instance.Show (Constant.kPopupAnimationDuraction);
			return;
		}

		Player.currentRegionPlay = RegionType.StoneMountain;
		MissionPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	public void EnterMapWolfCamp () 
	{
		if (!Region.IsUnlockMap (RegionType.WolfCamp)) {
			regionTypeNeedUnlock = RegionType.WolfCamp;
			
			MessagePopup.Instance.message = "You don't unlock WolfCamp map. Do you wain to unlock ? You need 300 coin.";
			MessagePopup.Instance.buttonTitle = "Unlock";
			MessagePopup.Instance.onButtonClick += EnterUnlockRegion;
			MessagePopup.Instance.enableCloseButton = true;
			MessagePopup.Instance.Show (Constant.kPopupAnimationDuraction);
			return;
		}

		Player.currentRegionPlay = RegionType.WolfCamp;
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

	void HandleScoreButtonClick ()
	{
		ShopPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	void EnterUnlockRegion ()
	{
		int coinToUnlock = 0;
		if (this.regionTypeNeedUnlock == RegionType.Forest) {
			coinToUnlock = 100;
		} else if (this.regionTypeNeedUnlock == RegionType.StoneMountain) {
			coinToUnlock = 200;
		} else if (this.regionTypeNeedUnlock == RegionType.WolfCamp) {
			coinToUnlock = 300;
		}

		if (Player.Coin < coinToUnlock) {
			// Show popup don't enought coin
			StartCoroutine(WatingAndShowNeedCoinPopup());
		} else {
			// Do unlock
			Region.UnlockMap (this.regionTypeNeedUnlock, true, true);
			// Tru coin
			Player.Coin = Player.Coin - coinToUnlock;

			StartCoroutine(WatingAndShowUnlockSuccessPopup());
		}
	}

	IEnumerator WatingAndShowNeedCoinPopup ()
	{
		yield return new WaitForSeconds (Constant.kPopupAnimationDuraction);

		MessagePopup.Instance.message = "You don't enought coin to unlock.";
		MessagePopup.Instance.buttonTitle = "Close";
		MessagePopup.Instance.enableCloseButton = false;
		MessagePopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	IEnumerator WatingAndShowUnlockSuccessPopup ()
	{
		yield return new WaitForSeconds (Constant.kPopupAnimationDuraction);
		
		MessagePopup.Instance.message = "You did unlock success. Enjoy it";
		MessagePopup.Instance.buttonTitle = "Close";
		MessagePopup.Instance.enableCloseButton = false;
		MessagePopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}
}
