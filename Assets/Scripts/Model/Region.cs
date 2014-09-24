using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum RegionType
{
	KingdomOfRabbits, // 4x4
	Forest,           // 5x5
	StoneMountain,    // 5x5 
	WolfCamp          // 6x6
}

public class Region 
{
	#region Static function

	/// <summary>
	/// Gets the cards.
	/// </summary>
	/// <returns>Danh sach : loai card : so luong</returns>
	/// <param name="region">Region.</param>
	/// <param name="round">Round : bat dau tu 0.</param>
	/// <param name="numberOfCol">Number of col.</param>
	/// <param name="numberOfRow">Number of row.</param>
	public static Dictionary<int, int> GetCards (RegionType region, int round, int numberOfCol, int numberOfRow) 
	{
		int numberOfObjectToDraw = numberOfCol * numberOfRow;

		Dictionary<int, int> numberCardToRandomWithTypeKey = new Dictionary<int, int> ();
		
		if (numberOfObjectToDraw <= 6) {
			numberCardToRandomWithTypeKey [(int)CardType.Stone] = 2;
			int value = UnityEngine.Random.Range(0,2);
			if (value == 0) {
				numberCardToRandomWithTypeKey[(int)CardType.Wolf]  = 2;
			}
		} else {
			numberCardToRandomWithTypeKey [(int)CardType.Stone] = 2;
			numberCardToRandomWithTypeKey[(int)CardType.Wolf]  = 2;
		}
		
		if (numberOfObjectToDraw >= 12) {
			numberCardToRandomWithTypeKey[(int)CardType.BlueButterfly] = 2;
			numberCardToRandomWithTypeKey[(int)CardType.RedButterfly]  = 2;
		}
		if (numberOfObjectToDraw >= 16) {
			numberCardToRandomWithTypeKey[(int)CardType.YellowButterfly] = 2;
			numberCardToRandomWithTypeKey[(int)CardType.VioletButterfly] = 2;
		}
		
		// Test only
		//numberCardToRandomWithTypeKey[(int)CardType.YellowButterfly] = 2;
		
		// Dem so luong tong so card special da random
		int totalCarDidRandom = 0;
		foreach (int key in numberCardToRandomWithTypeKey.Keys) {
			totalCarDidRandom += (int)numberCardToRandomWithTypeKey[key];
		}
		
		// So luong card con lai danh cho card thuong
		int numberOfNormalCard = numberOfObjectToDraw - totalCarDidRandom;
		// Random cac cap thuong
		for (int i = 0; i < numberOfNormalCard/2; i ++) {		
			// Random type
			CardType type = (CardType)UnityEngine.Random.Range (0, (int)CardType.Cherry + 1);
			
			// Lay so luong da random truoc danh cho type nay
			int numberCardDidRandomForType = 0;
			if (numberCardToRandomWithTypeKey.ContainsKey((int)type)) {
				numberCardDidRandomForType = numberCardToRandomWithTypeKey[(int)type];
			}
			numberCardDidRandomForType += 2;
			numberCardToRandomWithTypeKey[(int)type] = numberCardDidRandomForType;
		}

		return numberCardToRandomWithTypeKey;
	}

	/// <summary>
	/// Gets the missions of region.
	/// </summary>
	/// <returns>The missions.</returns>
	/// <param name="region">Region.</param>
	public static ArrayList GetMissions (RegionType region)
	{
		return null;
	}

	/// <summary>
	/// Unlocks the mission.
	/// </summary>
	/// <param name="code">Code.</param>
	static public void UnlockMission (int code) 
	{
		string unlockKey = "UNLOCK_MISSION_" + code.ToString ();
		PlayerPrefs.SetInt (unlockKey, 1);
		PlayerPrefs.Save ();
	}
	
	/// <summary>
	/// Determines if is unlock the specified code.
	/// </summary>
	/// <returns><c>true</c> if is unlock the specified code; otherwise, <c>false</c>.</returns>
	/// <param name="code">Code.</param>
	static public bool IsUnlockMission (int code) 
	{
		string unlockKey = "UNLOCK_MISSION_" + code.ToString ();
		int unlockValue = PlayerPrefs.GetInt (unlockKey, 0);
		if (unlockValue == 1) {
			return true;
		}
		return false;
	}
	
	#endregion Static function

	#region Initialize
	
	/// <summary>
	/// Initialize default value.
	/// </summary>
	public static void Initialize () 
	{
		/// ----------------------------------------------------------------------
		/// Chi Init 1 lan duy nhat khi User moi cai dat
		
		int didCardInitValue = PlayerPrefs.GetInt ("REGION_INITIALIZE", 0);
		if (didCardInitValue == 1) {
			return;
		}

		// Region KingdomOfRabbits
		List<Mission> listMissionOfKingdomOfRabbits = new List<Mission> {
			new Mission ()
		}
	
		PlayerPrefs.SetInt("REGION_INITIALIZE",1);
		PlayerPrefs.Save ();
		/// ----------------------------------------------------------------------
	}	
	#endregion Initialize
}
