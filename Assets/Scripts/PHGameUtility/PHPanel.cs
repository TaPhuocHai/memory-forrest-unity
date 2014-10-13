using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PHPanel : MonoBehaviour 
{
	public enum PHPanelDirection {Top, Left, Bottom, Right}

	private Vector3 _normalPosition;

	void Start ()
	{
		if (this.transform != null) {
			this._normalPosition = this.transform.position;
		}
	}

	public void HideToDirection (PHPanelDirection direction, float second) 
	{
		if (this.transform == null || this.transform.renderer == null) {
			return;
		}

		Vector3 newPosition = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
		
		switch (direction) 
		{
		case PHPanelDirection.Top:
			newPosition.y = PHUtility.WorldHeight/2 + this.transform.renderer.bounds.size.y/2;
			break;
		case PHPanelDirection.Left: 
			newPosition.x = - PHUtility.WorldWidth/2 - this.transform.renderer.bounds.size.x/2;
			break;
		case PHPanelDirection.Bottom: 
			newPosition.y = - PHUtility.WorldHeight/2 - this.transform.renderer.bounds.size.y/2;
			break;
		case PHPanelDirection.Right: 
			newPosition.x = PHUtility.WorldWidth/2 + this.transform.renderer.bounds.size.x/2;
			break;
		}

		if (second == 0) {
			this.transform.position = newPosition;
		} else {
			TweenParms parms = new TweenParms().Prop("position", newPosition).Ease(EaseType.EaseOutBack);
			HOTween.To (this.transform, second, parms);
		}
	}

	public void Show (float second) 
	{
		this.Show (this._normalPosition, second);
	}

	public void Show (Vector3 position, float second) 
	{
		if (this.transform == null || this.transform.renderer == null) {
			return;
		}

		if (second == 0) {
			this.transform.position = position;
		} else {
			TweenParms parms = new TweenParms().Prop("position", position).Ease(EaseType.EaseOutBack);
			HOTween.To (this.transform, second, parms);
		}
	}
}
