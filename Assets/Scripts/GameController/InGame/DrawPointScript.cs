using UnityEngine;
using System.Collections;

public class DrawPointScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UILabel label = this.GetComponent<UILabel> ();
		if (PlayGameData.currentPlayGameData != null) {
			label.text = "Point : " + PlayGameData.currentPlayGameData.score.ToString ();
		} else {
			label.text = "Point : 0";
		}

	}
}
