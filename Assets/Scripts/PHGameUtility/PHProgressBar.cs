using UnityEngine;
using System.Collections;

public class PHProgressBar : MonoBehaviour {

	public Transform progressBarBg;
	public Transform progressBar;

	public  Sprite  progressBarSprite = null;
	private Sprite  currentProgressBarSprite = null;

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
			spriteRenderer.transform.localScale = new Vector3 (1,1,1);
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
		Texture2D spriteTexture = this.progressBarSprite.texture;
		float withToScrop = spriteTexture.width * progress / 100;
		
		// Crop sprite
		Rect croppedSpriteRect = new Rect();
		croppedSpriteRect.x = 0;
		croppedSpriteRect.y = 0;
		croppedSpriteRect.width  = withToScrop;
		croppedSpriteRect.height = spriteTexture.height;

		Sprite croppedSprite  = Sprite.Create(spriteTexture, croppedSpriteRect, new Vector2(0.5f,0.5f), 100);

		return croppedSprite;
	}
}
