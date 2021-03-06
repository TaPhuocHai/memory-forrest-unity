using UnityEngine;
using System.Collections;

public class MissionInPauseGame : MonoBehaviour 
{
	public TextMesh  title;
	public TextMesh  description;
	public Transform thumbnail;

	private Mission _mission;
	public Mission mission {
		get {
			return _mission;
		}
		set {
			_mission = value;
			if (_mission != null) {
				// Set title
				if (title != null) {
					title.text = mission.name.text;
				}
				// Set description
				if (description != null) {
					string text = PHUtility.FormatStringMultiLine (mission.description.text, 25);
					description.text = text;
				}

				// Set thumbnail
				if (thumbnail != null) {
					SpriteRenderer spriteRender = thumbnail.GetComponent<SpriteRenderer>();
					spriteRender.sprite = _mission.thumbnail;
				}
			}
		}
	}

	void Start ()
	{
		title.text = "";
		description.text = "";
		SpriteRenderer spriteRender = thumbnail.GetComponent<SpriteRenderer>();
		spriteRender.sprite = null;
	}
}
