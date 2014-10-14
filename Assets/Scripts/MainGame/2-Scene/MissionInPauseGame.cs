using UnityEngine;
using System.Collections;

public class MissionInPauseGame : MonoBehaviour 
{
	public PHLabel title;
	public PHLabel description;

	private Mission _mission;
	public Mission mission {
		get {
			return _mission;
		}
		set {
			_mission = value;
			if (_mission != null) {
				if (title != null) {
					title.guiText.text = mission.name.text;
				}
				if (description != null) {
					description.guiText.text = mission.description.text;
				}
			}
		}
	}

	public void Hide () 
	{
		title.alpha = 0;
		description.alpha = 0;
	}

	public void Show ()
	{
		title.alpha = 1;
		description.alpha = 1;
	}
}
