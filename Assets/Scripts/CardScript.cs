using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum CardType {
	Mashroom,
	Apple,
	Carrot,
	WhiteRabbit,
	BrownRabbit,
	RabbitKing,
	Wolf,
	WolfKing,
	Stone,
	BlueButterfly,
	RedButterfly,
	YellowButterfly,
	VioletButterfly
}

public enum CardEffect {
	None,
	Effect1,
	Effect2,
}

public class CardProperties {	

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
		{(int)CardType.Wolf, 100},
		{(int)CardType.WolfKing, 250},
		{(int)CardType.Stone, 30},
		{(int)CardType.BlueButterfly, 35},
		{(int)CardType.RedButterfly, 35},
		{(int)CardType.YellowButterfly, 35},
		{(int)CardType.VioletButterfly, 35},
	};

	public CardProperties(CardType type) {
		_type   = type;
		_point  = (int)cardPoint [(int)type];
		_effect = CardEffect.None;
	}
}

public class CardScript : MonoBehaviour {

	private CardProperties _cardProperties;
	private CardType       _cardType;

	static Sprite[] sprites;

	#region Properties

	public CardProperties cardProperties {
		get {
			return _cardProperties;
		}
	}

	public CardType cardType {
		get {
			return _cardType;
		}
		set {
			_cardType = value;

			// Set card properties
			_cardProperties = new CardProperties(value);

			// Set sprite for this card
			var cardFace = this.transform.FindChild("CardFace");
			if (cardFace) {
				SpriteRenderer spriteRender = cardFace.GetComponent<SpriteRenderer>();
				if (spriteRender) {
					spriteRender.sprite = CardScript.sprites[(int)value + 1] as Sprite;
				} else {
					print ("not found sprite render");
				}
			} else {
				print ("not found card face");
			}
		}
	}

	#endregion Properties

	// Use this for initialization
	void Start () {
		// Load sprite if not loaded
		if (CardScript.sprites == null) {
			sprites = Resources.LoadAll<Sprite>(@"Textures/Card");
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
