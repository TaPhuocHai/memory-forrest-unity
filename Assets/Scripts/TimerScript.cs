using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	private float TIME_PLAY = 45.0f;
	private float timerCountDown;

	private bool enableTimer;
	
	void Start () {
		this.timerCountDown = TIME_PLAY;
		this.enableTimer = false;
	}
	
	void Update () {
		if (!this.enableTimer) {
			return;
		}

		this.timerCountDown -= Time.deltaTime;
		if (this.timerCountDown <= 0) {
			this.timerCountDown = 0;

			// Game Over
			Transform gameOver = this.transform.parent.FindChild("GameOver");
			if (gameOver) {
				GameOverScipt gameOverScipt = gameOver.GetComponent<GameOverScipt> ();
				gameOverScipt.EnterGameOver ();
			}

			this.enabled = false;
		}

		GameObject player = GameObject.FindGameObjectWithTag("Player");
		Transform  progressBar = player.transform.FindChild ("ProgressBar");
		UISlider slider = progressBar.GetComponent<UISlider> ();

		if (slider) {
			slider.value = this.timerCountDown / TIME_PLAY;
		} else {
			print ("get progress bar faild");
		}
	}

	public void ResetTimer () {
		this.timerCountDown = TIME_PLAY;
	}

	public void StartTimer () {
		this.enableTimer = true;
	}
}
