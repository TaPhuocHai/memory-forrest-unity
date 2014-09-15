using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GameOverScipt : MonoBehaviour {

	// Use this for initialization
	void Start () {

		// Top,left, boom, right cua visiable screen so voi camera
		Vector2 edgeTopRightVector  = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		Vector2 edgeLeftBottomVector = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		// width, height cua visiable screen so voi camera
		float screenWidth  = edgeTopRightVector.x * 2;
		float screenHeight = edgeTopRightVector.y * 2;

		UIPanel panel = this.GetComponent<UIPanel> ();
		Bounds bounds = NGUIMath.CalculateAbsoluteWidgetBounds(panel.gameObject.transform);

		float posY = 2;

		// Move to top
//		TweenParms parms = new TweenParms().Prop("position", new Vector3(this.transform.position.x,posY,0)).Ease(EaseType.EaseOutBack);
//		HOTween.To (this, 0.5f, parms);
		this.transform.position = new Vector3 (this.transform.position.x, posY,0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EnterGameOver () {
		TweenParms parms = new TweenParms().Prop("position", new Vector3(this.transform.position.x,0,0)).Ease(EaseType.EaseOutBack);
		HOTween.To (this.transform, 0.65f, parms);
	}
}
