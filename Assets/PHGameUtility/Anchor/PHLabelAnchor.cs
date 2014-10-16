using UnityEngine;
using System.Collections;

/// <summary>
/// Neo 1 label den 1 object
/// GUIText phai co setting : anchor : middle center, aligment : center
/// </summary>
public abstract class PHLabelAnchor : PHAnchor
{
	void Update ()
	{
		this.UpdateAnchor ();
	}
}
