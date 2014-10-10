using UnityEngine;
using System.Collections;

public class PHProgressBar : MonoBehaviour {

	public Transform progressBarBg;
	public Transform progressBar;

	public Sprite  progressBarSprite;
	private Sprite currentProgressBarSprite;

	/// <summary>
	/// Gets or sets the progress. From 0 -> 100
	/// </summary>
	/// <value>The progress.</value>
	private float _progress;
	public float progress {
		get {
			return _progress;
		}
		set {
			_progress = value;
			this.UpdateProgressBar (true);
		}
	}
	
	void Start ()  {}

	void Update () 
	{	
		this.UpdateProgressBar (false);
	}

	private void UpdateProgressBar (bool forceUpdate) 
	{	
		if (forceUpdate == false && this.currentProgressBarSprite != null) {
			return;
		}

		if (this.progressBar) {		
			this.currentProgressBarSprite = this.cropProgressBar();
			SpriteRenderer spriteRenderer = this.progressBar.GetComponent<SpriteRenderer> ();
			spriteRenderer.sprite = this.currentProgressBarSprite;

			PHAnchorLeft anchorLeft = this.progressBar.GetComponent<PHAnchorLeft> ();
			if (anchorLeft) {
				anchorLeft.UpdateAnchor ();
			}
		}
	}

	private Sprite cropProgressBar ()
	{
		if (this.progressBar == null || this.progressBarSprite == null) {
			return null;
		}

		// Calculate topLeftPoint and bottomRightPoint of drawn rectangle
		Vector3 topLeftPoint = new Vector3 (0, 0, 0);
		
//		SpriteRenderer spriteRenderer = this.progressBar.GetComponent<SpriteRenderer>();
//		Sprite spriteToCropSprite     = spriteRenderer.sprite;
//		Texture2D spriteTexture       = spriteToCropSprite.texture;
//		Rect spriteRect               = this.progressBar.GetComponent<SpriteRenderer>().sprite.textureRect;
//
		//float withToScrop = spriteRenderer.bounds.size.x * progress / 100;
		float withToScrop = this.progressBarSprite.bounds.size.x * progress / 100;
		Vector3 bottomRightPoint= new Vector3 (withToScrop,this.progressBarSprite.bounds.size.y,0);

		Texture2D spriteTexture = this.progressBarSprite.texture;
		
		int pixelsToUnits = 100; // It's PixelsToUnits of sprite which would be cropped
		
		// Crop sprite
		Rect croppedSpriteRect = new Rect();
		croppedSpriteRect.x = 0;
		croppedSpriteRect.y = 0;
		croppedSpriteRect.width  = (Mathf.Abs(bottomRightPoint.x - topLeftPoint.x)*pixelsToUnits);
		croppedSpriteRect.height = (Mathf.Abs(bottomRightPoint.y - topLeftPoint.y)*pixelsToUnits);
		Sprite croppedSprite     = Sprite.Create(spriteTexture, croppedSpriteRect, new Vector2(0.5f,0.5f), pixelsToUnits);

		return croppedSprite;
	}
}
