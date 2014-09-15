using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {
	
	void Start () {	
	}

	void Update () {	
	}

	public void EnterMap3x2 () {
		PlayerPrefs.SetInt ("MapSizeLoadRow", 2);
		PlayerPrefs.SetInt ("MapSizeLoadCol", 3);
		Application.LoadLevel ("Mission");
	}

	public void EnterMap4x3 () {
		PlayerPrefs.SetInt ("MapSizeLoadRow", 3);
		PlayerPrefs.SetInt ("MapSizeLoadCol", 4);
		Application.LoadLevel ("Mission");
	}

	public void EnterMap4x4 () {
		PlayerPrefs.SetInt ("MapSizeLoadRow", 4);
		PlayerPrefs.SetInt ("MapSizeLoadCol", 4);
		Application.LoadLevel ("Mission");
	}

	public void EnterMap5x5 () {
		PlayerPrefs.SetInt ("MapSizeLoadRow", 4);
		PlayerPrefs.SetInt ("MapSizeLoadCol", 5);
		Application.LoadLevel ("Mission");
	}

	public void EnterMap6x6 () {
		PlayerPrefs.SetInt ("MapSizeLoadRow", 5);
		PlayerPrefs.SetInt ("MapSizeLoadCol", 6);
		Application.LoadLevel ("Mission");
	}
}
