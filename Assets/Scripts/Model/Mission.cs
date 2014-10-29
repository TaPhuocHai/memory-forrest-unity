using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

/// <summary>
/// Mission text
/// Using @D to replace a int value
/// </summary>
[XmlRoot(ElementName="MissionText")]
public class MissionText : IXmlSerializable
{
	#region Properties
	public string text  { get ; private set; }
	public int    value { get ; private set; }
	public string code  { get ; private set; }
	#endregion

	public MissionText () {}
	public MissionText (string text) 
	{
		this.text = text;
	}

	public MissionText (MissionText missionText) 
	{
		this.text = missionText.text;
		this.value = missionText.value;
		if (missionText.code != null) {
			this.code = missionText.code;
		}
	}

	public MissionText (string code, int value) 
	{
		this.code = code;
		this.value = value;
		this.text = code.Replace ("@D", value.ToString ());
	}

	#region IXmlSerializable
	
	public System.Xml.Schema.XmlSchema GetSchema() { return null; }
	
	virtual public void ReadXml(System.Xml.XmlReader reader)
	{	
		reader.MoveToContent ();
		reader.ReadStartElement ();

		this.text  = reader.ReadElementString ("text");
		this.value = Convert.ToInt32 (reader.ReadElementString ("value"));
		this.code = reader.ReadElementString ("code");

		reader.ReadEndElement ();
	}
	
	virtual public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteElementString ("text", this.text);
		writer.WriteElementString ("value", this.value.ToString());
		writer.WriteElementString ("code", this.code);
	}
	
	#endregion
}

