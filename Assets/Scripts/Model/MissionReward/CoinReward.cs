using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CoinReward : MissionReward
{
	public int coin { get; private set; }
	public CoinReward (int coin)
	{
		this.coin = coin;
	}
	
	public bool DoGetReward () 
	{
	}
}