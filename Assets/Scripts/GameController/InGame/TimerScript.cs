using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	static private float TIME_PLAY = 45.0f;
	static private float timerCountDown;

	static private bool enableTimer;

	void Start () {
		TimerScript.timerCountDown = TIME_PLAY;
		TimerScript.enableTimer = false;
	}
	
	void Update () {
		if (!TimerScript.enableTimer) {
			return;
		}

		TimerScript.timerCountDown -= Time.deltaTime;
		if (TimerScript.timerCountDown <= 0) {
			TimerScript.timerCountDown = 0;

			// Game Over
			Transform gameOver = this.transform.parent.FindChild("GameOver");
			if (gameOver) {
				GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
				gameOverScipt.EnterGameOver ();
			}
		}

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Transform  progressBar = player.transform.FindChild ("ProgressBar");
		UISlider slider = progressBar.GetComponent<UISlider> ();

		if (slider) {
			slider.value = TimerScript.timerCountDown / TimerScript.TIME_PLAY;
		} else {
			print ("get progress bar faild");
		}
	}

	static public void ResetTimer () {
		TimerScript.timerCountDown = TimerScript.TIME_PLAY;
	}

	static public void AddMoreTime (float addmore) {
		TimerScript.timerCountDown += addmore;
	}

	static public void StartTimer () {
		TimerScript.enableTimer = true;
		print("call start timer");
	}
}
