using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UnlockExtraRoundReward : MissionReward
{
	public int roundToUnlock { get; private set; }
	public UnlockExtraRoundReward (int roundToUnlock)
	{
		this.roundToUnlock = roundToUnlock;
	}
	
	public bool DoGetReward () 
	{
	}
}