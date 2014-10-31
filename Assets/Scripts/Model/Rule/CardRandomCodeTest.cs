using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class CardRandomCodeTest 
{
	static int NUMBER_ROUND = 4;
	static int NUMBER_TEST_IN_ROUND = 5;
	/*
	/// <summary>
	/// Khi chay cac RunTestMap cac card co the bi unlock, goi ham nay de tra ve mat dinh
	/// </summary>
	public static void ResertLockCardDefault () 
	{
		Region.UnlockMap (RegionType.KingdomOfRabbits, false, true);
		Region.UnlockMap (RegionType.Forest, false, true);
		Region.UnlockMap (RegionType.StoneMountain, false, true);
		Region.UnlockMap (RegionType.WolfCamp, false, true);

		// Lock 1 so card cua map 4
		Card.Unlock (CardType.Banana, false);
		Card.Unlock (CardType.Grape, false);
		Card.Unlock (CardType.Pears, false);
		Card.Unlock (CardType.Cherry, false);
	}

	public static void RunTestMap1 () 
	{
		Region map1 = Region.Instance (RegionType.KingdomOfRabbits);

		var  fileName = Application.persistentDataPath + "/Map1_Rule_Random_Test.txt";
		if (File.Exists(fileName)) {
			File.Delete(fileName);
			return;
		}
		var sr = File.CreateText(fileName);

		// Lock White Rabbit
		Region.UnlockMap (RegionType.Forest, false, true);
		Region.UnlockMap (RegionType.StoneMountain, false, true);
		Region.UnlockMap (RegionType.WolfCamp, false, true);

		sr.WriteLine ("MAP 1 : Lock  -------------------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");

			for (int j = 0; j < NUMBER_TEST_IN_ROUND ; j ++) {
				Dictionary<CardType, int> list = map1.GetCards (i);

				string data = "          TEST " + j.ToString() + " -  ";
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		// Unlock White Rabbit
		// Unlock map 2 thi cac card cua map 1 tu dong unlock het
		Region.UnlockMap (RegionType.Forest, true, true);

		sr.WriteLine ("");
		sr.WriteLine ("");
		sr.WriteLine ("MAP 1 : Unlock all card  --------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				Dictionary<CardType, int> list = map1.GetCards (i);

				string data = "          TEST " + j.ToString() + " -  ";
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		sr.Close ();

		Debug.Log ("RunTestMap1 success");
	}

	public static void RunTestMap2 () 
	{
		Region map = Region.Instance (RegionType.Forest);

		// Open file
		var  fileName = Application.persistentDataPath + "/Map2_Rule_Random_Test.txt";
		if (File.Exists(fileName)) {
			File.Delete(fileName);
			return;
		}
		var sr = File.CreateText(fileName);

		// Lock cac card khong can thiet
		Region.UnlockMap (RegionType.StoneMountain, false, true);
		Region.UnlockMap (RegionType.WolfCamp, false, true);
		// Goi unlock map 2 thi cac card cua map1 tu dong unlock het
		Region.UnlockMap (RegionType.Forest, true, true);

		sr.WriteLine ("MAP 2 : Lock  -------------------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				Dictionary<CardType, int> list = map.GetCards (i);

				string data = "          TEST " + j.ToString() + " -  ";
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		// Unlock map 3 thi cac card cua map 2 tu dong unlock het
		Region.UnlockMap (RegionType.StoneMountain, true, true);

		sr.WriteLine ("");
		sr.WriteLine ("");
		sr.WriteLine ("MAP 2 : Unlock all card  --------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				string data = "          TEST " + j.ToString() + " -  ";
				Dictionary<CardType, int> list = map.GetCards (i);

				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		sr.Close();
		Debug.Log ("RunTestMap2 success");
	}

	public static void RunTestMap3 () 
	{
		Region map = Region.Instance (RegionType.StoneMountain);
		
		// Open file
		var  fileName = Application.persistentDataPath + "/Map3_Rule_Random_Test.txt";
		if (File.Exists(fileName)) {
			File.Delete(fileName);
			return;
		}
		var sr = File.CreateText(fileName);

		// Lock cac card khong can thiet
		Region.UnlockMap (RegionType.WolfCamp, false, true);
		// Goi unlock map 3 thi cac card cua map2 & map1 tu dong unlock het
		Region.UnlockMap (RegionType.StoneMountain, true, true);

		
		sr.WriteLine ("MAP 3 : Lock  -------------------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				Dictionary<CardType, int> list = map.GetCards (i);
				
				string data = "          TEST " + j.ToString() + " -  ";
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		// Chi unlock Blue Butterfly & Red Butterfly 
		Card.Unlock (CardType.BlueButterfly, true);
		Card.Unlock (CardType.RedButterfly, true);

		sr.WriteLine ("");
		sr.WriteLine ("");
		sr.WriteLine ("MAP 3 : Unlock  Blue Butterfly & Red Butterfly  -----------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				string data = "          TEST " + j.ToString() + " -  ";
				Dictionary<CardType, int> list = map.GetCards (i);
				
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		// Unlock map 4 thi cac card cua map 3 tu dong unlock het
		Region.UnlockMap (RegionType.WolfCamp, true, true);
		
		sr.WriteLine ("");
		sr.WriteLine ("");
		sr.WriteLine ("MAP 3 : Unlock all card  --------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				string data = "          TEST " + j.ToString() + " -  ";
				Dictionary<CardType, int> list = map.GetCards (i);
				
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}
		
		sr.Close();

		Debug.Log ("RunTestMap3 success");
	}

	public static void RunTestMap4 () 
	{
		Region map = Region.Instance (RegionType.WolfCamp);
		
		// Open file
		var  fileName = Application.persistentDataPath + "/Map4_Rule_Random_Test.txt";
		if (File.Exists(fileName)) {
			File.Delete(fileName);
			return;
		}
		var sr = File.CreateText(fileName);

		// Goi unlock map 4 thi cac card cua map 3 tu dong unlock het
		Region.UnlockMap (RegionType.Forest, true, true);
		Region.UnlockMap (RegionType.StoneMountain, true, true);
		Region.UnlockMap (RegionType.WolfCamp, true, true);		

		// Lock cac card can unlock cua map 4
		Card.Unlock (CardType.Banana, false);
		Card.Unlock (CardType.Grape, false);
		Card.Unlock (CardType.Pears, false);
		Card.Unlock (CardType.Cherry, false);
		
		sr.WriteLine ("MAP 4 : Lock  -------------------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				Dictionary<CardType, int> list = map.GetCards (i);
				
				string data = "          TEST " + j.ToString() + " -  ";
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}
		
		// Unlock banane & Grape
		Card.Unlock (CardType.Banana, true);
		Card.Unlock (CardType.Grape, true);
		
		sr.WriteLine ("");
		sr.WriteLine ("");
		sr.WriteLine ("MAP 4 : Unlock Banana & Grape --------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				string data = "          TEST " + j.ToString() + " -  ";
				Dictionary<CardType, int> list = map.GetCards (i);
				
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}

		// Unlock all card
		Card.Unlock (CardType.Pears, true);
		Card.Unlock (CardType.Cherry, true);

		sr.WriteLine ("");
		sr.WriteLine ("");
		sr.WriteLine ("MAP 4 : Unlock all card  --------------------------------------------------------------------------------");
		for (int i = 0; i < NUMBER_ROUND; i ++) {
			sr.WriteLine ("     ROUND " + (i + 1).ToString() + "---------------------------------------------");
			
			for (int j = 0; j < NUMBER_TEST_IN_ROUND; j ++) {
				string data = "          TEST " + j.ToString() + " -  ";
				Dictionary<CardType, int> list = map.GetCards (i);
				
				foreach (CardType type in list.Keys) {
					data += type.ToString () + " : " + list[type].ToString() + "  ||  ";
				}
				sr.WriteLine (data);
			}
			sr.WriteLine ("");
		}
		
		sr.Close();

		Debug.Log ("RunTestMap4 success");
	}	
	*/
}
