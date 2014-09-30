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
	/// <param name="round">Round : bat dau tu 0.</param>
	public Dictionary<CardType, int> GetCards (int round) 
	{
		int numberOfObjectToDraw = this.numberOfCol * this.numberOfRow;

		// Rule random
		CardRandomCode cardRandomCode = Region.GetCardRandomCode (this._regionType, round);
		return cardRandomCode.GetCards (numberOfObjectToDraw);
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
	
	/// <summary>
	/// Xoa thong tin mission da finish
	/// Ham nay chi co gia tri khi Constant.kClearMissionData = true
	/// </summary>
	/// <param name="missionId">Mission identifier.</param>
	public void ClearDataFinishMission () 
	{
		if (!Constant.kClearMissionData) {
			return;
		}

		if (this.missions != null && this.missions.Count != 0) {
			foreach (Mission mission in this.missions) {
				mission.ClearUnlockMission ();
			}
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

		// Tao moi danh sach neu can thiet
		if (_currentMissions == null) {
			_currentMissions = new List<Mission>();
		}

		if (currentMissionId.Count != 0) {
			// Duyet qua cac phan tu cua currentMissionId,
			// Bam bao rang currentMissionId chua cac misison co trong danh sach _missions
			// Neu id do khong ton tai, thi xoa no khoi danh sach currentMissionId
			for (int i = 0 ; i < currentMissionId.Count ; i ++) {
				int id = currentMissionId[i];
				bool idIsExitInMission = false;
				foreach (Mission mission in this.missions) {
					if (id == mission.id) {
						idIsExitInMission = true;
						break;
					}
				}
				// Neu id do khong ton tai trong _missions
				if (!idIsExitInMission) {
					currentMissionId.Remove(id);
					i --;
				}
			}
		}

		//Cap nhat danh sach Current Mission
		foreach (int id in currentMissionId) {
			bool idIsExitInCurrentMission = false;
			// Tim xem no da ton tai trong current mission chua
			foreach (Mission mission in _currentMissions) {
				if (mission.id == id) {
					idIsExitInCurrentMission = true;
					break;
				}
			}

			// Neu chua ton tai thi them moi
			if (!idIsExitInCurrentMission) {
				foreach (Mission mission in this.missions) {
					if (mission.id == id) {
						_currentMissions.Add (mission);
					}
				}
			}
		}

		// Kiem tra xem can lay them bao nhieu mission
		int countNeedMore = 0;
		if (currentMissionId == null) {
			countNeedMore = Constant.kMaxCurrentMisison;
		}
		if (currentMissionId.Count < Constant.kMaxCurrentMisison) {
			countNeedMore += Constant.kMaxCurrentMisison - currentMissionId.Count; 
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
	public static void UnlockRound (RegionType regionType, int round, bool isUnlock) 
	{
		string key = regionType.ToString () + round.ToString ();
		if (isUnlock) {
			PlayerPrefs.SetInt (key, 1);
		} else {
			PlayerPrefs.SetInt (key, 0);
		}
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
		if (System.IO.File.Exists (Region.MissionFilePath (RegionType.KingdomOfRabbits))) {
			Debug.Log ("FileMission da ton tai");
			return;
		} else {
			Debug.Log ("FileMission khong ton tai");
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
			             new CollectTask (new Dictionary<CardType,int> () {{CardType.Mushroom,8},
																		 {CardType.Apple,8},
																		 {CardType.Carrot,8}}
							,false),
			             new CoinReward (25)),
			new Mission ("Colllect rabbit", "collect 5 whiterabbit pairs, 5 rabbit king pairs",
			             "You have just earned 25 coints",
			             new CollectTask (new Dictionary<CardType,int> () {{CardType.WhiteRabbit,10},
																		{CardType.RabbitKing,10}}
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

		// -----------------------------------------------------------------------------------------------
		// Region Forest

		List<Mission> listMissionOfForest = new List<Mission> () {
			new Mission ("Pine Apple Tree", "Collect 10 apples and 10 carrots in one game",
			             "You have just Unlock pine apple",
			             new CollectTask(new Dictionary<CardType,int>() {{CardType.Apple, 10},
																		 {CardType.Carrot, 10}
											}, false),
			             new UnlockCardReward(CardType.PineApple)),
			new Mission ("Unlock Strawberry", "Collect 40 apple and  40 mushroom",
			             "You have just Unlock Strawberry",
			             new CollectTask(new Dictionary<CardType,int>() {{CardType.Apple, 80},
																		{CardType.Mushroom, 80}
											}, true),
			             new UnlockCardReward(CardType.Strawberry)),						
			new Mission (new MissionText ("Amazing Score"),
			             new MissionText ("Score @D points",800),
			             new MissionText ("You have just earned @D coins", 100),
			             new ScoreTask (800,true),
			             new CoinReward (100),true,300,20),
			new Mission ("Harvest time", "Collect 4 mushroom, 4 Pine Apple, 4 Strawberry in 1 game",
			             "You have just earned 50 coins",
			             new CollectTask(new Dictionary<CardType,int>() {{CardType.Mushroom, 8},
																		 {CardType.PineApple, 8},
																		 {CardType.Strawberry, 8}
											}, false),
			             new CoinReward(50)),
			new Mission ("Army of Rabbit", "Collect 10 Brown Rabbit, 10 White Rabbits",
			             "You have just earned 50 coins",
			             new CollectTask(new Dictionary<CardType,int>() {{CardType.WhiteRabbit, 20},
																		{CardType.BrownRabbit, 20}
												}, true),
			             new CoinReward(50)),
			new Mission (new MissionText ("Explore the forrest"), 
			             new MissionText ("Collect all card before time run out"),
			             new MissionText ("You have just Unlock Round @D",3),
			             new CollectAllCardTask (1), 
			             new UnlockExtraRoundReward (RegionType.Forest, 2),true,1,1),
			new Mission ("Money can buy time", "Have 200 coin in you pocket",
			             "You have just Add 10s to timer",
			             new CoinTask(200),
			             new MoreTimeReward(10))
		};
		// Save to file
		if (UnityXMLSerializer.SerializeToXMLFile<List<Mission>> (Region.MissionFilePath(RegionType.Forest), listMissionOfForest, true)) {
			Debug.Log ("Init mission Forest success");
		} else {
			Debug.Log ("Init mission Forest faild");
		}

		// -----------------------------------------------------------------------------------------------
		// Region StoneMountain

		List<Mission> listMissionOfStoneMountain = new List<Mission> () {
			new Mission ("Wanna see some magic?","Escape 10 wolves",
			             "Unlock Blue Butterfly",
			             new CollectTask (CardType.Wolf,10),
			             new UnlockCardReward (CardType.BlueButterfly)),
			new Mission (new MissionText ("Explore the stone mountain"), 
			             new MissionText ("Collect all card before time run out"),
			             new MissionText ("You have just Unlock Round @D",4),
			             new CollectAllCardTask (2), 
			             new UnlockExtraRoundReward (RegionType.StoneMountain, 3),true,1,1),
			new Mission ("Rune Stone","Skip 10 Stone Card",
			             "Unlock Yellow Butterfly",
			             new CollectTask (CardType.Stone,10),
			             new UnlockCardReward (CardType.YellowButterfly)),
			new Mission ("Deep in the forest","Reach Round 4",
			             "Unlock Red Butterfly",
			             new ReachRoundTask (3),
			             new UnlockCardReward (CardType.RedButterfly)),
			new Mission (new MissionText ("Bigger Score Again"),
			             new MissionText ("Score @D points",1000),
			             new MissionText ("You have just earned @D coins", 150),
			             new ScoreTask (1000,true),
			             new CoinReward (150),true,400,30)
		};
		// Save to file
		if (UnityXMLSerializer.SerializeToXMLFile<List<Mission>> (Region.MissionFilePath(RegionType.StoneMountain), listMissionOfStoneMountain, true)) {
			Debug.Log ("Init mission StoneMountain success");
		} else {
			Debug.Log ("Init mission StoneMountain faild");
		}

		// -----------------------------------------------------------------------------------------------
		// Region WolfCamp

		List<Mission> listMissionOfWolfCamp = new List<Mission> () {
			new Mission ("Rescue the rabbit king","Rescue Rabbit King",
			             "You have earned 100 coins",
			             new CollectTask (CardType.RabbitKing,2),
			             new CoinReward (100)),
			new Mission ("Escape the Wolf King","Escape Wolf King 3 times",
			             "You have earned 100 coins",
			             new CollectTask (CardType.WolfKing,3,true),
			             new CoinReward (100)),
			new Mission ("Big harvest time", "3 Pine Apple, 3 carrot, 3 mushroom in 1 game",
			             "Unlock Banana",
			             new CollectTask(new Dictionary<CardType,int>() {{CardType.PineApple, 6},
																		{CardType.Carrot, 6},
																		{CardType.Mushroom, 6}
										}, false),
			             new UnlockCardReward (CardType.Banana)),
			new Mission ("Precious Fruits","Reach Round 6",
			             "Unlock Pear",
			             new ReachRoundTask (5),
			             new UnlockCardReward (CardType.Pears)),
			new Mission ("Fortune of Fruits","Collect 50 Bananas, 50 Grapes, 50 Pears",
			             "Unlock Pear",
			             new CollectTask(new Dictionary<CardType,int>() {{CardType.Banana, 100},
																		{CardType.Grape, 100},
																		{CardType.Pears, 100}
												}, true),
			             new UnlockCardReward (CardType.Cherry)),
			new Mission (new MissionText ("Bigger Score Again"),
			             new MissionText ("Score @D points",1500),
			             new MissionText ("You have just earned @D coins", 250),
			             new ScoreTask (1500,true),
			             new CoinReward (250),true,400,50),
			new Mission (new MissionText ("Explore the WolfCamp"), 
			             new MissionText ("Collect all card before time run out"),
			             new MissionText ("You have just Unlock Round @D",5),
			             new CollectAllCardTask (3), 
			             new UnlockExtraRoundReward (RegionType.WolfCamp, 4),true,1,1)
		};
		// Save to file
		if (UnityXMLSerializer.SerializeToXMLFile<List<Mission>> (Region.MissionFilePath(RegionType.WolfCamp), listMissionOfWolfCamp, true)) {
			Debug.Log ("Init mission WolfCamp success");
		} else {
			Debug.Log ("Init mission WolfCamp faild");
		}

	}	

	private static CardRandomCode GetCardRandomCode (RegionType regionType, int round) 
	{
		CardRandomCode cardRandomCode = null;
		if (regionType == RegionType.KingdomOfRabbits) {
			if (round == 0) {
				cardRandomCode = new CardRandomCode (
					 new ComplexRule (new Dictionary <CardType, float> () {
						{CardType.Mushroom, 30},
						{CardType.Apple, 30},
						{CardType.Carrot, 30},
						{CardType.WhiteRabbit, 10}})
				);
			} else {
				cardRandomCode = new CardRandomCode (
					 new ComplexRule (new Dictionary <CardType, float> () {
						{CardType.Mushroom, 25},
						{CardType.Apple, 25},
						{CardType.Carrot, 25},
						{CardType.WhiteRabbit, 25}})
				);
			}
		} else if (regionType == RegionType.Forest) {
			if (round == 0) {
				cardRandomCode = new CardRandomCode (
					new ComplexRule (
						new Dictionary <CardType,float> () {
							{CardType.Mushroom, 25},
							{CardType.Apple, 25},
							{CardType.Carrot, 25},
							{CardType.WhiteRabbit, 10},
							{CardType.PineApple, 15},
						}
					),
					new Dictionary<CardType, int> () {{CardType.Wolf, 1}}
				);
			} else if (round == 1) {
				cardRandomCode = new CardRandomCode (
					new ComplexRule (
						new Dictionary <CardType,float> () {
							{CardType.Strawberry, 15},
							{CardType.Apple, 25},
							{CardType.Carrot, 25},
							{CardType.BrownRabbit, 10},
							{CardType.PineApple, 25},
						}
					),
					new Dictionary<CardType, int> () {{CardType.Wolf, 1}}
				);
			} else {
				cardRandomCode = new CardRandomCode (
					new ComplexRule (
						new Dictionary <CardType,float> () {
							{CardType.RabbitKing, 10},
							{CardType.Strawberry, 15},
							{CardType.Apple, 20},
							{CardType.Carrot, 20},
							{CardType.BrownRabbit, 10},
							{CardType.PineApple, 25},
						}
					),
					new Dictionary<CardType, int> () {{CardType.Wolf, 3}}
				);
			}
		} else if (regionType == RegionType.StoneMountain) {
			if (round == 0) {
				cardRandomCode = new CardRandomCode (
					new ComplexRule (
						new Dictionary <CardType,float> () {
							{CardType.RabbitKing, 10},
							{CardType.Strawberry, 15},
							{CardType.Apple, 20},
							{CardType.Carrot, 20},
							{CardType.BrownRabbit, 10},
							{CardType.PineApple, 25},
						}
					),
					new Dictionary<CardType, int> () {{CardType.Stone, 2}}
				);
			} else if (round == 1) {
			} else {
			}
		} else if (regionType == RegionType.WolfCamp) {
		}
		return cardRandomCode;
	}

	private static string MissionFilePath (RegionType type)
	{
		return Application.persistentDataPath + "/" + type.ToString() + "_mission.xml";
	}
	private static string CurrentMissionFilePath (RegionType type) 
	{
		return Application.persistentDataPath + "/" + type.ToString() + "_current_mission.xml";
	}
	#endregion
}

