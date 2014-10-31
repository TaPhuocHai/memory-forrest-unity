using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="CoinReward")]
public class CoinReward : MissionReward
{
	public int coin { get; set; }

	public CoinReward () {}
	public CoinReward (int coin)
	{
		this.coin = coin;
	}
	
	override public bool DoGetReward () 
	{
		Player.Coin = Player.Coin + coin;
		Debug.Log ("CoinReward : " + this.coin.ToString());
		return true;
	}

	override public void DoUndoGetReward () 
	{
		Player.Coin = 0;
	}

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