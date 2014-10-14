using UnityEngine;
using System.Collections;

public class PHLabelAnchorBottom : PHLabelAnchorMargin 
{
	override public void UpdateAnchor () 
	{
		if (this.anchorObject == null) {
			return;
		}
		
		// Get size anchorObject
		Vector3 sizeObject = PHUtility.GetSizeOfTransforum (this.anchorObject);
		
		// left position of anchorObject
		Vector3 bottomPosition = new Vector3 ();
		bottomPosition.x = this.anchorObject.transform.position.x;
		bottomPosition.y = this.anchorObject.transform.position.y - sizeObject.y/2;
		bottomPosition.z = this.anchorObject.transform.position.z;
		
		Rect sizeText = this.guiText.GetScreenRect ();
		float heightTextOnWorld = sizeText.height * PHUtility.WorldWidthOfPixel;
		
		// Vi tri trai nhat cua text so voi anchorObject
		bottomPosition.y += heightTextOnWorld/2;

		// Add padding
		bottomPosition.y += PHScreen.Instance.ConveterPixelToWorldWidthRatioByHeight (this.margin);

		// Conveter to screen point
		Vector3 point = Camera.main.WorldToScreenPoint (bottomPosition);
		
		float y = (float)point.y / Screen.height;		
		this.guiText.transform.position = new Vector3 (this.guiText.transform.position.x, y, this.guiText.transform.position.z);
	}
}
