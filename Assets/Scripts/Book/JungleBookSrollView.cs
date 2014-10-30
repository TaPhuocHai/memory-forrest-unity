using UnityEngine;
using System.Collections;
using System;

public class JungleBookSrollView : MonoBehaviour 
{
	public Transform jungleBook;

	void Start () 
	{
		float ratio = 1.0f;
		float left = 0;
		UIScrollView scrollView = this.gameObject.GetComponent<UIScrollView> ();

		var values = Enum.GetValues(typeof(CardType));
		foreach (CardType cardType in values) {
			var book = Instantiate(this.jungleBook) as Transform;
			book.parent = this.gameObject.transform;
			UI2DSprite sprite = book.GetComponent<UI2DSprite> ();
			sprite.transform.position = new Vector3 (0,0,0);
			sprite.transform.localScale = new Vector3(ratio,ratio,1);
			sprite.SetRect(left, - (sprite.height * ratio)/2, sprite.width * ratio, sprite.height * ratio);

			PaperCard paperCard = book.GetComponent<PaperCard> ();
			paperCard.cardType = cardType;

			left += sprite.width * ratio + 10;
		}

		scrollView.ResetPosition ();
	}
}
