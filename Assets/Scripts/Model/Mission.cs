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

public delegate void HandleTaskComplete ();
public delegate void HandleRewardComplete ();

public class Mission 
{
	static private int _missionCode;
	static private int GetMissionCode () {
		if (_missionCode == 0) {
			_missionCode = PlayerPrefs.GetInt("AUTO_MISSION_CODE",0);
		}
		_missionCode ++;
		
		// Auto save
		PlayerPrefs.SetInt ("AUTO_MISSION_CODE", _missionCode);
		PlayerPrefs.Save ();
		
		return _missionCode;
	}

	protected int    _id;
	protected string _name;
	protected string _description;
	protected int    _goldValue;
	protected int    _rewardValue;
	protected int    _objectIdGetReward;
	protected MissionType _missionType;

	protected bool  _isIncremental;
	protected int   _goldModifier;
	protected int   _rewardModifier;

	protected MissionTask task;

	#region Properties

	public int id { get { return _id; } }
	public string name { get { return _name; } }
	public string description { get { return _description; } }
	public int goldValue { get { return _goldValue; } }
	public int rewardValue { get { return _rewardValue; } }
	public int objectIdGetReward { get { return _objectIdGetReward; } }
	public MissionType missionType { get { return _missionType; } }

	public bool isIncremental { get { return _isIncremental; } }
	public int goldModifier { get { return _goldModifier; } }
	public int rewardModifier { get { return _rewardModifier; } }

	#endregion Properties

	#region Constructors

	public Mission () 
	{
		_id            = Mission.GetMissionCode();
		_isIncremental = false;
		_objectIdGetReward = -1;
	}

	public Mission (string name, string description, MissionType missionType) 
		: this()
	{
		_name          = name;
		_description   = description;
		_missionType   = missionType;
	}

	public Mission(string name, string description,MissionType missionType, int goldValue, int rewardValue) 
		: this(name, description, missionType)
	{
		_goldValue     = goldValue;
		_rewardValue   = rewardValue;
	}

	public Mission(string name, string description,MissionType missionType, int goldValue, int rewardValue, int objectIdGetReward) 
		: this(name, description, missionType, goldValue, rewardValue)
	{
		_objectIdGetReward = objectIdGetReward;
	}

	public Mission(string name, string description,MissionType missionType, int goldValue, int rewardValue,
	               bool isIncremental,int goldModifier, int rewardModifier) 
		: this (name, description, missionType, goldValue, rewardValue)
	{
		_isIncremental  = isIncremental;
		_goldModifier   = goldModifier;
		_rewardModifier = rewardModifier;
	}	

	#endregion Constructors
}
