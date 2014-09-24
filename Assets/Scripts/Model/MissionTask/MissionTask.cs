using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class MissionTask
{
	protected bool isAccumulationTask { get; protected set; }

	public MissionTask (bool isAccumulationTask)
	{
		this.isAccumulationTask = isAccumulationTask;
	}

	/// <summary>
	/// Dos the task.
	/// </summary>
	/// <returns><c>true</c>, if task was done, <c>false</c> otherwise.</returns>
	abstract public bool DoTask () {}
}