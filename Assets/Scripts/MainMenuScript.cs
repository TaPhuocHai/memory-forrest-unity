using UnityEngine;
using System.Collections;

public class MainMenuScript : MonoBehaviour 
{
	PHButton playButton;
	PHButton jungleBookButton;

	void Start ()
	{
		if (playButton != null) {
			playButton.onClickHandle += HandlePlayButtonClick;
		}
		if (jungleBookButton != null) {
			jungleBookButton.onClickHandle += HandleJungleBookButtonClick;
		}
	}

	void HandlePlayButtonClick ()
	{
		Application.LoadLevel ("Map");
	}

	void HandleJungleBookButtonClick ()
	{
	}
}
