using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="PointTask")]
public class PointTask : MissionTask
{
	public int point { get; private set; }
	
	public PointTask () {}
	public PointTask (int point)
		: base (false)
	{
		this.point = point;
	}
	public PointTask (int point, bool isAccumulationTask)
		: base (isAccumulationTask)
	{
		this.point = point;
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
		this.point = Convert.ToInt32 (reader.ReadElementString ("point"));
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);		
		writer.WriteElementString ("point", point.ToString ());
	}
	
	#endregion
}