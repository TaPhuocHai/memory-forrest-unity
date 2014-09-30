using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="UnlockExtraRoundReward")]
public class UnlockExtraRoundReward : MissionReward
{
	public RegionType regionType    { get; private set; }
	public int        roundToUnlock { get; private set; }

	public UnlockExtraRoundReward () {}
	public UnlockExtraRoundReward (RegionType regionType, int roundToUnlock)
	{
		this.regionType = regionType;
		this.roundToUnlock = roundToUnlock;
	}
	
	override public bool DoGetReward () 
	{
		Debug.Log ("UnlockExtraRoundReward :" + this.roundToUnlock.ToString());
		Region.UnlockRound (regionType,roundToUnlock, true);
		return true;
	}

	override public void DoUndoGetReward () 
	{
		if (!Constant.kClearMissionData) {
			return;
		}
		Region.UnlockRound (regionType,roundToUnlock, false);
	}

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		base.ReadXml (reader);
		reader.MoveToContent ();
		reader.ReadStartElement ();
		string regionStr = reader.ReadElementString ("regionType");
		this.regionType = (RegionType)Enum.Parse (typeof(RegionType), regionStr);
		this.roundToUnlock = Convert.ToInt32 (reader.ReadElementString ("roundToUnlock"));
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);

		writer.WriteElementString ("regionType", regionType.ToString ());
		writer.WriteElementString ("roundToUnlock", roundToUnlock.ToString ());
	}
	
	#endregion
}