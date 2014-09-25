using UnityEngine;
using System.Collections;

public class PlayerData : ScriptableObject 
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
}
