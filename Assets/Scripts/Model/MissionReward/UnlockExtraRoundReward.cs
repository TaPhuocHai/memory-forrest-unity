using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="UnlockExtraRoundReward")]
public class UnlockExtraRoundReward : MissionReward
{
	public int roundToUnlock { get; set; }

	public UnlockExtraRoundReward () {}
	public UnlockExtraRoundReward (int roundToUnlock)
	{
		this.roundToUnlock = roundToUnlock;
	}
	
	override public bool DoGetReward () 
	{
		return true;
	}

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		base.ReadXml (reader);
		reader.MoveToContent ();
		reader.ReadStartElement ();
		this.roundToUnlock = Convert.ToInt32 (reader.ReadElementString ("roundToUnlock"));
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);
		
		writer.WriteElementString ("roundToUnlock", roundToUnlock.ToString ());
	}
	
	#endregion
}