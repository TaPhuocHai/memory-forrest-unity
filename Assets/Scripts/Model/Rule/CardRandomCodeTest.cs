using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CardRandomCodeTest 
{
	public static void RunTestMap1 () 
	{
		Region map1 = Region.Instance (RegionType.KingdomOfRabbits);

		// Lock White Rabbit
		Region.UnlockMap (RegionType.Forest, false, true);

		Debug.Log ("MAP 1 : Lock WhiteRabbit ----------------------------------------------------------------------------");
		for (int i = 0; i < 3; i ++) {
			Debug.Log ("ROUND " + (i + 1).ToString() + "--------------------------------------");

			for (int j = 0; j <= 2; j ++) {
				Debug.Log ("TEST " + j.ToString() + "--------------------");
				Dictionary<CardType, int> list = map1.GetCards (i);
				foreach (CardType type in list.Keys) {
					Debug.Log (type.ToString () + " : " + list[type].ToString());
				}
			}
		}

		// Unlock White Rabbit
		Region.UnlockMap (RegionType.Forest, true, true);

		Debug.Log ("MAP 1 : Unlock WhiteRabbit ----------------------------------------------------------------------------");
		for (int i = 0; i < 3; i ++) {
			Debug.Log ("ROUND " + (i + 1).ToString() + "--------------------------------------");
			
			for (int j = 0; j <= 2; j ++) {
				Debug.Log ("TEST " + j.ToString() + "--------------------");
				Dictionary<CardType, int> list = map1.GetCards (i);
				foreach (CardType type in list.Keys) {
					Debug.Log (type.ToString () + " : " + list[type].ToString());
				}
			}
		}

	}

	public static void RunTestMap2 () 
	{
		Region map = Region.Instance (RegionType.Forest);

		var  fileName = Application.persistentDataPath + "/Map2_Rule_Random_Test.txt";
		if (File.Exists(fileName)) {
			File.Delete(fileName);
			return;
		}

		var sr = File.CreateText(fileName);

		// Lock White Rabbit
		Region.UnlockMap (RegionType.StoneMountain, false, true);
		
		sr.WriteLine ("MAP 2 : Lock  -------------------------------------------------------------------------------------------");
		for (int i = 0; i < 3; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j <= 2; j ++) {
				sr.WriteLine ("          TEST " + j.ToString() + "--------------------");
				Dictionary<CardType, int> list = map.GetCards (i);

				string data = "               ";
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}
		
		// Unlock White Rabbit
		Region.UnlockMap (RegionType.StoneMountain, true, true);

		sr.WriteLine ("");
		sr.WriteLine ("");
		sr.WriteLine ("MAP 2 : Unlock all card  --------------------------------------------------------------------------------");
		for (int i = 0; i < 3; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j <= 2; j ++) {
				sr.WriteLine ("          TEST " + j.ToString() + "--------------------");
				Dictionary<CardType, int> list = map.GetCards (i);

				string data = "               ";
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		sr.Close();
	}
}
