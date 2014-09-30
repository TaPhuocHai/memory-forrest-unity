using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		UILabel label = this.GetComponent<UILabel> ();
		label.text = "Point : " + PlayGameData.Instance.score.ToString ();
	}
}
