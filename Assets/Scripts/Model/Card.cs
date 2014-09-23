using System;
using System.Collections;
using System.Collections.Generic;

public enum CardType {
	Mashroom,
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
	private int        _point;
	private CardType   _type;
	private CardEffect _effect;
	
	public int point {
		get {
			return _point;
		}
	}
	public CardType type {
		get {
			return _type;
		}
	}
	public CardEffect effect {
		get {
			return _effect;
		}
	}
	
	static Dictionary<int, int> cardPoint = new Dictionary<int, int> () 
	{
		{(int)CardType.Mashroom, 10},
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
	
	public Card(CardType type) {
		_type   = type;
		_point  = (int)cardPoint [(int)type];
		_effect = CardEffect.None;
	}
}

