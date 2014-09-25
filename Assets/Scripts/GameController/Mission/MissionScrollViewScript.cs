using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionScrollViewScript : MonoBehaviour {

	public Transform missionPrefab;
	public Transform cardPrefab;

	// Vung dat user dang choi
	private Region     _region;
	
	void Start () {
		// Init Region
		RegionType regionType = (RegionType)PlayerPrefs.GetInt (Constant.kPlayGameInRegion);
		this._region = new Region (regionType);

		UIScrollView scrollView = this.gameObject.GetComponent<UIScrollView> ();

		List<Mission> list = this._region.missions;
		print ("co mission : " + list.Count);

		// Draw Mission on screen
		foreach (Mission mission in this._region.missions) {
			var missionObject = Instantiate(this.missionPrefab) as Transform;
			missionObject.parent = this.gameObject.transform;
			if (missionObject != null) {
				print ("tao mission susccess");
			} else {
				print ("tao mission faild");
			}
		}
	}

	void OnGUI () {
	}

	// Update is called once per frame
	void Update () {	
	}
}
