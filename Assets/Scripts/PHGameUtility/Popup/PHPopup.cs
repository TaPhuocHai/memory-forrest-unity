using UnityEngine;
using System.Collections;

public class PHPopup : MonoBehaviour 
{
	public bool hideWhenInit = true;

	virtual public void Hide (bool animation) {}
	virtual public void Show (bool animation) {}
}
