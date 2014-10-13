using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PHPopup : MonoBehaviour 
{
	public bool hideWhenInit = true;

	public PHButton   closeButton;

	#region Properties

	private Texture2D _grayTexture;
	private Texture2D graySprite
	{
		get {
			if (_grayTexture == null) {
				_grayTexture = new Texture2D (100,100);
				for (int i = 0 ; i < 100 ; i ++)  {
					for (int j = 0 ; j < 100 ; j ++)  {
						_grayTexture.SetPixel(i,j,new Color (0.0f,0.0f,0.0f,0.7f));
					}
				}
				_grayTexture.Apply();
			}
			return _grayTexture;
		}
	}

	private Sprite _bgSprite;
	private Sprite bgSprite
	{
		get {
			if (_bgSprite == null) {
				_bgSprite  = Sprite.Create(this.graySprite, new Rect (0,0,100,100) , new Vector2(0.5f,0.5f), 100);
			}
			return _bgSprite;
		}
	}

	private GameObject _bgObject;

	#endregion

	protected virtual void Init() 
	{	
		// Tao gray backgroud cho popup
		_bgObject = new GameObject ();
		_bgObject.transform.parent = this.gameObject.transform;
		_bgObject.AddComponent<SpriteRenderer> ();
		_bgObject.transform.localScale = new Vector3 (Camera.main.orthographicSize * 2, Camera.main.orthographicSize * 4, 0);
		SpriteRenderer spriteRenderer = _bgObject.GetComponent<SpriteRenderer> ();
		spriteRenderer.sprite = this.bgSprite;
		spriteRenderer.color = new Color (0.0f, 0.0f, 0.0f, 1.0f);
		// Add fade component
		_bgObject.AddComponent<PHFade> ();

		// Setup close button
		if (closeButton != null) {
			closeButton.onClickHandle += HandleCloseButtonClick;
		}

		// Hide popup if need
		if (hideWhenInit) {
			this.Hide (0.0f);
			this.enabled = false;
		}
	}

	void Start () 
	{
		this.Init ();
	}

	#region Animation

	virtual public void Hide (float second) 
	{
		if (_bgObject == null) {
			return;
		}

		// Hide backgroud
		PHFade fade = _bgObject.GetComponent<PHFade> ();
		fade.FadeOut (second);
	}

	virtual public void Show (float second) 
	{	
		if (_bgObject == null) {
			return;
		}

		PHFade fade = _bgObject.GetComponent<PHFade> ();
		fade.FadeIn (second);
	}

	#endregion

	void HandleCloseButtonClick ()
	{
		this.Hide (0.35f);
	}
}
