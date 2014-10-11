﻿using UnityEngine;
using System.Collections;

public class PHAnchorRightScreen : PHAnchorMargin 
{
	void Start () 
	{
		if (this.transform == null) {
			return;
		}
		
		float worldPagging = PHScreen.Instance.ConveterPixelToWorld(this.margin);
		Vector2 pos = PHUtility.PositionOfTransformIfPaddingRightBottomScreen (this.transform, new Vector2 (worldPagging, 0));
		this.transform.position = new Vector3 (pos.x, this.transform.position.y, this.transform.position.z);
	}
}