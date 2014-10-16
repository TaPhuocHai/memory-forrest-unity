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
	
	/// <summary>
	/// Chieu cao cua the gioi thuc theo 1 pixel
	/// 1 Pixel nay duoc tinh theo kich thuoc that cua man hinh
	/// </summary>
	/// <value>The world height of pixel.</value>
	public static float WorldHeightOfPixel
	{
		get {
			return (1 * PHUtility.WorldHeight) / Screen.height;
		}
	}

	#endregion

	#region Padding helper

	/// <summary>
	/// Tinh toan vi tri cua transform neu no cach top lef cua man hinh
	/// </summary>
	/// <returns>The of transform if padding top left screen.</returns>
	/// <param name="transform">Transform.</param>
	/// <param name="topLeft">Top left.</param>
	public static Vector2 PositionOfTransformIfPaddingTopLeftScreen (Transform transform, Vector2 topLeft)
	{
		if (transform.renderer == null) {
			return new Vector2 (0,0);
		}						
		Vector2 size = transform.renderer.bounds.size;

		float leftScreenOfTransform = - PHUtility.WorldWidth / 2 + size.x/2;
		float topScreenOfTransform  = PHUtility.WorldHeight / 2 - size.y/2;

		return new Vector2 (leftScreenOfTransform + topLeft.x , topScreenOfTransform - topLeft.y);
	}

	/// <summary>
	/// Tinh toan vi tri cua transform neu no cach vi tri top left so voi 1 tranform khac
	/// </summary>
	/// <returns>The of transform if padding top left with transform.</returns>
	/// <param name="transform">Transform.</param>
	/// <param name="topLeft">Top left.</param>
	/// <param name="toTranform">To tranform.</param>
	public static Vector2 PositionOfTransformIfPaddingTopLeftWithTransform (Transform transform, Vector2 topLeft, Transform toTranform)
	{
		if (transform.renderer == null || toTranform.renderer == null) {
			return new Vector2 (0,0);
		}						
		Vector2 size       = transform.renderer.bounds.size;
		
		float leftOfTransform = - toTranform.renderer.bounds.size.x / 2 + size.x/2;
		float topOfTransform  = toTranform.renderer.bounds.size.y / 2 - size.y/2;
		
		return new Vector2 (leftOfTransform + topLeft.x , topOfTransform - topLeft.y);
	}

	/// <summary>
	/// Tinh toan vi tri cua transform neu no cach vi tri bottom right cua man hinh
	/// </summary>
	/// <returns>The of transform if padding right bottom screen.</returns>
	/// <param name="transform">Transform.</param>
	/// <param name="rightBottom">Right bottom.</param>
	public static Vector2 PositionOfTransformIfPaddingRightBottomScreen (Transform transform, Vector2 rightBottom)
	{
		if (transform.renderer == null) {
			return new Vector2 (0,0);
		}						
		Vector2 size = transform.renderer.bounds.size;
		
		float rightScreenOfTransform = PHUtility.WorldWidth / 2 - size.x/2;
		float bottomScreenOfTransform   = - PHUtility.WorldHeight / 2 + size.y/2;
		
		return new Vector2 (rightScreenOfTransform - rightBottom.x , bottomScreenOfTransform + rightBottom.y);
	}

	#endregion

	/// <summary>
	/// Gets the size of transforum.
	/// </summary>
	/// <returns>The size of transforum.</returns>
	/// <param name="transform">Transform.</param>
	public static Vector3 GetSizeOfTransforum (Transform transform)
	{
		Vector3 sizeObject = new Vector3 ();

		if (transform == null) {
			return sizeObject;
		}

		BoxCollider boxCollider = transform.GetComponent<BoxCollider> ();
		// Uu tien su dung kich thuoc cua boxCollider
		if (boxCollider != null) {
			sizeObject = new Vector3(boxCollider.size.x * transform.localScale.x, boxCollider.size.y * transform.localScale.y, 1);			 
		} else {
			if (transform.renderer != null) {
				sizeObject = transform.renderer.bounds.size;
			}
		}
		return sizeObject;
	}
}
