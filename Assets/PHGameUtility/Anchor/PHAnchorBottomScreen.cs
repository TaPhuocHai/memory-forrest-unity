using UnityEngine;
using System.Collections;

public class PHAnchorBottomScreen : PHAnchorScreen
{
	override public void UpdateAnchor () 
	{
		if (this.transform == null) {
			return;
		}
		
		float worldPagging = PHScreen.Instance.ConveterPixelToWorld(this.margin);
		Vector2 pos = PHUtility.PositionOfTransformIfPaddingRightBottomScreen (this.transform, new Vector2 (0, worldPagging));
		this.transform.position = new Vector3 (this.transform.position.x, pos.y, this.transform.position.z);
	}
}
