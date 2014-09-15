using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class GameOverScipt : MonoBehaviour {
	
	void Start () {
		this.transform.position = new Vector3 (this.transform.position.x, 2,0);
	}

	void Update () {
	
	}

	public void EnterGameOver () {
		TweenParms parms = new TweenParms().Prop("position", new Vector3(this.transform.position.x,0,0)).Ease(EaseType.EaseOutBack);
		HOTween.To (this.transform, 0.65f, parms);
	}
}
