using UnityEngine;
using System.Collections;

public class PHGUI {

	/// <summary>
	/// Button the specified position, text, style and autoScaleScreenRatio.
	/// </summary>
	/// <param name="position">Position.</param>
	/// <param name="text">Text.</param>
	/// <param name="style">Style.</param>
	/// <param name="autoScaleScreenRatio">Auto scale screen ratio.</param>
	public static bool Button (Rect position, string text, GUIStyle style, bool autoScaleScreenRatio)
	{
		Rect drawPosition = position;
		if (autoScaleScreenRatio) {
			drawPosition = new Rect (position.x * PHScreen.Instance.ratioWith, 
			                         position.x * PHScreen.Instance.ratioHeight,
			                         position.width * PHScreen.Instance.optimalRatio, 
			                         position.height * PHScreen.Instance.optimalRatio);
		}
		return GUI.Button (drawPosition, text, style);
	}
}
