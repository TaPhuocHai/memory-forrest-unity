using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PHPongButton : PHButton 
{
	private Vector3 localScale;
	void Awake ()
	{
		this.localScale = this.transform.localScale;
	}

	#region Button function
	
	override protected void OnButtonEnter () 
	{
		TweenParms parms = new TweenParms().Prop("localScale", new Vector3 (localScale.x - 0.1f,localScale.y - 0.1f,localScale.z - 0.1f)).Ease(EaseType.Linear);
		HOTween.To (this.transform, 0.1f, parms);
	}

	override protected void OnButtonExit () 
	{
		TweenParms parms = new TweenParms().Prop("localScale", localScale).Ease(EaseType.EaseOutBack);
		HOTween.To (this.transform, 1.0f, parms);
	}
	
	#endregion
}
