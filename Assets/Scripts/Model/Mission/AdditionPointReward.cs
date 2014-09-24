using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class AdditionPointReward : MissionReward
{
	public CardType cardType { get; private set; }
	public int      pointToAddMore { get; private set; }

	public AdditionPointReward (CardType cardType, int pointToAddMore)
	{
		this.cardType = cardType;
		this.pointToAddMore = pointToAddMore;
	}
	
	public bool DoGetReward () 
	{
	}
}