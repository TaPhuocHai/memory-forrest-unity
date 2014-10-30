using UnityEngine;
using System.Collections;

public abstract class PHAnchor : MonoBehaviour 
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
