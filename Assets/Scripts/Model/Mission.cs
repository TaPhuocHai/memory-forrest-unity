using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

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
	public string name  { get; private set; }
	public string description  { get; private set; }

	public bool isIncremental  { get; private set; }
	public int  goldModifier   { get; private set; }
	public int  rewardModifier { get; private set; }
	
	public MissionTask   missionTask   { get; private set; }
	public MissionReward missionReward { get; private set; }

	#endregion Properties

	#region Constructors

	public Mission () 
	{
		this.id            = Mission.GetMissionCode();
		this.isIncremental = false;
	}

	public Mission (string name, string description, MissionTask missionTask, MissionReward missionReward) 
		: this()
	{
		this.name          = name;
		this.description   = description;

		this.missionTask   = missionTask;
		this.missionReward = missionReward;
	}

	public Mission(string name, string description,MissionTask missionTask, MissionReward missionReward,
	               bool isIncremental,int goldModifier, int rewardModifier) 
		: this (name, description, missionTask, missionReward)
	{
		this.isIncremental  = isIncremental;
		this.goldModifier   = goldModifier;
		this.rewardModifier = rewardModifier;
	}	

	#endregion Constructors

	public System.Xml.Schema.XmlSchema GetSchema() { return null; }

	public void ReadXml(System.Xml.XmlReader reader)
	{
		reader.MoveToContent();

		this.id   = Convert.ToInt32 (reader.GetAttribute ("id"));			
		reader.ReadStartElement ();

		this.name = reader.ReadElementString ("name");
		this.description = reader.ReadElementString ("description");
		string incrementalValue = reader.ReadElementString ("isIncremental");
		this.isIncremental = Convert.ToBoolean (incrementalValue);
		this.goldModifier  = Convert.ToInt32 (reader.ReadElementString ("goldModifier"));
		this.rewardModifier = Convert.ToInt32 (reader.ReadElementString ("rewardModifier"));

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
		writer.WriteElementString ("name", this.name);
		writer.WriteElementString ("description", this.description);
		writer.WriteElementString ("isIncremental", this.isIncremental.ToString());
		writer.WriteElementString ("goldModifier", this.goldModifier.ToString());
		writer.WriteElementString ("rewardModifier", this.rewardModifier.ToString());

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
}
