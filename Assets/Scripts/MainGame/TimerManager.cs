using UnityEngine;
using System.Collections;

public class TimerManager : PHTimer 
{
	static private TimerManager _timerManager;
	static public TimerManager Instance
	{
		get { return _timerManager; }
	}

	public TimerManager ()
	{
		_timerManager = this;
	}

	override protected void Fire ()
	{
		// Save total score
		Player.totalScore = Player.totalScore + PlayGameData.Instance.score;
		
		// Luu thong tin man choi user da choi
		PlayGameData.Instance.Save ();

		// show rescue popup
		RescuePopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}
}
