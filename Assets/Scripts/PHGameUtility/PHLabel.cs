using UnityEngine;
using System.Collections;

public class PHLabel : MonoBehaviour 
{
	void Start ()
	{
		this.Init ();
	}

	virtual public void Init () 
	{
		this.guiText.fontSize = (int)(this.guiText.fontSize * PHScreen.Instance.optimalRatio);
	}
}
