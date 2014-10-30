using UnityEngine;
using System.Collections;

public class JungleBookScript : MonoBehaviour 
{
	public PHButton menuButton;

	void Start ()
	{
		if (this.menuButton) {
			this.menuButton.onClickHandle += HandleMenuButtonClick;
		}
	}

	void HandleMenuButtonClick ()
	{
		Application.LoadLevel ("Menu");
	}
}
