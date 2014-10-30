using UnityEngine;
using System.Collections;

public class PHSoundEffectButton : PHPongSoundButton 
{
	override protected void OnButtonClick () 
	{
		if (PHSetting.IsSoundEffect) {
			PHSetting.IsSoundEffect = false;
		} else {
			PHSetting.IsSoundEffect = true;
		}
	}
}
