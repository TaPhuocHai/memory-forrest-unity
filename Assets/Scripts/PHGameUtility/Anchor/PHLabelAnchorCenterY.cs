﻿using UnityEngine;
using System.Collections;

public class PHLabelAnchorCenterY : PHLabelAnchorObject 
{
	override public void UpdateAnchor () 
	{
		if (this.anchorObject == null) {
			return;
		}
		
		Vector3 point = Camera.main.WorldToScreenPoint (this.anchorObject.transform.position);
		float y = (float)point.y / Screen.height;
		
		this.guiText.transform.position = new Vector3 (this.guiText.transform.position.x, y, this.guiText.transform.position.z);
	}
}