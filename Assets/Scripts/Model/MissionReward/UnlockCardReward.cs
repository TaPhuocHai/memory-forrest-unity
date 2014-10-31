using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="UnlockCardReward")]
public class UnlockCardReward : MissionReward
{
	public CardType cardType { get; set; }
	public RegionType regionType { get; set; }

	public UnlockCardReward () {}
	public UnlockCardReward (CardType cardType, RegionType regionType)
	{
		this.cardType = cardType;
		this.regionType = regionType;
	}
	
	override public bool DoGetReward () 
	{
		Card.Unlock (cardType, regionType, true);
		Debug.Log ("UnlockCardReward : " + cardType.ToString());
		return true;
	}

	override public void DoUndoGetReward () 
	{
		if (!Constant.kClearRewardData) {
			return;
		}
		Card.Unlock (cardType,regionType,false);
	}

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		base.ReadXml (reader);
		reader.MoveToContent ();
		reader.ReadStartElement ();
		string cardStr = reader.ReadElementString ("cardType");
		this.cardType = (CardType)Enum.Parse (typeof(CardType), cardStr);
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);
		
		writer.WriteElementString ("cardType", cardType.ToString ());
	}
	
	#endregion
}