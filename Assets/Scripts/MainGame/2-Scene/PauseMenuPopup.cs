using UnityEngine;
using System.Collections;

public class PauseMenuPopup : PHPopup 
{
	#region Singleton
	public static PauseMenuPopup Instance { get; private set;}
	#endregion

	public PHPanel    panel;
	public PHButton   resetButton;

	public MissionInPauseGame mission1;
	public MissionInPauseGame mission2;
	public MissionInPauseGame mission3;

	void Awake () 
	{
		PauseMenuPopup.Instance = this;
	}
	
	void Start () 
	{
		this.Init ();

		if (this.resetButton != null) {
			this.resetButton.onClickHandle += HandleResetButtonClick;
		}
	}
	
	#region Animation

	override public void Hide (float second) 
	{
		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Top,second);

		if (mission1 != null) {
			mission1.Hide ();
		}
		if (mission2 != null) {
			mission2.Hide ();
		}
		if (mission3 != null) {
			mission3.Hide ();
		}
	}
	
	override public void Show (float second) 
	{
		base.Show (second);
		this.panel.Show (second);

		StartCoroutine(Wait(second));
	}

	#endregion

	private IEnumerator Wait (float seconds) 
	{
		Debug.Log ("start wait");
		yield return new WaitForSeconds(seconds);

		if (mission1 != null) {
			mission1.Show ();
		}
		if (mission2 != null) {
			mission2.Show ();
		}
		if (mission3 != null) {
			mission3.Show ();
		}
	}

	void HandleResetButtonClick () 
	{
		this.Hide (0.5f);
		SceneScript.Instance.ResetRound ();
	}
}
