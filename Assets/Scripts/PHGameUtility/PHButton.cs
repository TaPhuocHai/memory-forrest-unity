using UnityEngine;
using System.Collections;

public class PHButton : MonoBehaviour 
{
	public Sprite buttonUpSprite;
	public Sprite buttonDownSprite;
	public AudioClip buttonPressedSound;
	
	void Start () 
	{	
	}

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
		if (this.transform.renderer == null) return;
		
		SpriteRenderer spriteRender = this.transform.GetComponent<SpriteRenderer> ();
		if (spriteRender && this.buttonDownSprite) {
			spriteRender.sprite = this.buttonDownSprite;
		}
	}

	void OnMouseExit () 
	{
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

	virtual protected void OnButtonPressed () {}
}
