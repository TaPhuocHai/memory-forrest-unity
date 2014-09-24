using UnityEngine;
using System.Collections;

public class MapScript : MonoBehaviour {
	
	void Start () {	
	}

	void Update () {	
	}

	public void EnterMap4x4 () {
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.KingdomOfRabbits);
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMap5x5 () {	
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.Forest);
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMap5x5_2 () {
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.StoneMountain);
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}

	public void EnterMap6x6 () {
		PlayerPrefs.SetInt (Constant.kPlayGameInRegion, (int)RegionType.WolfCamp);
		PlayerPrefs.Save ();
		Application.LoadLevel ("Mission");
	}
}
