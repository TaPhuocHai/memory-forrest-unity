using UnityEngine;
using System.Collections;

public class PHButton : MonoBehaviour 
{
	public delegate void PHButtonPressed ();

	public Sprite buttonUpSprite;
	public Sprite buttonDownSprite;
	public AudioClip buttonPressedSound;

	public PHButtonPressed buttonPressedDelegate;

	void Start () 
	{	
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
		this.OnButtonPressed ();
	}

	#endregion

	#region Button function

	virtual protected void OnButtonPressed () 
	{
		if (this.buttonPressedDelegate != null) {
			this.buttonPressedDelegate ();
		}
	}

	virtual protected void OnButtonEnter () {}
	virtual protected void OnButtonExit () {}

	#endregion
}
