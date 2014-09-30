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
	/// Do the task.
	/// </summary>
	/// <returns><c>true</c>, if task was done, <c>false</c> otherwise.</returns>
	public virtual bool DoGetReward () {
		return true;
	}

	/// <summary>
	/// Do the undo get reward.
	/// Chi thuc hien nay nay khi Constant.kClearMissionData = true
	/// </summary>
	public virtual void DoUndoGetReward () {
	}

	#region IXmlSerializable
	
	public System.Xml.Schema.XmlSchema GetSchema() { return null; }	
	virtual public void ReadXml(System.Xml.XmlReader reader) {}	
	virtual public void WriteXml(System.Xml.XmlWriter writer) {
	}
	
	#endregion
}