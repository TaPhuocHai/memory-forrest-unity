using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="CollectTask")]
public class CollectTask : MissionTask
{
	/// <summary>
	/// cardType : so luong
	/// </summary>
	/// <value>The card type with number pair need collect.</value>
	//public Dictionary<int, int> cardTypeWithNumberPairNeedCollect { get; private set;}
	public SerializableDictionary<string, int> cardTypeWithNumberPairNeedCollect { get; private set;}

	#region Constructors

	public CollectTask (bool isAccumulationTask)
		: base (isAccumulationTask)
	{
		cardTypeWithNumberPairNeedCollect = new SerializableDictionary<string, int> ();
	}

	public CollectTask () : this (true) {}

	public CollectTask (CardType cardType, int pair) 
	: this (true) 
	{
		cardTypeWithNumberPairNeedCollect.Add (cardType.ToString(), pair);
	}

	public CollectTask (CardType cardType, int pair, bool isAccumulationTask) 
		: this (cardType, pair) 
	{
		this.isAccumulationTask = isAccumulationTask;
	}

	public CollectTask (Dictionary<string,int> cardTypeAndNumberPair, bool isAccumulationTask) 
		: this (isAccumulationTask) 
	{
		foreach (string key in cardTypeAndNumberPair.Keys) {
			this.cardTypeWithNumberPairNeedCollect[key] = cardTypeAndNumberPair[key];
		}
	}
	
	#endregion 

	#region Properties
	#endregion

	#region Function

	public void AddTask (CardType cardType, int pair) 
	{
		cardTypeWithNumberPairNeedCollect.Add (cardType.ToString(), pair);
	}

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

		cardTypeWithNumberPairNeedCollect = new SerializableDictionary<string, int> ();

		reader.ReadStartElement ("CardTypeAndNumberPair");
		XmlSerializer serializer = new XmlSerializer (cardTypeWithNumberPairNeedCollect.GetType());
		this.cardTypeWithNumberPairNeedCollect = (SerializableDictionary<string, int>) serializer.Deserialize (reader);
		reader.ReadEndElement ();

		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);

		writer.WriteStartElement ("CardTypeAndNumberPair");
		XmlSerializer serializer = new XmlSerializer (this.cardTypeWithNumberPairNeedCollect.GetType ());
		serializer.Serialize (writer, this.cardTypeWithNumberPairNeedCollect);
		writer.WriteEndElement ();
	}
	
	#endregion
}