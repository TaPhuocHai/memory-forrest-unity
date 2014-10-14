using UnityEngine;
using System.Collections;

public class PHLabelAnchorLeft : PHLabelAnchorMargin 
{
	override public void UpdateAnchor () 
	{
		if (this.anchorObject == null) {
			return;
		}

		// Get size anchorObject
		Vector3 sizeObject = PHUtility.GetSizeOfTransforum (this.anchorObject);

		// left position of anchorObject
		Vector3 leftPosition = new Vector3 ();
		leftPosition.x = this.anchorObject.transform.position.x - sizeObject.x/2;
		leftPosition.y = this.anchorObject.transform.position.y;
		leftPosition.z = this.anchorObject.transform.position.z;

		Rect sizeText = this.guiText.GetScreenRect ();
		float withTextOnWorld = sizeText.width * PHUtility.WorldWidthOfPixel;
		
		// Vi tri trai nhat cua text so voi anchorObject
		leftPosition.x += withTextOnWorld/2;

		// Add padding
		leftPosition.x += PHScreen.Instance.ConveterPixelToWorldWidthRatioByWidth (this.margin);

		// Conveter to screen point
		Vector3 point = Camera.main.WorldToScreenPoint (leftPosition);

		float x = (float)point.x / Screen.width;		
		this.guiText.transform.position = new Vector3 (x, this.guiText.transform.position.y, this.guiText.transform.position.z);
	}
}
