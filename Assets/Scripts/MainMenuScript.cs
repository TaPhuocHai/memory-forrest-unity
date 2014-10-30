using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour 
{
	public PHButton playButton;
	public PHButton jungleBookButton;

	public PHSoundButton musicButton;
	public PHSoundButton soundEffectButton;

	void Start ()
	{
		if (playButton != null) {
			playButton.onClickHandle += HandlePlayButtonClick;
		}
		if (jungleBookButton != null) {
			jungleBookButton.onClickHandle += HandleJungleBookButtonClick;
		}

		musicButton.isOn = PHSetting.IsSoundBackgroud;
		soundEffectButton.isOn = PHSetting.IsSoundEffect;
	}

	void HandlePlayButtonClick ()
	{
		Application.LoadLevel ("Map");
	}

	void HandleJungleBookButtonClick ()
	{
		Application.LoadLevel ("Book");
	}
}
