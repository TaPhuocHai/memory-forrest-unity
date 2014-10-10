using UnityEngine;
using System.Collections;

public class PHProgressBar : MonoBehaviour {

	public Transform progressBarBg;
	public Transform progressBar;

	private Sprite normalProgressBar;
	private Sprite cropProgressBar;

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
	
	void Start () 
	{	
		this.progress = 50;
		if (this.progressBar) {
			SpriteRenderer spriteRenderer = this.progressBar.GetComponent<SpriteRenderer> ();
			this.normalProgressBar = spriteRenderer.sprite;
		}
	}

	void Update () 
	{	
		this.UpdateProgressBar (false);
	}

	private void UpdateProgressBar (bool forceUpdate) 
	{
		if (forceUpdate == false && this.cropProgressBar != null) {
			return;
		}

		if (this.progressBar) {
			this.cropProgressBar = this.cropProgressBarSprite();
			SpriteRenderer spriteRenderer = this.progressBar.GetComponent<SpriteRenderer> ();
			spriteRenderer.sprite = this.cropProgressBar;

			PHAnchorLeft anchorLeft = this.progressBar.GetComponent<PHAnchorLeft> ();
			if (anchorLeft) {
				anchorLeft.UpdateAnchor ();
			}
		}
	}

	private Sprite cropProgressBarSprite ()
	{
		if (this.progressBar == null) {
			return null;
		}

		// Calculate topLeftPoint and bottomRightPoint of drawn rectangle
		Vector3 topLeftPoint = new Vector3 (0, 0, 0);
		
		SpriteRenderer spriteRenderer = this.progressBar.GetComponent<SpriteRenderer>();
		Sprite spriteToCropSprite = spriteRenderer.sprite;
		Texture2D spriteTexture = spriteToCropSprite.texture;
		Rect spriteRect = this.progressBar.GetComponent<SpriteRenderer>().sprite.textureRect;

		float withToScrop = spriteRenderer.bounds.size.x * progress / 100;
		Vector3 bottomRightPoint= new Vector3 (withToScrop,spriteRenderer.bounds.size.y,0);
		
		int pixelsToUnits = 100; // It's PixelsToUnits of sprite which would be cropped
		
		// Crop sprite
		Rect croppedSpriteRect = spriteRect;
		croppedSpriteRect.x = 0;
		croppedSpriteRect.y = 0;
		croppedSpriteRect.width  = (Mathf.Abs(bottomRightPoint.x - topLeftPoint.x)*pixelsToUnits) * (1/this.progressBar.transform.localScale.x);
		croppedSpriteRect.height = (Mathf.Abs(bottomRightPoint.y - topLeftPoint.y)*pixelsToUnits)* (1/this.progressBar.transform.localScale.y);
		Sprite croppedSprite     = Sprite.Create(spriteTexture, croppedSpriteRect, new Vector2(0.5f,0.5f), pixelsToUnits);

		return croppedSprite;
	}
}
