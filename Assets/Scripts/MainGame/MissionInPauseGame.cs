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
					string thumbnailPath = null;
					if (_mission.missionReward.GetType() == typeof(AdditionPointReward)) {
						thumbnailPath = "Textures/Reward/MorePointReward";
					} else if (_mission.missionReward.GetType() == typeof(CoinReward)) {
						thumbnailPath = "Textures/Reward/CoinReward";
					} else if (_mission.missionReward.GetType() == typeof(MoreTimeReward)) {
						thumbnailPath = "Textures/Reward/MoreTimeReward";
					} else if (_mission.missionReward.GetType() == typeof(UnlockCardReward)) {
						thumbnailPath = "Textures/Reward/UnlockCardReward";
					} else if (_mission.missionReward.GetType() == typeof(UnlockExtraRoundReward)) {
						thumbnailPath = "Textures/Reward/NewRoundReward";
					}
					Sprite thumbnailSprite = (Sprite)Resources.Load(thumbnailPath,typeof(Sprite));
					SpriteRenderer spriteRender = thumbnail.GetComponent<SpriteRenderer>();
					spriteRender.sprite = thumbnailSprite;
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
