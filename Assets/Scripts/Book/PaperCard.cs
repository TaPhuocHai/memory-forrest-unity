using UnityEngine;
using System.Collections;

public class PaperCard : MonoBehaviour 
{
	public Transform  cardTransform;
	public UILabel  nameText;
	public UILabel  descriptionText;

	private CardType _cardType;
	public CardType cardType {
		get {return _cardType;}
		set {
			_cardType = value;
			if (this.cardTransform) {
				this.cardTransform.GetComponent<UI2DSprite>().sprite2D = CardScript.GetSprite (value);
			}
			if (this.descriptionText) {
				this.descriptionText.text = PHUtility.FormatStringMultiLine (Card.Description(value), 30);
			}
			if (nameText) {
				this.nameText.text = cardType.ToString();
			}
		}
	}
}
