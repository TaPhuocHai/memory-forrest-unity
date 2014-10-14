using UnityEngine;
using System.Collections;

public class PHTimer : MonoBehaviour 
{	
	public bool autoStart = false;
	
	private bool enableTimer = false;
	private bool kichStart   = false;

	#region Properties

	// Thoi gian toi da
	public float maxSecond { get; private set; }
	// So giay hien tai
	public float currentSecond { get; private set; }

	// Dem nguoc ? max -> 0
	public bool isReverse { get; private set; }

	#endregion

	void Awake ()
	{
		this.maxSecond = 1;
		this.currentSecond = 1;
	}

	void Start () 
	{
		if (this.autoStart == false) {
			this.enableTimer = false;
		}
	}
	
	void Update () 
	{
		if (!this.enableTimer) {
			return;
		}

		if (this.isReverse) {
			this.currentSecond -= Time.deltaTime;
			if (this.currentSecond <= 0) {
				this.currentSecond = 0;
				this.enableTimer = false;

				this.Fire ();
			}
		} else {
			this.currentSecond += Time.deltaTime;
			if (this.currentSecond >= this.maxSecond) {
				this.currentSecond = this.maxSecond;
				this.enableTimer = false;

				this.Fire ();
			}
		}
	}

	/// <summary>
	/// Function did call when timer interval
	/// </summary>
	virtual protected void Fire ()
	{
	}

	#region Public function

	public void Reset () 
	{
		if (this.isReverse) {
			this.currentSecond = this.maxSecond;
		} else {
			this.currentSecond = 0.0f;
		}
	}

	public void AddMoreTime (float addmore) 
	{
		if (this.isReverse) {
			this.currentSecond += addmore;
			if (this.currentSecond > this.maxSecond) {
				this.currentSecond = this.maxSecond;
			}
		} else {
			this.currentSecond -= addmore;
			if (this.currentSecond < 0) {
				this.currentSecond = 0;
			}
		}
	}

	public void Start (float second, bool isReverse) 
	{
		this.kichStart = true;
		this.maxSecond = second;
		this.isReverse = isReverse;
		if (this.isReverse) {
			this.currentSecond = this.maxSecond;
		} else {
			this.currentSecond = 0;
		}
		this.enableTimer = true;
	}

	public void Stop () 
	{
		this.kichStart = false;
		this.enableTimer = false;
		if (this.isReverse) {
			this.currentSecond = this.maxSecond;
		} else {
			this.currentSecond = 0;
		}
	}

	public void Pause () 
	{
		this.enableTimer = false;
	}

	public void Resume ()
	{
		if (this.kichStart == true) {
			this.enableTimer = true;
		}
	}

	#endregion
}
