using UnityEngine;
using System.Collections;

public class PHSpriteFade : PHFade 
{
	override public float alpha 
	{ 
		get {
			return base.alpha;
		}
		set {
			base.alpha = value;

			SpriteRenderer spriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
			if (spriteRenderer == null) {
				return;
			}
			spriteRenderer.color = new Color (this.normalColor.r, this.normalColor.g, this.normalColor.b, value);
		}
	}
	
	override public void Init ()
	{
		base.Init ();

		if (this.transform == null) {
			return;
		}
		SpriteRenderer spriteRenderer = this.transform.GetComponent<SpriteRenderer> ();
		if (spriteRenderer == null) {
			return;
		}
		
		this.normalColor = spriteRenderer.color;
	}
}
