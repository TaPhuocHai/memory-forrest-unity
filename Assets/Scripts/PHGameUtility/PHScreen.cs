using UnityEngine;
using System.Collections;

public class PHScreen : MonoBehaviour {

	public float defaultWidth  = 1080;
	public float defaultHeight = 1920; 

	public void Awake ()
	{
		_instance = this;
	}

	#region Instance

	static PHScreen _instance;
	public static PHScreen Instance {
		get {
			return _instance;
		}
	}

	#endregion

	#region Properties

	private float _ratioWith;
	public float ratioWith 
	{
		get {
			if (_ratioWith == 0) {
				_ratioWith = Screen.width/defaultWidth;
			}
			return _ratioWith;
		}
	}

	private float _ratioHeight;
	public float ratioHeight 
	{
		get {
			if (_ratioHeight == 0) {
				_ratioHeight = Screen.height/defaultHeight;
			}
			return _ratioHeight;
		}
	}

	private float _optimalRatio;
	public float optimalRatio
	{
		get {
			if (_optimalRatio == 0) {
				if (this.ratioWith > this.ratioHeight) {
					_optimalRatio = this.ratioHeight;
				} else {
					_optimalRatio = this.ratioWith;
				}
			}
			return _optimalRatio;
		}
	}

	#endregion
}
