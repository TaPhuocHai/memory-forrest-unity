using UnityEngine;
using System.Collections;

public class Constant 
{
	/// <summary>
	/// Debug mode : su dung de chung thuc 1 so ham chi duoc phep chay o che do debug mode
	/// </summary>
	public static bool kDebugMode = true;

	/// <summary>
	/// Gia tri nay = true thi co the goi ham de set task mission complete
	/// </summary>
	public static bool  kDebugMission = false;

	/// <summary>
	/// Time play default.
	/// </summary>
	public static int    kTimePlayDefault  = 45;

	/// <summary>
	/// max current misison at the time.
	/// </summary>
	public static int    kMaxCurrentMisison = 3;

	public static int    kCardGreenBackSpriteIndex = 21;

	/// <summary>
	/// Thoi gian hien thi popup
	/// </summary>
	public static float  kPopupAnimationDuraction = 0.4f;

	/// <summary>
	/// Thoi gian duoc cong them khi user su dung Rescue
	/// </summary>
	public static int  kRescueAddTime = 15;

	static public string kAdBannerUnitId = "ca-app-pub-5151220756481063/6286731830";
	static public string kAdInterstitialId = "ca-app-pub-5151220756481063/7763465032";
}
