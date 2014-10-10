using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	static private float TIME_PLAY = 45.0f;
	static private float timerCountDown;

	static private bool enableTimer;

	void Start () 
	{
		TimerScript.TIME_PLAY = Player.secondTimePlay;
		TimerScript.timerCountDown = TIME_PLAY;
		TimerScript.enableTimer = false;
	}
	
	void Update () 
	{
		if (!TimerScript.enableTimer) {
			return;
		}

		TimerScript.timerCountDown -= Time.deltaTime;
		if (TimerScript.timerCountDown <= 0) {
			TimerScript.timerCountDown = 0;

			// Save total score
			Player.totalScore = Player.totalScore + PlayGameData.Instance.score;

			// Luu thong tin man choi user da choi
			PlayGameData.Instance.Save ();

			// Game Over
			Transform gameOver = this.transform.parent.FindChild("GameOver");
			if (gameOver) {
				GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
				gameOverScipt.EnterGameOver ();
			}

			TimerScript.enableTimer = false;
		}
	}

	static public void ResetTimer () 
	{
		TimerScript.TIME_PLAY = (float)Player.secondTimePlay;
		TimerScript.timerCountDown = TimerScript.TIME_PLAY;
	}

	static public void AddMoreTime (float addmore) 
	{
		TimerScript.timerCountDown += addmore;
	}

	static public void StartTimer () 
	{
		TimerScript.TIME_PLAY = (float)Player.secondTimePlay;
		TimerScript.enableTimer = true;
	}

	static public void StopTimer () 
	{
		TimerScript.enableTimer = false;
	}
}
