using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PHScalePanel : PHPanel {

	private Vector3 localScale;

	void Awake ()
	{
		this.Init ();
	}

	override public void Init () 
	{
		base.Init ();
		this.localScale = this.transform.localScale;
	}

	override public void HideToDirection (PHPanelDirection direction, float second) 
	{
		base.HideToDirection (direction, second);

		TweenParms parms = new TweenParms().Prop("localScale", new Vector3 (localScale.x - 0.1f,localScale.y - 0.1f,localScale.z - 0.1f)).Ease(EaseType.EaseInBack);
		HOTween.To (this.transform, second, parms);
	}

	override public void Show (Vector3 position, float second) 
	{
		base.Show (position, second);

		TweenParms parms = new TweenParms().Prop("localScale", localScale).Ease(EaseType.EaseOutBack);
		HOTween.To (this.transform, second, parms);
	}
}
