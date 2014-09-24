using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum RegionType
{
	/// <summary>
	/// 1. 4x4
	/// </summary>
	KingdomOfRabbits,
	/// <summary>
	/// 2. 5x5
	/// </summary>
	Forest, 
	/// <summary>
	/// 3. 5x5 
	/// </summary>
	StoneMountain,
	/// <summary>
	/// 4. 6x5
	/// </summary>
	WolfCamp
}

public class Region 
{
	private RegionType _regionType;

	#region Properties

	public RegionType regionType { get {return _regionType;}}
	public int        numberOfCol {
		get {
			switch (_regionType) {
			case RegionType.KingdomOfRabbits:
				return 4;
			case RegionType.Forest:
				return 5;
			case RegionType.StoneMountain:
				return 5;
			case RegionType.WolfCamp:
				return 6;
			}
			return 0;
		}
	}
	public int        numberOfRow {
		get {
			switch (_regionType) {
			case RegionType.KingdomOfRabbits:
				return 4;
			case RegionType.Forest:
				return 5;
			case RegionType.StoneMountain:
				return 5;
			case RegionType.WolfCamp:
				return 5;
			}
			return 0;
		}
	}

	#endregion Properties

	public Region (RegionType regionType)
	{
		_regionType = regionType;
	}

	#region Function

	/// <summary>
	/// Gets the cards.
	/// </summary>
	/// <returns>Danh sach : loai card : so luong</returns>
	/// <param name="region">Region.</param>
	/// <param name="round">Round : bat dau tu 0.</param>
	/// <param name="numberOfCol">Number of col.</param>
	/// <param name="numberOfRow">Number of row.</param>
	public Dictionary<int, int> GetCards (int round) 
	{
		int numberOfObjectToDraw = this.numberOfCol * this.numberOfRow;

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
	public ArrayList GetMissions (RegionType region)
	{
		return null;
	}

	/// <summary>
	/// Unlocks the mission.
	/// </summary>
	/// <param name="code">Code.</param>
	public void UnlockMission (int code) 
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
	public bool IsUnlockMission (int code) 
	{
		string unlockKey = "UNLOCK_MISSION_" + code.ToString ();
		int unlockValue = PlayerPrefs.GetInt (unlockKey, 0);
		if (unlockValue == 1) {
			return true;
		}
		return false;
	}
	
	#endregion Function

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
			new Mission ("Carrot Harvest", "Collect 10 carrot pairs",MissionType.MoreTime, 10,10),
			new Mission ("Faster Hand Level 1","Collect all card before time run out", MissionType.UnlockExtraRound,0,0,true,1,1),
			new Mission ("Apple Juice","Collect 5 pairs of apple in 1 game",MissionType.UnlockCards, 5,1,(int)CardType.WhiteRabbit),
			new Mission ("Carrot Master","Collect 30 carrot pairs",MissionType.AdditionPoints,30,5,(int)CardType.Carrot),
			new Mission ("Apple Master","Collect 50 apple pairs",MissionType.AdditionPoints,50,5,(int)CardType.Apple),
			new Mission ("Juice Mix","Collect 4 mushroom, 4 apple, 4 carrot in 1 game",MissionType.AdditionPoints,50,5,(int)CardType.Apple),
		};
	
		PlayerPrefs.SetInt("REGION_INITIALIZE",1);
		PlayerPrefs.Save ();
		/// ----------------------------------------------------------------------
	}	
	#endregion Initialize
}

