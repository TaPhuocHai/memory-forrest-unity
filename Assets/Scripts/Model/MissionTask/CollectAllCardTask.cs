using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CollectAllCardTask : MissionTask
{
	public int round { get; private set; }
	
	public CollectAllCardTask (int round)
		: this (false)
	{
		this.round = round;
	}
	
	#region
	
	public bool DoTask () 
	{
	}
	
	#endregion
}