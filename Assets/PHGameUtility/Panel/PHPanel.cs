﻿using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public enum PHPanelDirection {Top, Left, Bottom, Right}

public class PHPanel : MonoBehaviour 
{
	public delegate void PHPanelAnimationComplete ();

	private Vector3 _normalPosition;

	public PHPanelAnimationComplete hideCompleteHandle;
	public PHPanelAnimationComplete showCompleteHandle;

	void Awake ()
	{
		this.Init ();
	}

	virtual public void Init () 
	{
		if (this.transform != null) {
			this._normalPosition = this.transform.position;
		}
	}

	#region Animation

	virtual public void HideToDirection (PHPanelDirection direction, float second) 
	{
		if (this.transform == null) {
			return;
		}
		BoxCollider boxCollider = this.gameObject.GetComponent<BoxCollider> ();
		if (this.transform.renderer == null && boxCollider == null) {
			return;
		}

		Vector3 newPosition = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z);
		Vector3 size = PHUtility.GetSizeOfTransforum (this.transform);

		switch (direction) 
		{
		case PHPanelDirection.Top:
			newPosition.y = PHUtility.WorldHeight/2 + size.y/2;
			break;
		case PHPanelDirection.Left: 
			newPosition.x = - PHUtility.WorldWidth/2 - size.x/2;
			break;
		case PHPanelDirection.Bottom: 
			newPosition.y = - PHUtility.WorldHeight/2 - size.y/2;
			break;
		case PHPanelDirection.Right: 
			newPosition.x = PHUtility.WorldWidth/2 + size.x/2;
			break;
		}

		if (second == 0) {
			this.transform.position = newPosition;
			if (this.hideCompleteHandle != null) {
				this.hideCompleteHandle ();
			}
		} else {
			TweenParms parms = new TweenParms().Prop("position", newPosition).Ease(EaseType.EaseInBack).OnComplete(HideDidComplete);
			HOTween.To (this.transform, second, parms);
		}
	}

	virtual public void Show (float second) 
	{
		this.Show (this._normalPosition, second);
	}

	virtual public void Show (Vector3 position, float second) 
	{
		if (this.transform == null || this.transform.renderer == null) {
			return;
		}

		if (second == 0) {
			this.transform.position = position;
			if (this.showCompleteHandle != null) {
				this.showCompleteHandle ();
			}
		} else {
			TweenParms parms = new TweenParms().Prop("position", position).Ease(EaseType.EaseOutBack).OnComplete(ShowDidComplete);
			HOTween.To (this.transform, second, parms);
		}
	}
	
	#endregion 

	void HideDidComplete ()
	{
		if (this.hideCompleteHandle != null) {
			this.hideCompleteHandle ();
		}
	}

	void ShowDidComplete () 
	{
		if (this.showCompleteHandle != null) {
			this.showCompleteHandle ();
		}
	}
}
