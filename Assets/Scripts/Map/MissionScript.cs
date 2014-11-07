using UnityEngine;
using System.Collections;

public class MissionScript : MonoBehaviour 
{
	public TextMesh  title;
	public TextMesh  description;
	public TextMesh  reward;
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
					title.text = PHUtility.FormatStringMultiLine (mission.name.text, 28);
				}
				// Set description
				if (description != null) {
					string text = PHUtility.FormatStringMultiLine (mission.description.text, 32);
					description.text = text;
				}
				// Set reward 
				if (reward != null) {
					string text = PHUtility.FormatStringMultiLine ("+ " + mission.rewardMessage.text, 32);
					reward.text = text;
				}
				Vector3 rewardPosition = new Vector3 ();
				rewardPosition.x = description.transform.position.x;
				rewardPosition.z = description.transform.position.z;
				rewardPosition.y = description.transform.position.y - description.renderer.bounds.size.y/2 - 0.16f;
				// Fix size
				reward.transform.position = rewardPosition;

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
