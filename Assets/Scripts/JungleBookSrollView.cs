using UnityEngine;
using System.Collections;

public class JungleBookSrollView : MonoBehaviour 
{
	public Transform jungleBook;

	void Start () 
	{

		float ratio = 1.0f;
		float left = 0;
		UIScrollView scrollView = this.gameObject.GetComponent<UIScrollView> ();

		for (int i = 0; i < 5; i ++) {
			var book = Instantiate(this.jungleBook) as Transform;
			book.parent = this.gameObject.transform;
			UI2DSprite sprite = book.GetComponent<UI2DSprite> ();
			sprite.transform.position = new Vector3 (0,0,0);
			sprite.transform.localScale = new Vector3(ratio,ratio,1);
			sprite.SetRect(left, - (sprite.height * ratio)/2, sprite.width * ratio, sprite.height * ratio);

			left += sprite.width * ratio + 10;
		}

		scrollView.ResetPosition ();
	}
}
