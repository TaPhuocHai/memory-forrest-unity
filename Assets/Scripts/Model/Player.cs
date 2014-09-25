using UnityEngine;
using System.Collections;

public class Player
{
	public static int Coin {
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_COIN",0);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_COIN", value);
			PlayerPrefs.Save();
		}
	}
	public static int secondTimePlay {
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_TIME",Constant.kTimePlayDefault);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_TIME", value);
			PlayerPrefs.Save();
		}
	}
	public static int lastScore {
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_LAST_SCORE",0);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_LAST_SCORE", value);
			PlayerPrefs.Save();
		}
	}
	public static int totalScore {
		get {
			return PlayerPrefs.GetInt("PLAYER_DATA_TOTAL_SCORE",0);
		}
		set {
			PlayerPrefs.SetInt("PLAYER_DATA_TOTAL_SCORE", value);
			PlayerPrefs.Save();
		}
	}
}
