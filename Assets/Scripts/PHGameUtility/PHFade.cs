using UnityEngine;
using System.Collections;

public class PHFade : MonoBehaviour 
{
	// Thoi gian animation
	private float second;

	// Luu lai trang thai normal color
	private Color normalColor;

	// Cac bien su dung de chay animation
	private float currentTime;
	private float fromValue;
	private float toValue;

	// Type animation
	private enum PHFadeType {In, Out}
	private  PHFadeType type;
	
	private float _alpha;
	public float alpha { 
		get {
			return _alpha;
		}
		set {
			SpriteRenderer spriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
			if (spriteRenderer == null) {
				return;
			}
			spriteRenderer.color = new Color (this.normalColor.r, this.normalColor.g, this.normalColor.b, value);

			_alpha = value;
		}
	}

	void Awake () 
	{
		if (this.transform == null) {
			return;
		}
		SpriteRenderer spriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		if (spriteRenderer == null) {
			return;
		}

		this.normalColor = spriteRenderer.color;
		this.enabled = false;
	}

	void Update () 
	{
		if (this.transform == null) {		
			return;
		}

		currentTime += Time.deltaTime;
		float value = 0;
		if (type == PHFadeType.In) {
			// 0 alpha
			// 0 t
			value = (currentTime / this.second) * this.toValue;

			if (value >= toValue) {
				value = toValue;
				this.enabled = false;
			}
		}
		if (type == PHFadeType.Out) {
			// alpha 0 
			// 0     t
			value = this.fromValue - (currentTime / this.second) * this.fromValue;

			if (value <= this.toValue) {
				value = toValue;
				this.enabled = false;
			}
		}

		this.alpha = value;
	}

	#region Animation

	public void FadeIn (float second)
	{
		if (second == 0) {
			return;
			this.alpha = normalColor.a;
		}

		this.enabled = true;

		this.second = second;
		fromValue = 0;
		toValue = normalColor.a;
		type = PHFadeType.In;
		currentTime = 0;
	}

	public void FadeOut (float second)
	{
		if (second == 0) {
			this.alpha = 0;
			return;
		}

		this.enabled = true;
		this.second = second;
		fromValue = normalColor.a;
		toValue = 0;
		type = PHFadeType.Out;
		currentTime = 0;
	}

	#endregion
}
