using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CollectTask : MissionTask
{
	/// <summary>
	/// cardType : so luong
	/// </summary>
	/// <value>The card type with number pair need collect.</value>
	public Dictionary<int, int> cardTypeWithNumberPairNeedCollect { get; private set;}

	#region Constructors

	public CollectTask (bool isAccumulationTask)
		: this(isAccumulationTask)
	{
		cardTypeWithNumberPairNeedCollect = new Dictionary<int, int> ();
		Debug.Log ("CollectTask bool");
	}

	public CollectTask () : this (true) {}

	public CollectTask (CardType cardType, int pair) 
	: this (true) 
	{
		cardTypeWithNumberPairNeedCollect.Add ((int)cardType, pair);
	}

	public CollectTask (CardType cardType, int pair, bool isAccumulationTask) 
		: this (cardType, pair) 
	{
		this.isAccumulationTask = isAccumulationTask;
	}
	
	#endregion Constructors

	public void AddTask (CardType cardType, int pair) 
	{
		cardTypeWithNumberPairNeedCollect.Add ((int)cardType, pair);
	}

	#region

	public bool DoTask () 
	{
	}

	#endregion
}