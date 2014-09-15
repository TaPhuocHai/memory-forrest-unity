using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour {

	private float TIME_PLAY = 45.0f;
	private float timerCountDown;

	private bool enableTimer;
	
	void Start () {
		this.enableTimer = false;
	}
	
	void Update () {
		if (!this.enableTimer) {
			return;
		}

		this.timerCountDown -= Time.deltaTime;
		if (this.timerCountDown <= 0) {
			this.timerCountDown = 0;
			this.enabled = false;
		}

		GameObject root = GameObject.FindGameObjectWithTag("UIRoot");
		Transform progressBar = root.transform.FindChild ("ProgressBar");
		UISlider slider = progressBar.GetComponent<UISlider> ();
		//UIProgressBar pg = root.transform.FindChild ("ProgressBar");
		//UIProgressBar pg = root.GetComponent<UIProgressBar> ();
		if (slider) {
			slider.value = this.timerCountDown / TIME_PLAY;
		} else {
			print ("get progress bar faild");
		}
	}

	public void StartTimer () {
		this.timerCountDown = TIME_PLAY;
		this.enableTimer = true;
	}
}
