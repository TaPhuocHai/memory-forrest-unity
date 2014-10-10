using UnityEngine;
using System.Collections;

public class GUIPauseMenuButton : MonoBehaviour {

	public GUIStyle style;

	void OnGUI () 
	{
		if (PHGUI.Button(new Rect(10,10,this.style.normal.background.width,this.style.normal.background.height),"",style, true)) {
			Debug.Log ("pause menu");
		}
	}
}
