using UnityEngine;
using System.Collections;

public class PointScript : PHLabel 
{
	void OnGUI () 
	{
		GUI.depth = 0;
		this.guiText.text = PlayGameData.Instance.score.ToString ();
	}

	void Update () 
	{
		this.UpdateAnchor ();
	}
}
