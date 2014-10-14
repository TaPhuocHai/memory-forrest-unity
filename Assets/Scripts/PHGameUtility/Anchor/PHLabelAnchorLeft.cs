using UnityEngine;
using System.Collections;

public class PHLabelAnchorLeft : PHLabelAnchor 
{
	override public void UpdateAnchor () 
	{
		if (this.anchorObject.transform == null) {
			return;
		}

		// Get size object
		Vector3 sizeObject;
		BoxCollider boxCollider = this.anchorObject.GetComponent<BoxCollider> ();
		// Uu tien su dung kich thuoc cua boxCollider
		if (boxCollider != null) {
			sizeObject = new Vector3(boxCollider.size.x * this.anchorObject.localScale.x, boxCollider.size.y * this.anchorObject.localScale.y, 1);			 
		} else {
			if (this.anchorObject.renderer != null) {
				sizeObject = this.anchorObject.renderer.bounds.size;
			} else {
				// return if can't get size object
				return;
			}
		}

		Vector3 leftPosition = new Vector3 ();
		leftPosition.x = this.anchorObject.transform.position.x - sizeObject.x/2;
		leftPosition.y = this.anchorObject.transform.position.y;
		leftPosition.z = this.anchorObject.transform.position.z;

		Vector3 point = Camera.main.WorldToScreenPoint (leftPosition);
		float x = (float)point.x / Screen.width;

		Debug.Log ("x = " + x.ToString ());
		
		this.guiText.transform.position = new Vector3 (x, this.guiText.transform.position.y, this.guiText.transform.position.z);
	}
}
