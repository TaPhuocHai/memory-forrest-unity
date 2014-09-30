using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="MoreTimeReward")]
public class MoreTimeReward : MissionReward
{
	#region Properties

	public int second { get; private set; }

	#endregion

	#region Constructors

	public MoreTimeReward () {}
	public MoreTimeReward (int second)
	{
		this.second = second;
	}

	#endregion

	#region Functions

	override public bool DoGetReward () 
	{
		Player.secondTimePlay = Player.secondTimePlay + second;
		Debug.Log ("MoreTimeReward : " + this.second.ToString());
		return true;
	}

	#endregion

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		base.ReadXml (reader);
		reader.MoveToContent ();
		reader.ReadStartElement ();
		this.second = Convert.ToInt32 (reader.ReadElementString ("second"));
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);

		writer.WriteElementString ("second", second.ToString ());
	}
	
	#endregion
}