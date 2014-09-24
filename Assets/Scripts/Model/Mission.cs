using UnityEngine;
using System.Collections;

public enum MissionType {
	MoreTime,
	UnlockExtraRound,
	UnlockCards,
	AdditionPoints,
	Money,
	Coins,
}

public class Mission {
	protected string _name;
	protected string _description;
	protected float  _rewardValue;
	protected MissionType _missionType;

	#region Properties

	public string name {
		get { return _name; }
	}
	public string description {
		get { return _description; }
	}
	public float rewardValue {
		get { return _rewardValue; }
	}
	public MissionType missionType {
		get { return _missionType; }
	}

	#endregion Properties

	#region

	/// <summary>
	/// Unlocks the mission.
	/// </summary>
	/// <param name="code">Code.</param>
	static public void UnlockMission (int code) 
	{
	}

	#endregion
}
