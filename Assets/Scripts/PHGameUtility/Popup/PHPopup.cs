using UnityEngine;
using System.Collections;

public class PHPopup : MonoBehaviour 
{
	public bool hideWhenInit = true;

	#region Properties

	private Texture2D _grayTexture;
	private Texture2D graySprite
	{
		get {
			if (_grayTexture == null) {
				_grayTexture = new Texture2D (100,100);
				for (int i = 0 ; i < 100 ; i ++)  {
					for (int j = 0 ; j < 100 ; j ++)  {
						_grayTexture.SetPixel(i,j,new Color (0.0f,0.0f,0.0f,0.6f));
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

		if (hideWhenInit) {
			this.Hide (false);
			this.enabled = false;
		}
	}

	void Start () 
	{
		this.Init ();
	}

	void OnGUI () 
	{
		//GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), this.grayTexture);
	}

	virtual public void Hide (bool animation) 
	{

	}

	virtual public void Show (bool animation) 
	{

	}
}
