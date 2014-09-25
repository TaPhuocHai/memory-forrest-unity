using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

[XmlRoot(ElementName="MissionTask")]
public abstract class MissionTask : IXmlSerializable
{
	#region Properties
	public bool isAccumulationTask { get; protected set; }
	public bool isFinish           { get; protected set; }
	#endregion

	public MissionTask () {}
	public MissionTask (bool isAccumulationTask)
	{
		this.isAccumulationTask = isAccumulationTask;
	}
	
	/// <summary>
	/// Dos the task.
	/// </summary>
	/// <returns><c>true</c>, if task was done, <c>false</c> otherwise.</returns>
	public virtual bool DoTask () 
	{
		return false;
	}

	#region IXmlSerializable

	public System.Xml.Schema.XmlSchema GetSchema() { return null; }

	virtual public void ReadXml(System.Xml.XmlReader reader)
	{	
		this.isAccumulationTask = Convert.ToBoolean (reader.GetAttribute ("isAccumulationTask"));
		this.isFinish =  Convert.ToBoolean (reader.GetAttribute ("isFinish"));
	}
	
	virtual public void WriteXml(System.Xml.XmlWriter writer)
	{
		writer.WriteAttributeString ("isAccumulationTask", this.isAccumulationTask.ToString());
		writer.WriteAttributeString ("isFinish", this.isFinish.ToString());
	}

	#endregion
}