using UnityEngine;
using System;
using System.Collections;

public class MapScript : MonoBehaviour {

	void Awake () 
	{
		Card.Initialize ();
	}

	void Start () {	
//		Mission mission2 = new Mission ("Carrot Harvest", "Collect 10 carrot pairs", 
//		             					new CollectTask (CardType.Carrot, 10), 
//		                                new MoreTimeReward (10));
//		if (UnityXMLSerializer.SerializeToXMLFile<Mission> (Application.persistentDataPath + "mission2.xml", mission2, true)) {
//			print ("ghi file success");
//		} else {
//			print ("ghi file faild");
//		}
//
//		Mission mission = UnityXMLSerializer.DeserializeFromXMLFile<Mission> (Application.persistentDataPath + "mission2.xml");
//		if (mission != null) {
//			print (mission.id.ToString ());
//			print (mission.name.ToString ());
//			print (mission.description.ToString ());
//			print (mission.isIncremental.ToString ());
//			if (mission.missionTask != null) {
//				CollectTask task = (CollectTask)mission.missionTask;
//				foreach (string key in task.cardTypeWithNumberPairNeedCollect.Keys) {
//					int value = task.cardTypeWithNumberPairNeedCollect[key];
//					print (key + " : " + value.ToString());
//				}
//			} else {
//				print ("can't read taks");
//			}
//			if (mission.missionReward != null) {
//				MoreTimeReward reward = (MoreTimeReward)mission.missionReward;
//				print ("reward" + reward.second.ToString());
//			} else {
//				print ("can't read reward");
//			}
//		} else {
//			print ("read file faild");
//		}
	}

	void Update () {	
	}

	public void EnterMap4x4 () {
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.KingdomOfRabbits);
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMap5x5 () {	
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.Forest);
		PlayerPrefs.Save ();
		//Application.LoadLevel ("Mission");
	}

	public void EnterMap5x5_2 () {
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.StoneMountain);
		PlayerPrefs.Save ();
		//Application.LoadLevel ("Mission");
	}

	public void EnterMap6x6 () {
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.WolfCamp);
		PlayerPrefs.Save ();
		//Application.LoadLevel ("Mission");
	}
}
