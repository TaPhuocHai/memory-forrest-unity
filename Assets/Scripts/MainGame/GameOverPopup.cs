using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GameOverPopup : PHPopup 
{	
	#region Singleton
	public static GameOverPopup Instance { get; private set;}
	#endregion

	public PHPanel    panel;
	public PHButton   resetButton;
	public PHButton   menuButton;
	public TextMesh   pointText;
	public TextMesh   recordText;

	public GameOverPopup ()
	{
		GameOverPopup.Instance = this;
	}

	void Start ()
	{
		this.Init ();
		
		if (this.resetButton != null) {
			this.resetButton.onClickHandle += HandleResetButtonClick;
		}
		if (this.menuButton != null) {
			this.menuButton.onClickHandle += HandleMenuButtonClick;
		}
	}

	#region Animation
	
	override public void Hide (float second) 
	{
		base.Hide (second);
		this.panel.HideToDirection (PHPanelDirection.Top,second);
	}
	
	override public void Show (float second) 
	{
		base.Show (second);

		// Check and save best score
		int score = PlayGameData.Instance.score;
		if (score > Player.bestScore) {
			Player.bestScore = score;
		}

		// Show score
		this.pointText.text = "Point : " + PlayGameData.Instance.score.ToString ();

		// Show best record
		this.recordText.text = "Record : " + Player.bestScore.ToString ();

		// Show panel
		this.panel.Show (second);
	}
	
	#endregion

	void HandleResetButtonClick () 
	{
		this.Hide (0.5f);
		SceneScript.Instance.ResetRound ();
	}

	void HandleMenuButtonClick ()
	{
		Application.LoadLevel ("Map");
	}
}
