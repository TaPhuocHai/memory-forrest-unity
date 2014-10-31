using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="AdditionPointReward")]
public class AdditionPointReward : MissionReward
{
	public CardType cardType { get; private set; }
	public int      pointToAddMore { get; private set; }

	public AdditionPointReward () {}
	public AdditionPointReward (CardType cardType, int pointToAddMore)
	{
		this.cardType = cardType;
		this.pointToAddMore = pointToAddMore;
	}
	
	override public bool DoGetReward () 
	{
		Debug.Log ("AdditionPointReward : " + this.pointToAddMore.ToString() + " to card " + this.cardType.ToString());
		int oldPoint = Card.GetPoint (cardType);
		int newPoint = oldPoint + pointToAddMore;
		Card.SavePoint (newPoint, cardType);
		return true;
	}

	override public void DoUndoGetReward () 
	{
		Card.SavePoint (0, cardType);
	}

	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		base.ReadXml (reader);
		reader.MoveToContent ();
		reader.ReadStartElement ();
		this.pointToAddMore = Convert.ToInt32 (reader.ReadElementString ("pointToAddMore"));
		string cardStr = reader.ReadElementString ("cardType");
		this.cardType = (CardType)Enum.Parse (typeof(CardType), cardStr);
		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);
		
		writer.WriteElementString ("pointToAddMore", pointToAddMore.ToString ());
		writer.WriteElementString ("cardType", cardType.ToString ());
	}
	
	#endregion
}