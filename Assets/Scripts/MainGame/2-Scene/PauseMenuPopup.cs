using UnityEngine;
using System.Collections;

public class PauseMenuPopup : PHPopup 
{
	//private static PauseMenuPopup _instance;
	public static PauseMenuPopup Instance { get; private set;}

	void Awake () 
	{
		PauseMenuPopup.Instance = this;
	}

	// Use this for initialization
	void Start () {
		this.Init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
