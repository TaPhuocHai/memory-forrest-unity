using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="MissionReward")]
public class MissionReward : IXmlSerializable
{

	public MissionReward () {}

	/// <summary>
	/// Dos the task.
	/// </summary>
	/// <returns><c>true</c>, if task was done, <c>false</c> otherwise.</returns>
	public virtual bool DoGetReward () {
		return true;
	}

	#region IXmlSerializable
	
	public System.Xml.Schema.XmlSchema GetSchema() { return null; }	
	virtual public void ReadXml(System.Xml.XmlReader reader) {}	
	virtual public void WriteXml(System.Xml.XmlWriter writer) {
	}
	
	#endregion
}