using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

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
	private RegionType    _regionType;
	private List<Mission> _misisons;

	#region Properties

	public RegionType regionType { get {return _regionType;}}
	public int        numberOfCol 
	{
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
	public int        numberOfRow 
	{
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

	public List<Mission> missions 
	{
		get {
			if (_misisons == null) {
				string missionXmlPath = Region.MissionFilePath(this._regionType);
				_misisons = UnityXMLSerializer.DeserializeFromXMLFile<List<Mission>> (missionXmlPath);
			}
			return _misisons;
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

	#endregion

	#region Static Function

	/// <summary>
	/// Determines if is unlock round the specified regionType round.
	/// </summary>
	/// <returns><c>true</c> if is unlock round the specified regionType round; otherwise, <c>false</c>.</returns>
	/// <param name="regionType">Region type.</param>
	/// <param name="round">Round.</param>
	public static bool IsUnlockRound (RegionType regionType, int round) 
	{
		// Neu thoa dieu kien mat dinh duoc unlock cua moi vung dat
		if ((int)regionType - round >= 0) {
			return true;
		}

		string key = regionType.ToString () + round.ToString ();
		if (PlayerPrefs.GetInt (key, 0) == 1) {
			return true;
		}

		return false;
	}

	/// <summary>
	/// Unlocks the round.
	/// </summary>
	/// <returns><c>true</c>, if round was unlocked, <c>false</c> otherwise.</returns>
	/// <param name="regionType">Region type.</param>
	/// <param name="round">Round.</param>
	public static void UnlockRound (RegionType regionType, int round) 
	{
		string key = regionType.ToString () + round.ToString ();
		PlayerPrefs.SetInt (key, 1);
		PlayerPrefs.Save ();
	}

	#endregion

	#region Initialize
	
	/// <summary>
	/// Initialize default value.
	/// </summary>
	public static void Initialize () 
	{
		/// ----------------------------------------------------------------------
		/// Chi Init 1 lan duy nhat khi User moi cai dat
		
		int didCardInitValue = PlayerPrefs.GetInt ("REGION_INITIALIZE", 0);
		if (didCardInitValue == 1 && !Constant.kClearMissionData) {
			return;
		}

		// ----------------------------------------------------------------------
		// Region KingdomOfRabbits

		List<Mission> listMissionOfKingdomOfRabbits = new List<Mission> {
			new Mission ("Carrot Harvest", "Collect 10 carrot pairs", 
			             new CollectTask (CardType.Carrot,20), 
			             new MoreTimeReward (10)),
			new Mission ("Faster Hand Level 1","Collect all card before time run out",
			             new CollectAllCardTask (0), 
			             new UnlockExtraRoundReward (RegionType.KingdomOfRabbits, 1),true,1,1),
			new Mission ("Apple Juice","Collect 5 pairs of apple in 1 game",
			             new CollectTask (CardType.Apple,10, false),
			             new UnlockCardReward (CardType.WhiteRabbit)),
			new Mission ("Carrot Master","Collect 30 carrot pairs",
			             new CollectTask (CardType.Carrot,60),
			             new AdditionPointReward (CardType.Carrot,5)),
			new Mission ("Apple Master","Collect 50 apple pairs",
			             new CollectTask (CardType.Apple,100),
			             new AdditionPointReward (CardType.Apple,5)),
			new Mission ("Juice mix", "Collect 4 mushroom, 4 apple, 4 carrot in 1 game",
			             new CollectTask (new Dictionary<string,int> () {{CardType.Mushroom.ToString(),8},
																		 {CardType.Apple.ToString(),8},
																		 {CardType.Carrot.ToString(),8}}
							,false),
			             new CoinReward (25)),
			new Mission ("Colllect rabbit", "collect 5 whiterabbit pairs, 5 rabbit king pairs",
			             new CollectTask (new Dictionary<string,int> () {{CardType.WhiteRabbit.ToString(),10},
																		{CardType.RabbitKing.ToString(),10}}
										,true),
			             new CoinReward (25)),
			new Mission ("Big Score", "Score 500 points",
			             new ScoreTask (500,true),
			             new CoinReward (50),true,200,5)
		};
		// Save to file
		if (UnityXMLSerializer.SerializeToXMLFile<List<Mission>> (Region.MissionFilePath(RegionType.KingdomOfRabbits), listMissionOfKingdomOfRabbits, true)) {
			Debug.Log ("Init mission KingdomOfRabbits success");
		} else {
			Debug.Log ("Init mission KingdomOfRabbits faild");
		}

		PlayerPrefs.SetInt("REGION_INITIALIZE",1);
		PlayerPrefs.Save ();
		/// ----------------------------------------------------------------------
	}	

	public static string MissionFilePath (RegionType type)
	{
		return Application.persistentDataPath + "_" + type.ToString() + "_mission.xml";
	}
	#endregion
}

