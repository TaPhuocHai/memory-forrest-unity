using UnityEngine;
using System.Collections;

public class PHAnchorCenterX : PHAnchorObject
{
	override public void UpdateAnchor () 
	{
		if (this.transform == null) {
			return;
		}
		if (this.anchorObject == null) {
			return;
		}

		this.transform.position = new Vector3 (this.anchorObject.renderer.bounds.center.x, this.transform.position.y, this.transform.position.z);
	}
}
