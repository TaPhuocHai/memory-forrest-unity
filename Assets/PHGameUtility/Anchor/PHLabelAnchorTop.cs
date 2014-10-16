using UnityEngine;
using System.Collections;

public class PHLabelAnchorTop : PHLabelAnchorMargin 
{
	override public void UpdateAnchor () 
	{
		if (this.anchorObject == null) {
			return;
		}

		// Get size anchorObject
		Vector3 sizeObject = PHUtility.GetSizeOfTransforum (this.anchorObject);
		
		// left position of anchorObject
		Vector3 topPosition = new Vector3 ();
		topPosition.x = this.anchorObject.transform.position.x;
		topPosition.y = this.anchorObject.transform.position.y + sizeObject.y/2;
		topPosition.z = this.anchorObject.transform.position.z;

		//Debug.Log ("anchor = " + topPosition.y.ToString());

		Rect sizeText = this.guiText.GetScreenRect ();
		float heightTextOnWorld = sizeText.height * PHUtility.WorldWidthOfPixel;
		
		// Vi tri trai nhat cua text so voi anchorObject
		topPosition.y -= heightTextOnWorld/2;

		// Add padding
		topPosition.y -= PHScreen.Instance.ConveterPixelToWorldWidthRatioByHeight (this.margin);

		Vector3 pos = Camera.main.WorldToViewportPoint (topPosition);
		this.guiText.transform.position = pos;
	}
}
