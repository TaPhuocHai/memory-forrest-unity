using UnityEngine;
using System.Collections;

public class PHMusicButton : PHPongSoundButton 
{
	override protected void OnButtonClick () 
	{
		if (PHSetting.IsSoundBackgroud) {
			PHSetting.IsSoundBackgroud = false;
		} else {
			PHSetting.IsSoundBackgroud = true;
		}
	}
}
