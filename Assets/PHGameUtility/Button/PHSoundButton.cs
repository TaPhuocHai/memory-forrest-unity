using UnityEngine;
using System.Collections;

public class PHSoundButton : MonoBehaviour 
{
	public Sprite buttonOnUpSprite;
	public Sprite buttonOnDownSprite;

	public Sprite buttonOffUpSprite;
	public Sprite buttonOffDownSprite;
	
	public AudioClip buttonPressedSound;

	private bool _isOn;
	public bool isOn {
		get { return _isOn; }
		set { 
			_isOn = value;
			if (this.isOn && this.buttonOnUpSprite) {
				this.transform.GetComponent<SpriteRenderer> ().sprite = this.buttonOnUpSprite;
			}
			if (!this.isOn && this.buttonOffUpSprite) {
				this.transform.GetComponent<SpriteRenderer> ().sprite = this.buttonOffUpSprite;
			}
		}
	}

	void Awake ()
	{
		this.isOn = true;
	}

	void Start () 
	{	
		this.Init ();
	}

	void Update ()
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
		
		this.UpdateSpriteDownState ();
	}
	
	void OnMouseExit () 
	{
		this.OnButtonExit ();
		
		if (this.transform.renderer == null) return;		
		this.UpdateSpriteUpState ();
	}
	
	void OnMouseUpAsButton () 
	{
		if (this.isOn) {
			this.isOn = false;
		} else {
			this.isOn = true;
		}

		this.OnButtonClick ();
		this.UpdateSpriteUpState ();
	}
	
	#endregion
	
	#region Button function
	
	virtual protected void Init ()
	{
		if (buttonPressedSound != null) {
			this.gameObject.AddComponent<AudioSource> ();
			this.gameObject.audio.clip = buttonPressedSound;
		}
		if (this.gameObject.GetComponent<BoxCollider> () == null) {
			this.gameObject.AddComponent<BoxCollider> ();
		}

		// Set sprite 
		this.UpdateSpriteUpState ();
	}
	
	virtual protected void OnButtonClick () 
	{
		if (buttonPressedSound != null && PHSetting.IsSoundEffect) {
			this.gameObject.audio.Play ();
		}
	}
	
	virtual protected void OnButtonEnter () {}
	virtual protected void OnButtonExit () {}	

	#endregion

	#region Helper

	void UpdateSpriteUpState ()
	{
		if (this.isOn && this.buttonOnUpSprite) {
			this.transform.GetComponent<SpriteRenderer> ().sprite = this.buttonOnUpSprite;
		}
		if (!this.isOn && this.buttonOffUpSprite) {
			this.transform.GetComponent<SpriteRenderer> ().sprite = this.buttonOffUpSprite;
		}
	}

	void UpdateSpriteDownState () 
	{
		SpriteRenderer spriteRender = this.transform.GetComponent<SpriteRenderer> ();
		if (this.isOn && this.buttonOnDownSprite) {
			spriteRender.sprite = this.buttonOnDownSprite;
		}
		if (!this.isOn && this.buttonOffDownSprite) {
			spriteRender.sprite = this.buttonOffDownSprite;
		}
	}
	#endregion
}
