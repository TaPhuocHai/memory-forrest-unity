using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public enum PHPanelDirection {Top, Left, Bottom, Right}

public class PHPanel : MonoBehaviour 
{
	public delegate void PHPanelAnimationComplete (PHPanelDirection direction);

	private Vector3 _normalPosition;
	private PHPanelDirection _lastDirection;

	public PHPanelAnimationComplete hideCompleteHandle;
	public PHPanelAnimationComplete showCompleteHandle;

	public bool    enablePosition = false;
	public Vector3 position;

	void Awake ()
	{
		this.Init ();
	}

	virtual public void Init () 
	{
		if (this.transform != null) {
			this._normalPosition = this.transform.position;
		}
		if (this.enablePosition) {
			this._normalPosition = position;
		}
		if (this.gameObject.GetComponent<BoxCollider> () == null) {
			this.gameObject.AddComponent<BoxCollider> ();
		}
	}

	#region Animation

	virtual public void HideToDirection (PHPanelDirection direction, float second) 
	{
		this.HideToDirection (direction, second, EaseType.EaseInBack);
	}

	virtual public void HideToDirection (PHPanelDirection direction, float second, EaseType easeType) 
	{
		if (this.transform == null) {
			return;
		}
		BoxCollider boxCollider = this.gameObject.GetComponent<BoxCollider> ();
		if (this.transform.renderer == null && boxCollider == null) {
			return;
		}
		_lastDirection = direction;

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
				this.hideCompleteHandle (_lastDirection);
			}
		} else {
			TweenParms parms = new TweenParms().Prop("position", newPosition).Ease(easeType).OnComplete(HideDidComplete);
			HOTween.To (this.transform, second, parms);
		}
	}

	virtual public void Show (float second) 
	{
		this.Show (this._normalPosition, second);
	}

	virtual public void Show (Vector3 position, float second) 
	{
		this.Show (position, second, EaseType.EaseOutBack);
	}

	virtual public void Show (Vector3 position, float second, EaseType easeType) 
	{
		if (this.transform == null || this.transform.renderer == null) {
			return;
		}

		if (second == 0) {
			this.transform.position = position;
			if (this.showCompleteHandle != null) {
				this.showCompleteHandle (_lastDirection);
			}
		} else {
			TweenParms parms = new TweenParms().Prop("position", position).Ease(easeType).OnComplete(ShowDidComplete);
			HOTween.To (this.transform, second, parms);
		}
	}
	
	#endregion 

	void HideDidComplete ()
	{
		if (this.hideCompleteHandle != null) {
			this.hideCompleteHandle (_lastDirection);
		}
	}

	void ShowDidComplete () 
	{
		if (this.showCompleteHandle != null) {
			this.showCompleteHandle (_lastDirection);
		}
	}
}
