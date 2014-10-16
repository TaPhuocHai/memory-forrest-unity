using UnityEngine;
using System.Collections;

public class PauseButton : PHScaleButton 
{
	override protected void OnButtonClick () 
	{
		GameObject pauseMenu =  GameObject.FindGameObjectWithTag ("PauseMenu");
		if (pauseMenu == null) {
			Debug.Log ("pauseMenu not found");
			return;
		}
		PHPopup pauseMenuPopup = pauseMenu.GetComponent<PHPopup> ();
		if (pauseMenuPopup == null) {
			Debug.Log ("pauseMenuPopup not found");
			return;
		}
		pauseMenuPopup.Show (0.4f);
	}
}
