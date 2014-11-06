using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class TimeUpPanel : MonoBehaviour
{
	#region Singleton
	public static TimeUpPanel Instance { get; private set;}
	#endregion

	private Vector3 normalPanelPosition;

	private PHPanel panel;
	void Start () 
	{	
		TimeUpPanel.Instance = this;

		normalPanelPosition = this.transform.position;

		panel = this.gameObject.GetComponent<PHPanel> ();
		panel.showCompleteHandle += HandlePanelShowCompleteHandle;
		panel.hideCompleteHandle += HandlePanelHideCompleteHandle;
		panel.HideToDirection (PHPanelDirection.Bottom, 0);
	}
	
	void Update () 
	{	
	}

	public void Show ()
	{
		panel.Show (normalPanelPosition, 0.6f,EaseType.EaseOutCirc);
	}

	void HandlePanelShowCompleteHandle (PHPanelDirection lastDirection)
	{
		Vector3 newPosition = new Vector3 ();
		newPosition.x = this.transform.position.x;
		newPosition.y = this.transform.position.y + 0.5f;
		newPosition.z = this.transform.position.z;

		TweenParms parms = new TweenParms().Prop("position", newPosition).Ease(EaseType.Linear).OnComplete(EnterHidePanel);
		HOTween.To (this.transform, 1.0f, parms);
	}

	void EnterHidePanel() 
	{
		panel.HideToDirection (PHPanelDirection.Top, 0.4f,EaseType.EaseOutCirc);
	}
	void HandlePanelHideCompleteHandle (PHPanelDirection lastDirection)
	{
		if (lastDirection == PHPanelDirection.Top) {
			RescuePopup.Instance.Show (Constant.kPopupAnimationDuraction);
			panel.HideToDirection (PHPanelDirection.Bottom, 0);
		}
	}
}
