using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class UnlockCardReward : MissionReward
{
	public CardType cardType { get; private set; }
	
	public UnlockCardReward (CardType cardType)
	{
		this.cardType = cardType;
	}
	
	public bool DoGetReward () 
	{
	}
}