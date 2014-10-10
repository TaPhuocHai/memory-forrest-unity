using UnityEngine;
using System.Collections;

public class TimerManager : PHTimer 
{
	static private TimerManager _timerManager;
	static public TimerManager Instance
	{
		get { return _timerManager; }
	}

	void Awake () 
	{
		Debug.Log ("init timer");
		_timerManager = this;
	}

	override protected void Fire ()
	{
		// Save total score
		Player.totalScore = Player.totalScore + PlayGameData.Instance.score;
		
		// Luu thong tin man choi user da choi
		PlayGameData.Instance.Save ();
		
//		// Game Over
//		Transform gameOver = GameObject.FindGameObjectWithTag ("GameOver");
//		if (gameOver) {
//			GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
//			gameOverScipt.EnterGameOver ();
//		}	
	}
}
