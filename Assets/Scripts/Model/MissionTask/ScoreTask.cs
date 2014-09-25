using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="ScoreTask")]
public class ScoreTask : MissionTask
{
	public int score { get; private set; }

	public ScoreTask () {}
	public ScoreTask (int score)
		: base (false)
	{
		this.score = score;
	}

	#region
	
	override public bool DoTask () 
	{
		return false;
	}
	
	#endregion

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		reader.MoveToContent ();
		
		// CollectTask tag
		base.ReadXml (reader);
		
		reader.ReadStartElement ();
		this.score = Convert.ToInt32 (reader.ReadElementString ("score"));
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);		
		writer.WriteElementString ("score", score.ToString ());
	}
	
	#endregion
}