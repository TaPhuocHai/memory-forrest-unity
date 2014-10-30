using UnityEngine;
using System.Collections;

public class PauseButton : PHPongButton 
{
	override protected void OnButtonClick () 
	{
		PauseMenuPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}
}
