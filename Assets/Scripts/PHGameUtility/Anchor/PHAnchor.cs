using UnityEngine;
using System.Collections;

public class PHAnchor : MonoBehaviour 
{
	void Start ()
	{
		this.Init ();
	}

	virtual public void Init () 
	{
		this.UpdateAnchor ();
	}
	virtual public void UpdateAnchor () {}
}
