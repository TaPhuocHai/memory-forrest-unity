using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MissionScrollViewScript : MonoBehaviour 
{	
	public Transform missionPrefab;
	
	void Start () {
		// Region dang choi
		Region region         = Region.Instance (Player.currentRegionPlay);

		UIScrollView scrollView = this.gameObject.GetComponent<UIScrollView> ();

		float top = 0;
		// Draw Mission on screen
		foreach (Mission mission in region.currentMissions) {
			var missionObject = Instantiate(this.missionPrefab) as Transform;
			missionObject.parent = this.gameObject.transform;
			if (missionObject != null) {
				UISprite sprite = missionObject.GetComponent<UISprite> ();
				sprite.transform.position = new Vector3 (0, 0, 0);
				sprite.transform.localScale = new Vector3 (1, 1, 1);
				sprite.SetRect (-sprite.width/2,  top, sprite.width, sprite.height);
				top += sprite.height + 5;

				// Fill data
				MissionScript misisonScript = missionObject.GetComponent<MissionScript> ();
				misisonScript.mission = mission;
			} else {
			}
		}
		scrollView.ResetPosition ();
	}

	void OnGUI () {
	}

	// Update is called once per frame
	void Update () {	
	}
}
