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

	void Start ()
	{
		title.guiText.text = "";
		description.guiText.text = "";
	}

	public void Hide (float second) 
	{
		title.FadeOut (second);
		description.FadeOut (second);
	}

	public void Show (float second) 
	{
		title.FadeIn (second);
		description.FadeIn (second);
	}
}
