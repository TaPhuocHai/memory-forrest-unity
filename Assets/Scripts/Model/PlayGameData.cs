using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

// Luu thong tin du lieu user choi trong 1 game
[XmlRoot(ElementName="PlayGameData")]
public class PlayGameData : IXmlSerializable
{
	#region Properties

	public RegionType regionType { get; set; }
	public bool       isClearAllARound { get; set; }
	public int        roundClearAll { get; set; }
	public int        lastRound { get; set; }
	public int        score { get; set; }

	// So luong card da collect : cardType : so luong
	public SerializableDictionary<CardType, int> cardTypeAndNumberCollected { get; set;}

	static PlayGameData _instance;
	public static PlayGameData Instance {
		get { 
			if (_instance == null) {
				_instance = new PlayGameData ();
			}
			return _instance;
		}
	}

	#endregion 

	#region Constructors
	
	public PlayGameData () 
	{
		this.cardTypeAndNumberCollected = new SerializableDictionary<CardType, int> ();
	}
	public PlayGameData (RegionType regionType) 
		: this()
	{
		this.regionType = regionType;
	}

	#endregion

	public void CollectCard (CardType cardType, int value) 
	{
		int savedCollect = 0;
		if (this.cardTypeAndNumberCollected.ContainsKey (cardType)) {
			savedCollect = this.cardTypeAndNumberCollected[cardType];
		}
		savedCollect += value;
		this.cardTypeAndNumberCollected [cardType] = savedCollect;
	}

	public void Reset () 
	{
		this.isClearAllARound = false;
		this.roundClearAll = -1;
		this.score = 0;
		this.cardTypeAndNumberCollected = new SerializableDictionary<CardType, int> ();
	}

	public bool Save ()
	{
		if (UnityXMLSerializer.SerializeToXMLFile<PlayGameData> (Application.persistentDataPath + "/playgamedata.xml", this, true)) {
			Debug.Log ("Save gameplaydata success");
			return true;
		} else {
			Debug.Log ("Save gameplaydata faild");
		}
		return false;
	}

//	public static PlayGameData GetLastSave () 
//	{
//		PlayGameData data = UnityXMLSerializer.DeserializeFromXMLFile<PlayGameData> (Application.persistentDataPath + "/playgamedata.xml");
//		return data;
//	}

	#region IXmlSerializable
	
	public System.Xml.Schema.XmlSchema GetSchema() { return null; }
	
	virtual public void ReadXml(System.Xml.XmlReader reader)
	{	
		reader.MoveToContent();

		string regionStr = reader.GetAttribute ("regionType");
		this.regionType = (RegionType)Enum.Parse (typeof(RegionType), regionStr);
		this.isClearAllARound = Convert.ToBoolean (reader.GetAttribute ("isClearAllARound"));
		this.roundClearAll = Convert.ToInt32 (reader.GetAttribute ("roundClearAll"));
		this.score = Convert.ToInt32 (reader.GetAttribute ("score"));
		this.lastRound = Convert.ToInt32 (reader.GetAttribute ("lastRound"));

		reader.ReadStartElement ();

		XmlSerializer serializer = new XmlSerializer (typeof(SerializableDictionary<CardType, int>));
		this.cardTypeAndNumberCollected = (SerializableDictionary<CardType, int>) serializer.Deserialize (reader);

		reader.ReadEndElement ();
	}
	
	virtual public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString ("regionType", this.regionType.ToString());
		writer.WriteAttributeString ("isClearAllARound", this.isClearAllARound.ToString());
		writer.WriteAttributeString ("roundClearAll", this.roundClearAll.ToString());
		writer.WriteAttributeString ("score", this.score.ToString());
		writer.WriteAttributeString ("lastRound", this.lastRound.ToString());

		XmlSerializer serializer = new XmlSerializer (this.cardTypeAndNumberCollected.GetType ());
		serializer.Serialize (writer, this.cardTypeAndNumberCollected);
	}
	
	#endregion
}
