using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="CollectAllCardTask")]
public class CollectAllCardTask : MissionTask
{
	public int round { get; private set; }

	public CollectAllCardTask () {}
	public CollectAllCardTask (int round)
		: base (false)
	{
		this.round = round;
	}
	
	#region
	
	override public bool DoTask () 
	{
		PlayGameData lastData = PlayGameData.Instance;
		if (lastData != null) {
			if (lastData.isClearAllARound && lastData.roundClearAll >= round) {
				this.isFinish = true;
			}
		}
		return this.isFinish;
	}
	
	#endregion

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		base.ReadXml (reader);
		reader.MoveToContent ();
		reader.ReadStartElement ();
		this.round = Convert.ToInt32 (reader.ReadElementString ("round"));
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);
		
		writer.WriteElementString ("round", round.ToString ());
	}
	
	#endregion
}