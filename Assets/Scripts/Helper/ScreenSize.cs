using UnityEngine;
using System.Collections;

public class ScreenSize : MonoBehaviour 
{
	void Start () {
		Debug.Log ("Screen size: " + Screen.width.ToString () + " : " + Screen.height.ToString ());

		// Top,left, boom, right cua visiable screen so voi camera
		Vector2 edgeTopRightVector  = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
		Vector2 edgeLeftBottomVector = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
		// width, height cua visiable screen so voi camera
		float screenWidth  = edgeTopRightVector.x * 2;
		float screenHeight = edgeTopRightVector.y * 2;

		Debug.Log("Screen unit: " + screenWidth.ToString () + " : " + screenHeight.ToString());
	}
}
