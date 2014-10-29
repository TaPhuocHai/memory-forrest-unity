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
					// Max charactor in a line
					int maxCharactorInLine = 25;
					// Trim word by space charactor
					string[] stringSeparators = new string[] {" "};
					string[] arrWord = mission.description.text.Split(stringSeparators,System.StringSplitOptions.RemoveEmptyEntries);

					string text = "";
					string desInLine = "";
					// Connet word
					for (int i = 0 ; i < arrWord.Length ; i ++) {
						string str = arrWord[i];
						if (desInLine.Length + str.Length + 1 < maxCharactorInLine) {
							if (desInLine.Length == 0) {
								desInLine = str;
							} else {
								desInLine += " " + str;
							}
						} else {
							text += "\n" + desInLine;
							desInLine = str;
						}
					}
					if (desInLine.Length != 0) {
						text += "\n" + desInLine;
					}
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
	}
}
