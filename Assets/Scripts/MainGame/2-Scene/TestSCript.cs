using UnityEngine;
using System.Collections;

public class TestSCript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Vector3 point = Camera.main.WorldToScreenPoint (this.transform.position);

		Vector3 point = Camera.main.WorldToScreenPoint (new Vector3 (this.transform.position.x - this.transform.renderer.bounds.size.x/2,
		                                                             this.transform.position.y + this.transform.renderer.bounds.size.y/2,0));


	}
}
