using UnityEngine;
using System.Collections;

public class ProgressBar : PHProgressBar 
{
	void Start ()
	{
		this.progress = 100;
	}

	void Update () 
	{	
		if (TimerManager.Instance.maxSecond != 0) {
			this.progress = TimerManager.Instance.currentSecond * 100 / TimerManager.Instance.maxSecond;
		} else {
			this.progress = 100;
		}
	}
}
