﻿using UnityEngine;
using System.Collections;

public class FlipCardScript : MonoBehaviour {

	private int fps = 60;
	private float rotateDegreePerSecond = 720f;
	private bool isFaceUp = false;
	const   float FLIP_LIMIT_DEGREE = 180f;

	private float waitTime;
	private bool  isAnimationProgresing;

	// Use this for initialization
	void Start () {
		waitTime = 1.0f / fps;
		isAnimationProgresing = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown () {
		print ("OnMouseDown");
			
		if (isAnimationProgresing) {
			return;
		}
		if (SceneScript.enableToTouch == false) {
			return;
		}
		this.FlipCard (true);
	}

	public void FlipCard (bool isAddToSceneToCheck) {
		if (isAnimationProgresing) {
			return;
		}
		StartCoroutine (flip (isAddToSceneToCheck));
	}

	IEnumerator flip (bool isAddToSceneToCheck) {

		if (isAddToSceneToCheck && !isFaceUp) {
			GameObject scene = GameObject.FindGameObjectWithTag("Scene");
			SceneScript sceneScript = scene.GetComponent<SceneScript>();
			if (sceneScript) {
				sceneScript.OpenCard(this.gameObject);
			}
		}

		isAnimationProgresing = true;

		bool done = false;
		while (!done) {
			float degree = rotateDegreePerSecond * Time.deltaTime;
			if (isFaceUp) {
				degree = - degree;
			}
			transform.Rotate(0,degree,0);

			if (FLIP_LIMIT_DEGREE < transform.eulerAngles.y) {
				transform.Rotate(0,-degree,0);
				done = true;
			}

			yield return new WaitForSeconds(waitTime);
		}
		isFaceUp = ! isFaceUp;
		isAnimationProgresing = false;
	}
}
