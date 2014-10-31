using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

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
	Peach,
	Stone, // Speical
	BlueButterfly, // Speical
	RedButterfly,  // Speical
	YellowButterfly, // Speical
	VioletButterfly, // Speical
	WolfKing, // Speical
	Banana,
	Grape,
	Pears,
	Cherry,
	Coins5,
	Coins10,
	Coins20,
	Coins50,
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
	#region Properties 

	public int point {
		get {
			return Card.GetPoint(this.type); 
		}
	}

	public CardType   type { get; private set; }
	public CardEffect effect { get; private set; }

	#endregion Properties

	public Card () {}
	
	public Card(CardType type) {
		this.type   = type;
		this.effect = CardEffect.None;
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
		{(int)CardType.Peach, 70},
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
	public static bool IsUnlock (CardType cardType, RegionType regionType)
	{
		string unlockKey = "UNLOCK_CARD_" + cardType.ToString () + "_" + regionType.ToString();
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
	public static void Unlock (CardType cardType, RegionType regionType, bool isUnlock) 
	{
		string unlockKey = "UNLOCK_CARD_" + cardType.ToString () + "_" + regionType.ToString();
		if (isUnlock) {
			PlayerPrefs.SetInt (unlockKey, 1);
		} else {
			PlayerPrefs.SetInt (unlockKey, 0);
		}
		PlayerPrefs.Save ();
	}

	private static Dictionary<CardType,string> cardDescription;
	public static string Description (CardType cardType) 
	{
		if (cardDescription == null) {
			cardDescription = new Dictionary<CardType, string> {
				{CardType.Mushroom, "Easy to find in forest" },
				{CardType.Apple, "Adam, Eva ate apple for the fist time"},
				{CardType.Carrot, "Favourite food of Rabbit"},
				{CardType.WhiteRabbit, "A Rabbit warriot who searching for King"},
				{CardType.BrownRabbit, "Brown Rabbit have more experience than white rabbit"},
				{CardType.RabbitKing, "Rabbit King is kidnapped by wolves"},
				{CardType.PineApple, "Pine Apple is very tasty"},
				{CardType.Strawberry, "All cake have strawberry"},
				{CardType.Wolf, "Evil force of forest. When you flip him, he will change the position of 3 random card"},
				{CardType.Peach, "Beautiful and tasty, very hard to find in the forest"},
				{CardType.Stone, "Stone blocked sight and path of rabbits. Stone don't have pair, you must flip all card before Stone card disappear"},
				{CardType.BlueButterfly, "You can saw Blue butterfly near the river. Flip pair of Blue Butterfly will also flip 3 random pairs."},
				{CardType.RedButterfly, "Red likes a sun. Add more time for you"},
				{CardType.YellowButterfly, "Yellow butterfly dust make card change their color. Try to use it as advantage"},
				{CardType.VioletButterfly, "Violet Butterfly is the rarest kind of butterfly. They help you flip on cards of the most crowded kind."},
				{CardType.WolfKing, "The leader of the wolf. When you flip him he will change all the position of cards remained"},
				{CardType.Banana, "Actually, it's a monkey food more than rabbit food. But it's ok with rabbits."},
				{CardType.Grape, "Juicy but so complicated to eat"},
				{CardType.Pears, "Sweet fruit you can't never have enough"},
				{CardType.Cherry, "Hard to find in the forest but worth it"},
				{CardType.Coins5, "Give you 5 coins"},
				{CardType.Coins10, "Give you 10 coins"},
				{CardType.Coins20, "Give you 20 coins"},
				{CardType.Coins50, "Give you 50 coins"},
			};
		}
		return cardDescription[cardType];
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

