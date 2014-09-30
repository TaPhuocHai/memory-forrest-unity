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
	public SerializableDictionary<CardType, int> cardTypeWithNumberNeedCollect { get; private set;}

	// Danh sach da lay
	public SerializableDictionary<CardType, int> cardTypeWithNumberCollected  { get; private set;}

	#region Constructors

	public CollectTask (bool isAccumulationTask)
		: base (isAccumulationTask)
	{
		cardTypeWithNumberNeedCollect = new SerializableDictionary<CardType, int> ();
		cardTypeWithNumberCollected   = new SerializableDictionary<CardType, int> ();
	}

	public CollectTask () : this (true) {}

	public CollectTask (CardType cardType, int numberToCollect) 
	: this (true) 
	{
		cardTypeWithNumberNeedCollect.Add (cardType, numberToCollect);
	}

	public CollectTask (CardType cardType, int numberToCollect, bool isAccumulationTask) 
		: this (cardType, numberToCollect) 
	{
		this.isAccumulationTask = isAccumulationTask;
	}

	public CollectTask (Dictionary<CardType,int> cardTypeAndNumberCollet, bool isAccumulationTask) 
		: this (isAccumulationTask) 
	{
		foreach (CardType key in cardTypeAndNumberCollet.Keys) {
			this.cardTypeWithNumberNeedCollect[key] = cardTypeAndNumberCollet[key];
		}
	}
	
	#endregion 

	#region Properties
	#endregion

	#region Function

	public void AddTask (CardType cardType, int numberToCollect) 
	{
		cardTypeWithNumberNeedCollect.Add (cardType, numberToCollect);
	}

	override public bool DoTask () 
	{
		PlayGameData lastData = PlayGameData.Instance;

		// Neu la mission tich luy qua cac vong
		if (this.isAccumulationTask) {
			foreach (CardType key in this.cardTypeWithNumberNeedCollect.Keys) {
				// Neu man choi da lay dc loai card ma Mission nay yeu cau
				if (lastData.cardTypeAndNumberCollected.ContainsKey(key)) {
					int savedNumberCollect = 0;
					// Lay gia tri luu truoc do
					if (this.cardTypeWithNumberCollected.ContainsKey(key)) {
						savedNumberCollect = this.cardTypeWithNumberCollected[key];
					}
					savedNumberCollect += lastData.cardTypeAndNumberCollected[key];

					// Luu lai gia tri moi
					this.cardTypeWithNumberCollected[key] = savedNumberCollect;
				}
			}
		} 
		// Neu la loai mission khong tich luy qua cac vong
		else {
			this.cardTypeWithNumberCollected = lastData.cardTypeAndNumberCollected;
		}

		// Kiem tra xem da du yeu cau
		foreach (CardType key in this.cardTypeWithNumberNeedCollect.Keys) {
			if (this.cardTypeWithNumberCollected.ContainsKey(key)) {
				// Neu co 1 loai cardType nao do, ma so luong nhat chu du thi task chua hoan thanh
				if (this.cardTypeWithNumberCollected[key] < this.cardTypeWithNumberNeedCollect[key]) {
					this.isFinish = false;
					return false;
				}
			} 
			// Neu co cardType ma chua duoc nhac thi task chua hoan thanh
			else {
				this.isFinish = false;
				return false;
			}
		}

		this.isFinish = true;
		return true;
	}

	#endregion
	
	#region IXmlSerializable
	
	override public void ReadXml(System.Xml.XmlReader reader)
	{
		reader.MoveToContent ();

		// CollectTask tag
		base.ReadXml (reader);

		reader.ReadStartElement ();

		reader.ReadStartElement ("CardTypeAndNumberPair");
		XmlSerializer serializer = new XmlSerializer (typeof (SerializableDictionary<CardType, int>));
		this.cardTypeWithNumberNeedCollect = (SerializableDictionary<CardType, int>) serializer.Deserialize (reader);
		reader.ReadEndElement ();

		reader.ReadStartElement ("CardTypeAndNumberPairColleted");
		XmlSerializer serializerCollected = new XmlSerializer (typeof (SerializableDictionary<CardType, int>));
		this.cardTypeWithNumberCollected = (SerializableDictionary<CardType, int>) serializerCollected.Deserialize (reader);
		reader.ReadEndElement ();

		reader.ReadEndElement ();
	}
	
	override public void WriteXml(System.Xml.XmlWriter writer)
	{
		base.WriteXml (writer);

		writer.WriteStartElement ("CardTypeAndNumberPair");
		XmlSerializer serializer = new XmlSerializer (this.cardTypeWithNumberNeedCollect.GetType ());
		serializer.Serialize (writer, this.cardTypeWithNumberNeedCollect);
		writer.WriteEndElement ();

		writer.WriteStartElement ("CardTypeAndNumberPairColleted");
		XmlSerializer serializerCollected = new XmlSerializer (this.cardTypeWithNumberCollected.GetType ());
		serializerCollected.Serialize (writer, this.cardTypeWithNumberCollected);
		writer.WriteEndElement ();
	}
	
	#endregion
}