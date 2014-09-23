using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum RegionType
{
	KingdomOfRabbits,
	Forest,
	StoneMountain,
	WolfCamp
}

public class Region 
{
	/// <summary>
	/// Gets the cards.
	/// </summary>
	/// <returns>Danh sach : loai card - so luong</returns>
	public static Dictionary<int, int> GetCards (RegionType region, int numberOfCol, int numberOfRow) 
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
}

