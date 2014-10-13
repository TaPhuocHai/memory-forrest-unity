using UnityEngine;
using System.Collections;

public class PHLabel : MonoBehaviour 
{
	// Bien nay canh trai object
	public Transform anchorObject;

	void Start ()
	{
		this.guiText.fontSize = (int)(this.guiText.fontSize * PHScreen.Instance.optimalRatio);
	}

	public void UpdateAnchor () 
	{
		if (this.anchorObject.transform == null) {
			return;
		}

		Vector3 point = Camera.main.WorldToScreenPoint (this.anchorObject.transform.position);

		float x = (float)point.x / Screen.width;
		float y = (float)point.y / Screen.height - 0.01f;

		this.guiText.transform.position = new Vector3 (x, y, 0);
	}
}
