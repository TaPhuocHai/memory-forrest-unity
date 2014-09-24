using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public enum CardType {
	Mushroom,
	Apple,
	Carrot,
	WhiteRabbit,
	BrownRabbit,
	RabbitKing,
	PineApple,
	Strawberry,
	Wolf,  // Speical
	Peace,
	Stone, // Speical
	BlueButterfly, // Speical
	RedButterfly,  // Speical
	YellowButterfly, // Speical
	VioletButterfly, // Speical
	WolfKing, // Speical
	Banana,
	Grape,
	Pears,
	Cherry
}

public enum CardEffect {
	None,
	Effect1,
	Effect2,
}

public enum CardFaceBack {
	Normal,
	Special
}

public class Card
{
	private CardType   _type;
	private CardEffect _effect;

	#region Properties 

	public int point {
		get {
			return Card.GetPoint(_type); 
		}
	}
	public CardType type {
		get { return _type; }
	}
	public CardEffect effect {
		get { return _effect; }
	}

	#endregion Properties
	
	public Card(CardType type) {
		_type   = type;
		_effect = CardEffect.None;
	}

	#region Static function

	// Default point of card
	static Dictionary<int, int> cardPointDefault = new Dictionary<int, int> () 
	{
		{(int)CardType.Mushroom, 10},
		{(int)CardType.Apple, 15},
		{(int)CardType.Carrot, 30},
		{(int)CardType.WhiteRabbit, 50},
		{(int)CardType.BrownRabbit, 50},
		{(int)CardType.RabbitKing, 200},
		{(int)CardType.PineApple, 40},
		{(int)CardType.Strawberry, 60},
		{(int)CardType.Wolf, 100},
		{(int)CardType.Peace, 70},
		{(int)CardType.Stone, 30},
		{(int)CardType.BlueButterfly, 35},
		{(int)CardType.RedButterfly, 35},
		{(int)CardType.YellowButterfly, 35},
		{(int)CardType.VioletButterfly, 35},
		{(int)CardType.WolfKing, 250},
		{(int)CardType.Banana, 60},
		{(int)CardType.Grape, 70},
		{(int)CardType.Pears, 80},
		{(int)CardType.Cherry, 90},
	};

	/// <summary>
	/// Get point of card.
	/// </summary>
	/// <returns>The point.</returns>
	/// <param name="type">card type.</param>
	public static int GetPoint (CardType type) 
	{
		string pointKey = "POINT_CARD_" + type.ToString ();
		int savedPointOfCard = PlayerPrefs.GetInt(pointKey,0);

		// Neu chua co gia tri thi lay default
		if (savedPointOfCard == 0) {
			savedPointOfCard  = (int)cardPointDefault [(int)type];
		}
		return savedPointOfCard;
	}

	/// <summary>
	/// Save new point for card type
	/// </summary>
	/// <param name="point">Point.</param>
	/// <param name="type">Type.</param>
	public static void SavePoint (int point, CardType type) 
	{
		string pointKey = "POINT_CARD_" + type.ToString ();
		int savedPointOfCard = PlayerPrefs.GetInt(pointKey,0);

		// Validate point > saved point
		if (savedPointOfCard < point) {
			Debug.Log ("Card.SavePoint : point less than saved. No save");
			return;
		}

		// Save
		PlayerPrefs.SetInt (pointKey, point);
		PlayerPrefs.Save ();
	}

	/// <summary>
	/// Determines if is unlock the specified cardType.
	/// </summary>
	/// <returns><c>true</c> if is unlock the specified cardType; otherwise, <c>false</c>.</returns>
	/// <param name="cardType">Card type.</param>
	public static bool IsUnlock (CardType cardType)
	{
		string unlockKey = "UNLOCK_CARD_" + cardType.ToString ();
		int unlockValue = PlayerPrefs.GetInt (unlockKey, 0);
		if (unlockValue == 1) {
			return true;
		}
		return false;
	}

	/// <summary>
	/// Unlock the specified cardType.
	/// </summary>
	/// <param name="cardType">Card type.</param>
	public static void Unlock (CardType cardType) 
	{
		string unlockKey = "UNLOCK_CARD_" + cardType.ToString ();
		PlayerPrefs.SetInt (unlockKey, 1);
		PlayerPrefs.Save ();
	}

	#endregion Static function

	#region Init

	/// <summary>
	/// Initialize default value.
	/// </summary>
	public static void Initialize () 
	{
		/// ----------------------------------------------------------------------
		/// Chi Init 1 lan duy nhat khi User moi cai dat

		int didCardInitValue = PlayerPrefs.GetInt ("CARD_INITIALIZE", 0);
		if (didCardInitValue == 1) {
			return;
		}

		CardType[] unlockDefault = new CardType[] {
			// Place 1
			CardType.Mushroom,
			CardType.Apple,
			CardType.Carrot,
			// Place 2
			CardType.BrownRabbit,
			CardType.RabbitKing,
			CardType.Wolf,
			// Place 3
			CardType.Stone,
			// Place 4
			CardType.WolfKing
		};

		foreach (CardType type in unlockDefault) {
			string unlockKey = "UNLOCK_CARD_" + type.ToString ();
			PlayerPrefs.SetInt(unlockKey,1);
		}

		PlayerPrefs.SetInt("CARD_INITIALIZE",1);
		PlayerPrefs.Save ();
		/// ----------------------------------------------------------------------
	}

	#endregion Init
}

