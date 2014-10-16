using UnityEngine;
using System.Collections;

public class PHLabel : MonoBehaviour 
{
	#region Properties 

	private PHLabelFade fade {
		get {
			if (this.gameObject.GetComponent<PHLabelFade> () == null) {
				this.gameObject.AddComponent<PHLabelFade>();
			}
			return this.gameObject.GetComponent<PHLabelFade> ();
		}
	}

	public float alpha 
	{ 
		get {
			return this.guiText.color.a;
		}
		set {
			this.guiText.color = new Color (this.guiText.color.r, this.guiText.color.g, this.guiText.color.b, value);
		}
	}

	#endregion 

	void Start ()
	{
		this.Init ();
	}

	virtual public void Init () 
	{
		this.guiText.fontSize = (int)(this.guiText.fontSize * PHScreen.Instance.optimalRatio);
	}

	#region Animation

	public void FadeIn (float second)
	{
		if (this.guiText == null) {
			return;
		}
		this.fade.FadeIn (second);
	}

	public void FadeOut (float second) 
	{
		if (this.guiText == null) {
			return;
		}
		this.fade.FadeOut (second);
	}

	#endregion
}