public class Mission : IXmlSerializable
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

	#region Properties

	public int    id    { get; private set; }
	public MissionText name  { get; private set; }
	public MissionText description  { get; private set; }
	public MissionText rewardMessage  { get; private set; }

	public bool isIncremental  { get; private set; }
	public int  goldModifier   { get; private set; }
	public int  rewardModifier { get; private set; }

	public bool  isFinish { get; private set;}
	public bool  isTaskFinish {
		get {
			if (this.missionTask != null) {
				return this.missionTask.isFinish;
			}
			return false;
		}
	}
	
	public MissionTask   missionTask   { get; private set; }
	public MissionReward missionReward { get; private set; }

	public Sprite thumbnail {
		get {
			string thumbnailPath = null;
			if (missionReward.GetType() == typeof(AdditionPointReward)) {
				thumbnailPath = "Textures/Reward/MorePointReward";
			} else if (missionReward.GetType() == typeof(CoinReward)) {
				thumbnailPath = "Textures/Reward/CoinReward";
			} else if (missionReward.GetType() == typeof(MoreTimeReward)) {
				thumbnailPath = "Textures/Reward/MoreTimeReward";
			} else if (missionReward.GetType() == typeof(UnlockCardReward)) {
				thumbnailPath = "Textures/Reward/UnlockCardReward";
			} else if (missionReward.GetType() == typeof(UnlockExtraRoundReward)) {
				thumbnailPath = "Textures/Reward/NewRoundReward";
			}
			return (Sprite)Resources.Load(thumbnailPath,typeof(Sprite));
		}
	}

	#endregion Properties

	#region Constructors

	public Mission () 
	{
		this.id            = Mission.GetMissionCode();
		this.isIncremental = false;
	}

	public Mission (string name, string description, string rewardMessage, MissionTask missionTask, MissionReward missionReward) 
		: this()
	{
		this.name          = new MissionText (name);
		this.description   = new MissionText (description);
		this.rewardMessage = new MissionText (rewardMessage);

		this.missionTask   = missionTask;
		this.missionReward = missionReward;
	}

	public Mission (MissionText name, MissionText description, MissionText rewardMessage, MissionTask missionTask, MissionReward missionReward) 
		: this()
	{
		this.name          = name;
		this.description   = description;
		this.rewardMessage = rewardMessage;
		
		this.missionTask   = missionTask;
		this.missionReward = missionReward;
	}

	public Mission(string name, string description, string rewardMessage, MissionTask missionTask, MissionReward missionReward,
	               bool isIncremental,int goldModifier, int rewardModifier) 
		: this (name, description,rewardMessage, missionTask, missionReward)
	{
		this.isIncremental  = isIncremental;
		this.goldModifier   = goldModifier;
		this.rewardModifier = rewardModifier;
	}

	public Mission(MissionText name, MissionText description, MissionText rewardMessage,
	               MissionTask missionTask, MissionReward missionReward,
	               bool isIncremental,int goldModifier, int rewardModifier) 
		: this (name, description,rewardMessage, missionTask, missionReward)
	{
		this.isIncremental  = isIncremental;
		this.goldModifier   = goldModifier;
		this.rewardModifier = rewardModifier;
	}

	#endregion 

	#region Function

	public void UpdateMission () 
	{
		if (this.isTaskFinish) { return;}
		if (this.isFinish) { return; }

		if (this.missionTask != null) {
			if (this.missionTask.DoTask ()) {
				Debug.Log ("Mission : " + this.name.text + " task complete");
			}
		}
	}

	public bool GetReward () 
	{
		if (this.missionReward != null) {
			this.isFinish = this.missionReward.DoGetReward();
		}
		return this.isFinish;
	}

	public Mission CreateIncrementalMission () 
	{
		if (!this.isIncremental) {
			return null;
		}

		MissionText newName;
		if (this.name.code != null && this.name.code.Length > 0) {
			newName = new MissionText (this.name.code, this.name.value + this.goldModifier);
		} else {
			newName = new MissionText (this.name);
		}

		MissionText newDescription;
		if (this.description.code != null && this.description.code.Length > 0) {
			newDescription = new MissionText (this.description.code, this.description.value + this.rewardModifier);
		} else {
			newDescription = new MissionText (this.description);
		}

		MissionText newRewardMessage;
		if (this.rewardMessage.code != null && this.rewardMessage.code.Length > 0) {
			newRewardMessage = new MissionText (this.rewardMessage.code, this.rewardMessage.value + this.rewardModifier);
		} else {
			newRewardMessage = new MissionText (this.rewardMessage);
		}

		// Incremental task
		MissionTask newTask = null;
		if (this.missionTask.GetType () == typeof(CoinTask)) {
			CoinTask oldTaks = (CoinTask)this.missionTask;
			newTask = new CoinTask(oldTaks.coin + this.goldModifier);
		} else if (this.missionTask.GetType () == typeof(CollectAllCardTask)) {
			CollectAllCardTask oldTaks = (CollectAllCardTask)this.missionTask;
			newTask = new CollectAllCardTask(oldTaks.round + this.goldModifier);
		} else if (this.missionTask.GetType () == typeof(CollectTask)) {
			CollectTask oldTaks = (CollectTask)this.missionTask;
			SerializableDictionary<CardType, int> oldDicCardTypeAndNumber = oldTaks.cardTypeWithNumberNeedCollect;
			SerializableDictionary<CardType, int> newDic = new SerializableDictionary<CardType, int>();
			foreach (CardType key in oldDicCardTypeAndNumber.Keys) {
				newDic[key] = oldDicCardTypeAndNumber[key] + this.goldModifier;
			}
			newTask = new CollectTask(newDic,oldTaks.isAccumulationTask);
		} else if (this.missionTask.GetType () == typeof(ScoreTask)) {
			ScoreTask oldTaks = (ScoreTask)this.missionTask;
			newTask = new ScoreTask(oldTaks.score + this.goldModifier,oldTaks.isAccumulationTask);
		}

		// Incremental reward
		MissionReward newReward = null;
		if (this.missionReward.GetType () == typeof(AdditionPointReward)) {
			AdditionPointReward oldReward = (AdditionPointReward)this.missionReward;
			newReward = new AdditionPointReward(oldReward.cardType, oldReward.pointToAddMore + this.rewardModifier);
		} else if (this.missionReward.GetType () == typeof(CoinReward)) {
			CoinReward oldReward = (CoinReward)this.missionReward;
			newReward = new CoinReward (oldReward.coin + this.rewardModifier);
		} else if (this.missionReward.GetType () == typeof(MoreTimeReward)) {
			MoreTimeReward oldReward = (MoreTimeReward)this.missionReward;
			newReward = new MoreTimeReward (oldReward.second + this.rewardModifier);
		} else if (this.missionReward.GetType () == typeof(UnlockCardReward)) {
			// Loai nay khong co tu dong tang
		} else if (this.missionReward.GetType () == typeof(UnlockExtraRoundReward)) {
			UnlockExtraRoundReward oldReward = (UnlockExtraRoundReward)this.missionReward;
			newReward = new UnlockExtraRoundReward (oldReward.regionType, oldReward.roundToUnlock + this.rewardModifier);
		}

		// Tao mission
		Mission newMission = null;
		if (newTask != null && newReward != null) {
			newMission = new Mission(newName, newDescription,
			                         newRewardMessage,
			                         newTask,
			                         newReward,
			                         this.isIncremental,this.goldModifier, this.rewardModifier);
		}

		return newMission;
	}

	/// <summary>
	/// Sets the complete mission.
	/// Ham nay chi co gia tri khi Constant.kDebugMission = true
	/// </summary>
	/// <param name="isComplete">If set to <c>true</c> is complete.</param>
	public void SetCompleteTask (bool isComplete) 
	{
		if (!Constant.kDebugMission) {
			return;
		}
		Debug.Log ("set mission : " + this.id.ToString () + " complete");
		this.missionTask.SetCompleteTask (isComplete);
	}

	/// <summary>
	/// Clears the unlock mission.
	/// Ham nay chi co gia tri khi Constant.kClearMissionData = true
	/// </summary>
	public void ClearRewardData () 
	{
		if (!Constant.kClearRewardData) {
			return;
		}
		this.isFinish = false;
		this.missionReward.DoUndoGetReward ();
	}

	#endregion

	#region IXmlSerializable

	public System.Xml.Schema.XmlSchema GetSchema() { return null; }

	public void ReadXml(System.Xml.XmlReader reader)
	{
		reader.MoveToContent();

		this.id   = Convert.ToInt32 (reader.GetAttribute ("id"));			
		reader.ReadStartElement ();

		this.name = (MissionText)new XmlSerializer (typeof(MissionText)).Deserialize (reader);
		this.description = (MissionText)new XmlSerializer (typeof(MissionText)).Deserialize (reader);
		this.rewardMessage = (MissionText)new XmlSerializer (typeof(MissionText)).Deserialize (reader);

		string incrementalValue = reader.ReadElementString ("isIncremental");
		this.isIncremental = Convert.ToBoolean (incrementalValue);
		this.goldModifier  = Convert.ToInt32 (reader.ReadElementString ("goldModifier"));
		this.rewardModifier = Convert.ToInt32 (reader.ReadElementString ("rewardModifier"));
		this.isFinish = Convert.ToBoolean (reader.ReadElementString ("isFinsh"));

		reader.ReadStartElement ("MissionTask");
		string typeAttribTask = reader.Name;

		Type typeTask = Type.GetType(typeAttribTask);
		if (typeTask != null) {
			this.missionTask = (MissionTask) new XmlSerializer(typeTask).Deserialize(reader);
		}
		reader.ReadEndElement ();

		reader.ReadStartElement ("MissionReward");
		string typeAttribReward = reader.Name;

		Type typeReward = Type.GetType(typeAttribReward);
		if (typeReward != null) {
			this.missionReward = (MissionReward) new XmlSerializer(typeReward).Deserialize(reader);
		}
		reader.ReadEndElement ();

		reader.ReadEndElement ();
	}
	
	public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString ("id", this.id.ToString());

		XmlSerializer nameSerializer = new XmlSerializer (typeof(MissionText));
		nameSerializer.Serialize (writer, this.name);

		XmlSerializer descriptionSerializer = new XmlSerializer (typeof(MissionText));
		descriptionSerializer.Serialize (writer, this.description);

		XmlSerializer rewardMessageSerializer = new XmlSerializer (typeof(MissionText));
		rewardMessageSerializer.Serialize (writer, this.rewardMessage);

		writer.WriteElementString ("isIncremental", this.isIncremental.ToString());
		writer.WriteElementString ("goldModifier", this.goldModifier.ToString());
		writer.WriteElementString ("rewardModifier", this.rewardModifier.ToString());
		writer.WriteElementString ("isFinsh", this.isFinish.ToString());

		if (this.missionTask != null) {
			writer.WriteStartElement ("MissionTask");
			XmlSerializer taskSerializer = new XmlSerializer (this.missionTask.GetType ());
			taskSerializer.Serialize (writer, this.missionTask);
			writer.WriteEndElement ();
		}

		if (this.missionReward != null) {
			writer.WriteStartElement ("MissionReward");
			XmlSerializer rewardSerialize = new XmlSerializer (this.missionReward.GetType ());
			rewardSerialize.Serialize (writer, this.missionReward);
			writer.WriteEndElement ();
		}
	}

	#endregion
}
