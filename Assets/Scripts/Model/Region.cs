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
	private List<Mission> _currentMissions;

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
				// Load du lieu tu file
				string missionXmlPath = Region.MissionFilePath(this._regionType);
				_misisons = UnityXMLSerializer.DeserializeFromXMLFile<List<Mission>> (missionXmlPath);
			}
			return _misisons;
		}
	}

	public List<Mission> currentMissions 
	{
		get {
			if (_currentMissions == null) {
				// Tao du lieu
				this.LoadOrUpdateCurrentMission ();
			}
			return _currentMissions;
		}
	}
	
	#endregion Properties

	#region Contructors

	/// <summary>
	/// Khong nen goi ham nay de tao 1 Region
	/// </summary>
	/// <param name="regionType">Region type.</param>
	public Region (RegionType regionType)
	{
		// Goi init mission data neu can thiet
		Region.InitMissionDataIfNeed();

		_regionType = regionType;
	}

	private static Dictionary<string, Region> _instances;
	/// <summary>
	/// Instance the specified regionType.
	/// </summary>
	/// <param name="regionType">Region type.</param>
	public static Region Instance (RegionType regionType) {
		if (_instances == null) {
			_instances = new Dictionary<string, Region> ();
		}
		if (_instances.ContainsKey (regionType.ToString ())) {
			return _instances[regionType.ToString()];
		}

		Region region = new Region (regionType);
		_instances [regionType.ToString ()] = region;
		return region;
	}

	#endregion

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
	/// Updates the current mission.
	/// </summary>
	public void UpdateCurrentMission () 
	{
		foreach (Mission misison in this.currentMissions) {
			misison.UpdateMission ();
		}
	}
	
	/// <summary>
	/// Gets the reward and complete current mission.
	/// </summary>
	/// <param name="missionId">Mission identifier.</param>
	public void GetRewardAndCompleteCurrentMission (int missionId) 
	{
		// Lay object mission can xu ly finish
		Mission missionNeedComplete = null;
		foreach (Mission mission in this.currentMissions) {
			if (mission.id == missionId) {
				missionNeedComplete = mission;
				break;
			}
		}

		if (missionNeedComplete == null) {
			Debug.Log ("Region : can't find misison to get reward");
			return;
		}

		// Nhan reward success
		if (missionNeedComplete.GetReward ()) {
			// Neu mission la dang tang dan -> tao 1 mission moi
			if (missionNeedComplete.isIncremental) {
				Mission newMission = missionNeedComplete.CreateIncrementalMission ();
				if (newMission != null) {
					// Them mission moi vao danh sach mission cua region
					this.missions.Add (newMission);
				}
			}

			// Remove complete mission
			this.currentMissions.Remove(missionNeedComplete);

			// Luu thong tin current mission
			List<int> currentMissionId = new List<int> ();
			foreach (Mission mission in this.currentMissions) {
				currentMissionId.Add (mission.id);
			}
			if (UnityXMLSerializer.SerializeToXMLFile<List<int>> (Region.CurrentMissionFilePath(this.regionType), currentMissionId, true)) {
				Debug.Log ("Region : Luu thong tin current mission thanh cong");
			} else {
				Debug.Log ("Region : Luu thong tin current mission that bai");
			}

			// Luu thong tin mission
			if (UnityXMLSerializer.SerializeToXMLFile<List<Mission>> (Region.MissionFilePath(this.regionType), this.missions, true)) {
				Debug.Log ("Region : Luu thong tin mission thanh cong");
			} else {
				Debug.Log ("Region : Luu thong tin mission that bai");
			}

			// Update danh sachfo current mission
			this.LoadOrUpdateCurrentMission ();
		} else {
			Debug.Log ("Region : mission id : " + missionId.ToString() + " get reward faild");
		}
	}

	#endregion

	#region Private functions

	private void LoadOrUpdateCurrentMission () 
	{
		// Doc du lieu tu file
		List<int> currentMissionId = UnityXMLSerializer.DeserializeFromXMLFile<List<int>> (Region.CurrentMissionFilePath(this.regionType));
		if (currentMissionId == null) {
			currentMissionId = new List<int>();
		}

		// Kiem tra xem can lay them bao nhieu mission
		int countNeedMore = 0;
		if (currentMissionId == null) {
			countNeedMore = Constant.kMaxCurrentMisison;
		}
		if (currentMissionId.Count < Constant.kMaxCurrentMisison) {
			countNeedMore += Constant.kMaxCurrentMisison - currentMissionId.Count; 
		}

		// Tao moi danh sach neu can thiet
		if (_currentMissions == null) {
			_currentMissions = new List<Mission>();
		}

		// Current Mission
		foreach (int id in currentMissionId) {
			foreach (Mission mission in this.missions) {
				if (mission.id == id) {
					_currentMissions.Add (mission);
				}
			}
		}

		// Lay them du lieu
		if (countNeedMore != 0) {
			foreach (Mission mission in this.missions) {
				// Neu mission chua duoc thuc hien hoac thu hien chua xong
				if (!mission.isFinish) {
					bool isInCurrentMisison = false;
					// Kiem tra xem mission nay da trong o trong danh current mission chua
					foreach (Mission cMission in this.currentMissions) {
						if (mission.id == cMission.id) {
							isInCurrentMisison = true;
							break;
						}
					}
					// Neu no chua co trong danh sach mission currrent
					if (!isInCurrentMisison) {
						// Them misison nay vao current mission
						currentMissionId.Add(mission.id);
						_currentMissions.Add(mission);
						if (currentMissionId.Count == Constant.kMaxCurrentMisison) {
							break;
						}
					}
				}
			}

			// Luu thong tin current mission 
			if (UnityXMLSerializer.SerializeToXMLFile<List<int>> (Region.CurrentMissionFilePath(this.regionType), currentMissionId, true)) {
				Debug.Log ("Region : Luu thong tin current mission thanh cong");
			} else {
				Debug.Log ("Region : Luu thong tin current mission that bai");
			}
		}
	}
	
	#endregion

	#region Static Functions

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

	#region Private static functions
	
	/// <summary>
	/// Initialize default value.
	/// </summary>
	private static void InitMissionDataIfNeed () 
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
			             "You have just add 10s to timer",
			             new CollectTask (CardType.Carrot,20), 
			             new MoreTimeReward (10)),
			new Mission (new MissionText ("Faster Hand Level @D",1), 
			             new MissionText ("Collect all card before time run out"),
			             new MissionText ("You have just Unlock Round @D",2),
			             new CollectAllCardTask (0), 
			             new UnlockExtraRoundReward (RegionType.KingdomOfRabbits, 1),true,1,1),
			new Mission ("Apple Juice","Collect 5 pairs of apple in 1 game",
			             "You have just Unlock White Rabbit",
			             new CollectTask (CardType.Apple,10, false),
			             new UnlockCardReward (CardType.WhiteRabbit)),
			new Mission ("Carrot Master","Collect 30 carrot pairs",
			             "You have just Add 5 points to every carrot pair",
			             new CollectTask (CardType.Carrot,60),
			             new AdditionPointReward (CardType.Carrot,5)),
			new Mission ("Apple Master","Collect 50 apple pairs",
			             "You have just Add 5 points to every apple pair",
			             new CollectTask (CardType.Apple,100),
			             new AdditionPointReward (CardType.Apple,5)),
			new Mission ("Juice mix", "Collect 4 mushroom, 4 apple, 4 carrot in 1 game",
			             "You have just earned 25 coins",
			             new CollectTask (new Dictionary<string,int> () {{CardType.Mushroom.ToString(),8},
																		 {CardType.Apple.ToString(),8},
																		 {CardType.Carrot.ToString(),8}}
							,false),
			             new CoinReward (25)),
			new Mission ("Colllect rabbit", "collect 5 whiterabbit pairs, 5 rabbit king pairs",
			             "You have just earned 25 coints",
			             new CollectTask (new Dictionary<string,int> () {{CardType.WhiteRabbit.ToString(),10},
																		{CardType.RabbitKing.ToString(),10}}
										,true),
			             new CoinReward (25)),
			new Mission (new MissionText ("Big Score"),
			             new MissionText ("Score @D points",500),
			             new MissionText ("You have just earned @D coins", 50),
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

	private static string MissionFilePath (RegionType type)
	{
		return Application.persistentDataPath + "_" + type.ToString() + "_mission.xml";
	}
	private static string CurrentMissionFilePath (RegionType type) 
	{
		return Application.persistentDataPath + "_" + type.ToString() + "_current_mission.xml";
	}
	#endregion
}

