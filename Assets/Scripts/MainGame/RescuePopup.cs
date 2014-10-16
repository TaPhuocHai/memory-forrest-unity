using UnityEngine;
using System.Collections;

public class RescuePopup : PHPopup 
{
	#region Singleton
	public static RescuePopup Instance { get; private set;}
	#endregion
	
	public PHPanel    panel;
	public PHButton   rescureButton;

	public RescuePopup ()
	{
		RescuePopup.Instance = this;
	}

	void Start () 
	{
		this.Init ();

		if (this.rescureButton != null) {
			this.rescureButton.onClickHandle += HandleRescueButtonClick;
		}
	}

	void HandleRescueButtonClick ()
	{
		// Cong them thoi gian
		TimerManager.Instance.AddMoreTime (Constant.kRescueAddTime);

		// Tru coin

		// An 
		this.Hide (Constant.kPopupAnimationDuraction);
	}

	IEnumerator WaitingAndShowGameOver (float second)
	{
		yield return new WaitForSeconds (second);
		GameOverPopup.Instance.Show (Constant.kPopupAnimationDuraction);
	}

	#region Animation
	
	override public void Hide (float second) 
	{
		TimerManager.Instance.Resume ();
		
		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Top,second);
	}
	
	override public void Show (float second) 
	{
		// Pause timer
		TimerManager.Instance.Pause ();

		base.Show (second);
		// Show panel
		this.panel.Show (new Vector3(0,0,this.panel.gameObject.transform.position.z), second);
	}

	override public void OnClose ()
	{
		base.OnClose ();

		// Stop music backgroud
		PHMusicBackground.Instance.Stop();
		
		// Play sound effect
		SoundEffects.Play (SoundEffectTypes.EndGame);

		// Wating and show game over
		StartCoroutine (WaitingAndShowGameOver(0.35f));
	}

	#endregion
}
