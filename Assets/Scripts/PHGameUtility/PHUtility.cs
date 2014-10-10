using UnityEngine;
using System.Collections;

public class PHUtility 
{
	static private float _worldWidth;
	static private float _worldHeght;

	#region Static Properties

	/// <summary>
	/// Chieu rong cua the gioi thuc
	/// </summary>
	/// <returns>The width.</returns>
	public static float WorldWidth
	{
		get {
			if (PHUtility._worldWidth == 0) {
				Vector2 edgeTopRightVector  = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
				// width cua visiable screen so voi camera
				PHUtility._worldWidth  = edgeTopRightVector.x * 2;
			}
			return PHUtility._worldWidth;
		}
	}

	/// <summary>
	/// Chieu cao cua the gioi thuc
	/// </summary>
	/// <returns>The height.</returns>
	public static float WorldHeight
	{
		get {
			if (PHUtility._worldHeght == 0) {
				Vector2 edgeTopRightVector  = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
				// height cua visiable screen so voi camera
				PHUtility._worldHeght = edgeTopRightVector.y * 2;
			}
			return PHUtility._worldHeght;
		}
	}

	/// <summary>
	/// Chieu rong cua the gioi thuc theo 1 pixel
	/// 1 Pixel nay duoc tinh theo kich thuoc that cua man hinh
	/// </summary>
	/// <returns>The width of pixel.</returns>
	public static float WorldWidthOfPixel
	{
		get {
			return (1 * PHUtility.WorldWidth) / Screen.width;
		}
	}

	#endregion

	public static Vector2 PositionOfTransformIfPaddingTopLeftScreen (Transform transform, Vector2 topLeft)
	{
		if (transform.renderer == null) {
			return new Vector2 (0,0);
		}						
		Vector3 currentPos = transform.position;
		Vector2 size = transform.renderer.bounds.size;

		float leftScreenOfTransform = - PHUtility.WorldWidth / 2 + size.x/2;
		float topScreenOfTransform  = PHUtility.WorldHeight / 2 - size.y/2;

		return new Vector2 (leftScreenOfTransform + topLeft.x , topScreenOfTransform - topLeft.y);
	}

	public static Vector2 PositionOfTransformIfPaddingBottomRightScreen (Transform transform, Vector2 bottomLeft)
	{
		if (transform.renderer == null) {
			return new Vector2 (0,0);
		}						
		Vector3 currentPos = transform.position;
		Vector2 size = transform.renderer.bounds.size;
		
		float newX = currentPos.x + (PHUtility.WorldWidth / 2 - bottomLeft.x - size.x / 2);
		float newY = currentPos.y - (PHUtility.WorldHeight / 2 - bottomLeft.y - size.y / 2);
		return new Vector2 (newX, newY);
	}
}
