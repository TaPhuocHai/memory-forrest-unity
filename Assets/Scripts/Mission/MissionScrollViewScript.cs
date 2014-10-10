using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionScrollViewScript : MonoBehaviour 
{	
	public Transform missionPrefab;
	
	void Start () {
		// Region dang choi
		Region region         = Region.Instance (Player.currentRegionPlay);
	}

	void OnGUI () {
	}

	// Update is called once per frame
	void Update () {	
	}
}
