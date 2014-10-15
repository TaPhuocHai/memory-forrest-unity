using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GetScoreText : MonoBehaviour 
{
	public PHLabel label;
	public bool    startAnimation;

	void Start ()
	{
		StartCoroutine (WaitingAndFadeOut (0.1f));
		StartCoroutine (WaitingAndDestroy (0.75f));
	}

	void Update ()
	{
		this.gameObject.transform.Translate (new Vector3 (0, 0.06f, 0));
	}

	private IEnumerator WaitingAndFadeOut (float second)
	{
		yield return new WaitForSeconds (second);
		label.FadeOut (0.65f);
	}

	private IEnumerator WaitingAndDestroy (float second)
	{
		yield return new WaitForSeconds (second);
		Destroy (this.gameObject);
	}
}
