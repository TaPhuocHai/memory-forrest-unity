using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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

	protected int           _id;
	protected string        _name;
	protected string        _description;
	protected MissionTask   _missionTask;
	protected MissionReward _missionReward;

	protected bool  _isIncremental;
	protected int   _goldModifier;
	protected int   _rewardModifier;

	#region Properties

	public int id { get { return _id; } }
	public string name { get { return _name; } }
	public string description { get { return _description; } }
	public MissionTask missionTask { get { return _missionTask; } }
	public MissionReward missionReward { get { return _missionTask; } }

	public bool isIncremental { get { return _isIncremental; } }
	public int goldModifier { get { return _goldModifier; } }
	public int rewardModifier { get { return _rewardModifier; } }

	#endregion Properties

	#region Constructors

	public Mission () 
	{
		_id            = Mission.GetMissionCode();
		_isIncremental = false;
	}

	public Mission (string name, string description, MissionTask missionTask, MissionReward missionReward) 
		: this()
	{
		_name          = name;
		_description   = description;
		_missionTask = missionTask;
		_missionReward = missionReward;
	}

	public Mission(string name, string description,MissionTask missionTask, MissionReward missionReward,
	               bool isIncremental,int goldModifier, int rewardModifier) 
		: this (name, description, missionTask, missionReward)
	{
		_isIncremental  = isIncremental;
		_goldModifier   = goldModifier;
		_rewardModifier = rewardModifier;
	}	

	#endregion Constructors
}
