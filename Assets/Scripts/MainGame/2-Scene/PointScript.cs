using UnityEngine;
using System.Collections;

public class PointScript : PHLabel 
{
	void OnGUI () 
	{
		this.guiText.text = PlayGameData.Instance.score.ToString ();
	}

	void Update () 
	{
		this.UpdateAnchor ();
	}
}
