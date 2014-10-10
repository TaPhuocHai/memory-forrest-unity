using UnityEngine;
using System.Collections;

/// <summary>
/// Lop nay dinh nghia width, height mac dinh cua game duoc thiet ke
/// Tuy nhien de support cho nhieu man hinh, voi nhieu kich thuoc va ti le khac nhau.
/// Vi vay khi ve 1 so control co nhu cau phai tinh lai with, height dua tren ti le thuc te cua man hinh
/// </summary>
public class PHScreen : MonoBehaviour {

	/// <summary>
	/// Chieu cao width mac dinh
	/// </summary>
	public float defaultWidth  = 1080;
	public float defaultHeight = 1920; 

	public void Awake ()
	{
		_instance = this;
		Debug.Log ("PHScreen : " + defaultWidth.ToString () + " : " + defaultHeight.ToString ());
		Debug.Log ("Real Screen : " + Screen.width.ToString() + " : " + Screen.height.ToString());
	}

	#region Instance

	static PHScreen _instance;
	public static PHScreen Instance {
		get {
			return _instance;
		}
	}

	#endregion

	#region Properties

	private float _ratioWith;
	public float ratioWith 
	{
		get {
			if (_ratioWith == 0) {
				_ratioWith = Screen.width/defaultWidth;
			}
			return _ratioWith;
		}
	}

	private float _ratioHeight;
	public float ratioHeight 
	{
		get {
			if (_ratioHeight == 0) {
				_ratioHeight = Screen.height/defaultHeight;
			}
			return _ratioHeight;
		}
	}

	private float _optimalRatio;
	public float optimalRatio
	{
		get {
			if (_optimalRatio == 0) {
				if (this.ratioWith > this.ratioHeight) {
					_optimalRatio = this.ratioHeight;
				} else {
					_optimalRatio = this.ratioWith;
				}
			}
			return _optimalRatio;
		}
	}

	#endregion

	/// <summary>
	/// Chuyen doi gia tri cua pixel theo the goi thuc
	/// Pixel dua vao ti le default cua PHScreen
	/// Ham nay se su dung optimalRatio de chuyen doi
	/// </summary>
	/// <returns>The pixel to world.</returns>
	/// <param name="pixel">Pixel : pixel dua vao tinh the ti le default screen</param>
	public float ConveterPixelToWorld (int pixel)
	{
		// Tinh gia tri pixel theo man hinh thuc te
		float realPixel = pixel * this.optimalRatio;

		// Nhan voi chieu rong the gioi thuc cua 1 pixel
		return realPixel * PHUtility.WorldWidthOfPixel;
	}

	/// <summary>
	/// Conveters the width of the pixel to world width ratio by.
	/// Tinh width theo the gioi thuc. Ti le chuyen doi tinh theo chieu ngang
	/// </summary>
	/// <returns>The pixel to world width ratio by width.</returns>
	/// <param name="pixel">Pixel</param>
	public float ConveterPixelToWorldWidthRatioByWidth (int pixel)
	{
		float realPixel = pixel * this.ratioWith;
		return realPixel * PHUtility.WorldWidthOfPixel;
	}
}
