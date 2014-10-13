using UnityEngine;
using System.Collections;

public class PHAnchor : MonoBehaviour 
{
	void Start ()
	{
		this.UpdateAnchor ();
	}
	virtual public void UpdateAnchor () {}
}
