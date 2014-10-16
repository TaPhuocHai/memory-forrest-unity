using UnityEngine;
using System.Collections;

public class PointScript : MonoBehaviour 
{
	void Update () 
	{
		TextMesh textMesh = this.gameObject.GetComponent<TextMesh> ();
		textMesh.text = PlayGameData.Instance.score.ToString ();
	}
}
