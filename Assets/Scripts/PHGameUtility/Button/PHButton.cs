using UnityEngine;
using System.Collections;

public class PHButton : MonoBehaviour 
{
	public delegate void PHButtonClick ();

	public Sprite buttonUpSprite;
	public Sprite buttonDownSprite;

	public AudioClip buttonPressedSound;

	public PHButtonClick onClickHandle;

	void Start () 
	{	
		if (buttonPressedSound != null) {
			this.gameObject.AddComponent<AudioSource> ();
			this.gameObject.audio.clip = buttonPressedSound;
		}
	}

	#region Mouse touch

	void OnMouseDown () 
	{
	}

	void OnMouseUp ()
	{
	}

	void OnMouseOver () 
	{	
	}

	void OnMouseEnter () 
	{
		this.OnButtonEnter ();

		if (this.transform.renderer == null) return;
		
		SpriteRenderer spriteRender = this.transform.GetComponent<SpriteRenderer> ();
		if (spriteRender && this.buttonDownSprite) {
			spriteRender.sprite = this.buttonDownSprite;
		}
	}

	void OnMouseExit () 
	{
		this.OnButtonExit ();

		if (this.transform.renderer == null) return;
		
		SpriteRenderer spriteRender = this.transform.GetComponent<SpriteRenderer> ();
		if (spriteRender && this.buttonUpSprite) {
			spriteRender.sprite = this.buttonUpSprite;
		}
	}

	void OnMouseUpAsButton () 
	{
		this.OnButtonClick ();
	}

	#endregion

	#region Button function

	virtual protected void OnButtonClick () 
	{
		if (buttonPressedSound != null && PHSetting.IsSoundEffect) {
			this.gameObject.audio.Play ();
		}

		if (this.onClickHandle != null) {
			this.onClickHandle ();
		}
	}

	virtual protected void OnButtonEnter () {}
	virtual protected void OnButtonExit () {}

	#endregion
}
