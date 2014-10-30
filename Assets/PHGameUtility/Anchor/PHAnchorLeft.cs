using UnityEngine;
using System.Collections;

public class PHAnchorLeft : PHAnchorMargin
{
	override public void UpdateAnchor () 
	{
		if (this.transform == null) {
			return;
		}
		if (this.anchorObject == null) {
			return;
		}
		
		float worldPagging = PHScreen.Instance.ConveterPixelToWorld(this.margin);
		Vector2 pos = PHUtility.PositionOfTransformIfPaddingLeftTopWithTransform (this.transform, new Vector2 (worldPagging, 0), this.anchorObject);
		this.transform.position = new Vector3 (pos.x, this.transform.position.y, this.transform.position.z);
	}
}
