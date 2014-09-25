using UnityEngine;
using System.Collections;

public class MissionScript : MonoBehaviour {

	void Awake () 
	{
		Card.Initialize ();
		Region.Initialize ();
	}
	
	void Start () {
	
	}

	void Update () {
	
	}

	public void EnterGame () {
		Application.LoadLevel ("Main");
	}
}
