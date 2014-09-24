using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UnlockExtraRound : MissionReward
{
	public int roundToUnlock { get; private set; }
	public UnlockExtraRound (int roundToUnlock)
	{
		this.roundToUnlock = roundToUnlock;
	}
	
	public bool DoGetReward () 
	{
	}
}