using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardScript : MonoBehaviour {

	private Card     _card;
	private CardType _cardType;

	static Sprite[] sprites;

	#region Properties

	public Card card {
		get { return _card; }
	}

	public CardType cardType {
		get {
			return _cardType;
		}
		set {
			_cardType = value;

			// Set card properties
			_card = new Card(value);

			// Set sprite for this card
			var cardFace = this.transform.FindChild("CardFace");
			if (cardFace) {
				SpriteRenderer spriteRender = cardFace.GetComponent<SpriteRenderer>();
				if (spriteRender) {								
					Sprite sprite = CardScript.GetSprite (value);
					if (sprite == null) {
						DebugScript.AddText ("khong tim thay sprite \n");

					}
					spriteRender.sprite = sprite;
				}
			}
		}
	}

	#endregion Properties

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	void DestroyMe () 
	{
		Destroy (this.gameObject);
	}

	public void ChangeFaceBack (CardFaceBack type) {
		var cardBack = this.transform.FindChild ("CardBack");
		SpriteRenderer spriteRender = cardBack.GetComponent<SpriteRenderer> ();

		if (type == CardFaceBack.Normal) {
			spriteRender.sprite = CardScript.sprites [0] as Sprite;
		} else {
			spriteRender.sprite = CardScript.sprites[(int)CardType.Coins50 + 2] as Sprite;
		}
	}

	public static Sprite GetSprite (CardType cardType)
	{
		// Load sprite if not loaded
		if (CardScript.sprites == null) {
			CardScript.sprites = Resources.LoadAll<Sprite>("Textures/Card");
		}

		return CardScript.sprites [(int)cardType] as Sprite;
	}
}
