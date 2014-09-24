using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum MissionType {
	MoreTime,
	UnlockExtraRound,
	UnlockCards,
	AdditionPoints,
	Money,
	Coins,
}

public class Mission {

	protected int    _id;
	protected string _name;
	protected string _description;
	protected int    _goldValue;
	protected int    _rewardValue;
	protected MissionType _missionType;

	protected bool  _isIncremental;
	protected int   _goldModifier;
	protected int   _rewardModifier;

	#region Properties

	public int id { get { return _id; } }
	public string name { get { return _name; } }
	public string description { get { return _description; } }
	public int goldValue { get { return _goldValue; } }
	public int rewardValue { get { return _rewardValue; } }
	public MissionType missionType { get { return _missionType; } }

	public bool isIncremental { get { return _isIncremental; } }
	public int goldModifier { get { return _goldModifier; } }
	public int rewardModifier { get { return _rewardModifier; } }

	#endregion Properties

	public Mission(int id, string name, string description,int goldValue, int rewardValue, MissionType missionType) {
		_id            = id;
		_name          = name;
		_description   = description;
		_goldValue     = goldValue;
		_rewardValue   = rewardValue;
		_isIncremental = false;
	}

	public Mission(int id, string name, string description,int goldValue, int rewardValue, MissionType missionType,
	               int goldModifier, int rewardModifier) {
		_id          = id;
		_name        = name;
		_description = description;
		_goldValue   = goldValue;
		_rewardValue = rewardValue;

		_isIncremental  = true;
		_goldModifier   = goldModifier;
		_rewardModifier = rewardModifier;
	}	
}
