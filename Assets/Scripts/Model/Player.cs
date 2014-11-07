using UnityEngine;
using System.Collections;

public class Player
{
	/// <summary>
	/// Gets or sets the coin.
	/// </summary>
	/// <value>The coin.</value>
	public static int Coin 
	{
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_COIN",110);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_COIN", value);
			PlayerPrefs.Save();
		}
	}

	/// <summary>
	/// Gets or sets the second time play.
	/// </summary>
	/// <value>The second time play.</value>
	public static int secondTimePlay 
	{
		get {
			//return 4;
			return PlayerPrefs.GetInt("PLAYER_DATA_TIME",Constant.kTimePlayDefault);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_TIME", value);
			PlayerPrefs.Save();
		}
	}

	/// <summary>
	/// Gets or sets the last score.
	/// </summary>
	/// <value>The last score.</value>
	public static int lastScore {
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_LAST_SCORE",0);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_LAST_SCORE", value);
			PlayerPrefs.Save();
		}
	}

	/// <summary>
	/// Gets or sets the best score.
	/// </summary>
	/// <value>The last score.</value>
	public static int bestScore {
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_BEST_SCORE",0);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_BEST_SCORE", value);
			PlayerPrefs.Save();
		}
	}

	/// <summary>
	/// Gets or sets the total score.
	/// </summary>
	/// <value>The total score.</value>
	public static int totalScore {
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_TOTAL_SCORE",0);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_TOTAL_SCORE", value);
			PlayerPrefs.Save();
		}
	}

	/// <summary>
	/// Gets or sets the current region play.
	/// </summary>
	/// <value>The current region play.</value>
	public static RegionType currentRegionPlay 
	{
		get {
			return (RegionType)PlayerPrefs.GetInt("PLAYER_DATA_CURREN_REGION",0);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_CURREN_REGION", (int)value);
			PlayerPrefs.Save();
		}
	}

	public static void ResetData ()
	{
		Player.Coin = 110;
		Player.secondTimePlay = Constant.kTimePlayDefault;
		Player.lastScore = 0;
		Player.totalScore = 0;
		Player.bestScore = 0;
	}
}
