using UnityEngine;
using System.Collections;

public class PHLabelAnchorCenterX : PHLabelAnchor 
{
	override public void UpdateAnchor () 
	{
		if (this.anchorObject.transform == null) {
			return;
		}
		
		Vector3 point = Camera.main.WorldToScreenPoint (this.anchorObject.transform.position);
		float x = (float)point.x / Screen.width;

		this.guiText.transform.position = new Vector3 (x, this.guiText.transform.position.y, this.guiText.transform.position.z);
	}
}
