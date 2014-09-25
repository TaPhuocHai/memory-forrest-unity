using UnityEngine;
using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;

// Luu thong tin du lieu play trong 1 game
[XmlRoot(ElementName="PlayGameData")]
public class PlayGameData : IXmlSerializable
{
	#region Properties

	public RegionType regionType { get; set; }
	public bool       isClearAllARound { get; set; }
	public int        roundClearAll { get; set; }
	public int        score { get; set; }

	// So luong card da collect : cardType : so luong
	public SerializableDictionary<string, int> cardTypeAndNumberCollected { get; set;}

	#endregion 

	#region Constructors
	
	public PlayGameData () 
	{
		this.cardTypeAndNumberCollected = new SerializableDictionary<string, int> ();
	}
	public PlayGameData (RegionType regionType) 
		: this()
	{
		this.regionType = regionType;
	}

	#endregion

	public bool Save ()
	{
		if (UnityXMLSerializer.SerializeToXMLFile<PlayGameData> (Application.persistentDataPath + "_playgamedata.xml", this, true)) {
			Debug.Log ("Save gameplaydata success");
			return true;
		} else {
			Debug.Log ("Save gameplaydata faild");
		}
		return false;
	}

	public static PlayGameData GetLastSave () 
	{
		PlayGameData data = UnityXMLSerializer.DeserializeFromXMLFile<PlayGameData> (Application.persistentDataPath + "_playgamedata.xml");
		return data;
	}

	#region IXmlSerializable
	
	public System.Xml.Schema.XmlSchema GetSchema() { return null; }
	
	virtual public void ReadXml(System.Xml.XmlReader reader)
	{	
		reader.MoveToContent();

		string regionStr = reader.ReadElementString ("regionType");
		this.regionType = (RegionType)Enum.Parse (typeof(RegionType), regionStr);
		this.isClearAllARound = Convert.ToBoolean (reader.ReadElementString ("isClearAllARound"));
		this.roundClearAll = Convert.ToInt32 (reader.ReadElementString ("roundClearAll"));
		this.score = Convert.ToInt32 (reader.ReadElementString ("score"));

		XmlSerializer serializer = new XmlSerializer (typeof(SerializableDictionary<string, int>));
		this.cardTypeAndNumberCollected = (SerializableDictionary<string, int>) serializer.Deserialize (reader);

		reader.ReadStartElement ();
		reader.ReadEndElement ();
	}
	
	virtual public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString ("regionType", this.regionType.ToString());
		writer.WriteAttributeString ("isClearAllARound", this.isClearAllARound.ToString());
		writer.WriteAttributeString ("roundClearAll", this.roundClearAll.ToString());
		writer.WriteAttributeString ("score", this.score.ToString());

		XmlSerializer serializer = new XmlSerializer (this.cardTypeAndNumberCollected.GetType ());
		serializer.Serialize (writer, this.cardTypeAndNumberCollected);
	}
	
	#endregion
}
