using UnityEngine;
using System.Collections;

public class PHAnchorCenterY : PHAnchor
{
	public Transform anchorObject;
	
	override public void UpdateAnchor () 
	{
		if (this.transform == null) {
			return;
		}
		if (this.anchorObject == null) {
			return;
		}
		this.transform.position = new Vector3 (this.transform.position.x,this.anchorObject.renderer.bounds.center.y, this.transform.position.z);
	}
}

