using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ScoreTask : MissionTask
{
	public int score { get; private set; }

	public ScoreTask (int score)
		: this (false)
	{
		this.score = score;
	}

	#region
	
	public bool DoTask () 
	{
	}
	
	#endregion
}