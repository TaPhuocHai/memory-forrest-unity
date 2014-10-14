using UnityEngine;
using System.Collections;

public class PHLabelFade : PHFade 
{
	override public float alpha 
	{ 
		get {
			return base.alpha;
		}
		set {
			base.alpha = value;
			
			if (this.guiText == null) {
				return;
			}
			this.guiText.color = new Color (this.normalColor.r, this.normalColor.g, this.normalColor.b, value);
		}
	}
	
	override public void Init ()
	{
		base.Init ();

		if (this.guiText == null) {
			return;
		}
		
		this.normalColor = guiText.color;
	}
}
