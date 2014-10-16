using UnityEngine;
using System.Collections;

public class MissionInPauseGame : MonoBehaviour 
{
	public TextMesh title;
	public TextMesh description;

	private Mission _mission;
	public Mission mission {
		get {
			return _mission;
		}
		set {
			_mission = value;
			if (_mission != null) {
				if (title != null) {
					title.text = mission.name.text;
				}
				if (description != null) {
					description.text = mission.description.text;
				}
			}
		}
	}

	void Start ()
	{
		title.text = "";
		description.text = "";
	}
}
