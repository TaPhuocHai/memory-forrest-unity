using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardScript : MonoBehaviour {

	private Card     _card;
	private CardType _cardType;

	static Sprite[] sprites;
	static Sprite[] faceBackSprite;

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

			// Load sprite if not loaded
			if (CardScript.sprites == null) {
				CardScript.sprites = Resources.LoadAll<Sprite>("Textures/Card");
			}

			// Set sprite for this card
			var cardFace = this.transform.FindChild("CardFace");
			if (cardFace) {
				SpriteRenderer spriteRender = cardFace.GetComponent<SpriteRenderer>();
				if (spriteRender) {
					spriteRender.sprite = CardScript.sprites[(int)value + 1] as Sprite;
				}
			}
		}
	}

	#endregion Properties

	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	void DestroyMe () {
		Destroy (this.gameObject);
	}

	public void ChangeFaceBack (CardFaceBack type) {
		var cardBack = this.transform.FindChild ("CardBack");
		SpriteRenderer spriteRender = cardBack.GetComponent<SpriteRenderer> ();

		if (type == CardFaceBack.Normal) {
			spriteRender.sprite = CardScript.sprites [0] as Sprite;
		} else {
			if (CardScript.faceBackSprite == null) {
				CardScript.faceBackSprite = Resources.LoadAll<Sprite>("Textures/GreenBackCard");
			}
			spriteRender.sprite = CardScript.faceBackSprite [0] as Sprite;
		}
	}
}
