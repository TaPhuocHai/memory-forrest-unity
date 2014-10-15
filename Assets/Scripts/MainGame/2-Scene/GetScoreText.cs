using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GetScoreText : MonoBehaviour 
{
	public PHLabel label;
	public bool    startAnimation;

	void Start ()
	{
		StartCoroutine (WaitAndHide ());
	}

	private IEnumerator WaitAndHide ()
	{
		yield return new WaitForSeconds (0.5f);
		label.FadeOut (0.6f);

		Vector3 newPos = new Vector3 ();
		newPos.x = this.transform.position.x;
		newPos.y = this.transform.position.y + 0.1f;
		newPos.z = this.transform.position.z;

		this.transform.Translate(new Vector3 (0.0f,0.1f,0.0f));
	}

	private float currentValue = 0;
	private float toValue  = 0.1f;
	private float yVelocity = 0.0f;
	public float smoothTime = 0.3f;

	void Update ()
	{
		if (startAnimation)
		currentValue = Mathf.SmoothDamp (currentValue, toValue, ref yVelocity, smoothTime);
		Debug.Log ("value = " + currentValue.ToString ());
	}
}
