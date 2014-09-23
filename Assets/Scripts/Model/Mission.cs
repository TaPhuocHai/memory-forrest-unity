using UnityEngine;
using System.Collections;

public enum MissionType {
	Time,
	Money,
	UnlockExtraRound,
	Coins,
	UpgradeCard
}

public class Mission {
	protected string _name;
	protected string _description;
	protected float  _rewardValue;
	protected MissionType _missionType;

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
}
