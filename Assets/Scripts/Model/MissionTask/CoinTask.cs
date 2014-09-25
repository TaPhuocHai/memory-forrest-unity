using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="CoinTask")]
public class CoinTask : MissionTask
{
	public int coin { get; private set; }

	public CoinTask () {}
	public CoinTask (int coin)
		: base (false)
	{
		this.coin = coin;
	}
	
	#region
	
	override public bool DoTask () 
	{
		if (PlayerData.Coin >= coin) {
			return true;
		}
		return false;
	}
	
	#endregion

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		base.ReadXml (reader);
		reader.MoveToContent ();
		reader.ReadStartElement ();
		this.coin = Convert.ToInt32 (reader.ReadElementString ("coin"));
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);
		
		writer.WriteElementString ("coin", coin.ToString ());
	}
	
	#endregion
}