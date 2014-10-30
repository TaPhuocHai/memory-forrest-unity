using UnityEngine;
using System.Collections;

public abstract class PHAnchor : MonoBehaviour 
{
	bool IsUpdateAnchor;

	void Start ()
	{
		this.Init ();
	}

	virtual public void Init () 
	{
		this.UpdateAnchor ();
	}

	void OnEnable ()
	{
	}

	void Update ()
	{
	}
	
	virtual public void UpdateAnchor () {}	
}
