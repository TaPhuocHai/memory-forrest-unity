using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CoinTask : MissionTask
{
	public int coin { get; private set; }
	
	public CoinTask (int coin)
		: this (false)
	{
		this.coin = coin;
	}
	
	#region
	
	public bool DoTask () 
	{
	}
	
	#endregion
}