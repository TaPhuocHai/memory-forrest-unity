using UnityEngine;
using System.Collections;

public class PHAnchorLeftScreen : PHAnchorMargin 
{
	override public void UpdateAnchor () 
	{
		if (this.transform == null) {
			return;
		}
		
		float worldPagging = PHScreen.Instance.ConveterPixelToWorld(this.margin);
		Vector2 pos = PHUtility.PositionOfTransformIfPaddingTopLeftScreen (this.transform, new Vector2 (worldPagging, 0));
		this.transform.position = new Vector3 (pos.x, this.transform.position.y, this.transform.position.z);
	}
}
