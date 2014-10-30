using UnityEngine;
using System.Collections;

public class JungleBook : MonoBehaviour 
{
	public Transform cardTransform;
	public TextMesh  descriptionText;

	public CardType cardType {
		get {return cardType;}
		set {
			cardType = value;
			if (this.cardTransform) {
				this.cardTransform.GetComponent<SpriteRenderer>().sprite = CardScript.GetSprite (value);
			}
			if (this.descriptionText) {
				this.descriptionText.text = PHUtility.FormatStringMultiLine (Card.Description(value), 30);
			}
		}
	}
}
