using UnityEngine;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System;

public static class UnityXMLSerializer
{	
	/// <summary>
	/// Serialize an object to an xml file.
	/// </summary>
	/// <returns>
	/// True if written, false if file exists and overWrite is false
	/// </returns>
	/// <param name='writePath'>
	/// Where to write the file.  Consider using Application.PersistentData
	/// </param>
	/// <param name='serializableObject'>
	/// The Object to be serialized.  The generic needs to be the type of the object to be serialized
	/// </param>
	/// <param name='overWrite'>
	/// If set to <c>true</c> over write the file if it exists.
	/// </param>
	public static bool SerializeToXMLFile<T>(string writePath, object serializableObject, bool overWrite = true)
	{
		if(File.Exists(writePath) && overWrite == false)
			return false;

		XmlSerializer serializer = new XmlSerializer(typeof(T));
		using( var writeFile = File.Create(writePath))
		{
			serializer.Serialize(writeFile, serializableObject);
		}
		return true;
	}
	
	/// <summary>
	/// Deserialize an object from an XML file.
	/// </summary>
	/// <returns>
	/// The deserialized list.  If the file doesn't exist, returns the default for 'T'
	/// </returns>
	/// <param name='readPath'>
	/// Where to read the file from.
	/// </param>
	/// <typeparam name='T'>
	/// Type of object being loaded from the file
	/// </typeparam>
	public static T DeserializeFromXMLFile<T>(string readPath)
	{
		if(!File.Exists (readPath))
			return default(T);
		
		XmlSerializer serializer = new XmlSerializer(typeof(T));
		
		using (var readFile = File.OpenRead (readPath))
			return (T)serializer.Deserialize(readFile);
	}
}

