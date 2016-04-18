using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

[XmlRoot("Language_List")]
public class LanguageXMLSerializer {

	[XmlArray("Translation"),XmlArrayItem("Tense")] public List<Words> languages = new List <Words> ();

	public void Save (string path)
	{
		var serializer = new XmlSerializer (typeof(LanguageXMLSerializer));
		using (var stream = new FileStream (path, FileMode.Create))
		using (XmlWriter writer = new XmlTextWriter(stream, Encoding.UTF8))
			serializer.Serialize(writer, this);
	}

	public static LanguageXMLSerializer Load (string path)
	{
		var serializer = new XmlSerializer (typeof(LanguageXMLSerializer));
		using (var stream = new FileStream (path, FileMode.Open))
			return serializer.Deserialize(stream) as LanguageXMLSerializer;
	}
}

/*
	public void Save (string path)
	{
		var serializer = new XmlSerializer (typeof(LanguageXMLSerializer));
		var stream = new FileStream (path, FileMode.Create);
		// Instantiate xmlWriter and pass stream and encoding.
		XmlWriter writer = new XmlTextWriter(stream, Encoding.Unicode);
		serializer.Serialize (writer, this);
	}
	

public static LanguageXMLSerializer Load (string path)
{
	// Instantiate xmlSerializer with my object type
	var serializer = new XmlSerializer (typeof(LanguageXMLSerializer));

	// Instantiate a new stream and pass file location and mode.
	Stream stream = new FileStream(path, FileMode.Open);


	// Instantiate xmlWriter and pass stream and encoding.
	XmlWriter xmlWriter = new XmlTextWriter(stream, Encoding.Unicode);

	// Call serialize method and pass xmlWriter and my object.
	return serializer.Deserialize(stream) as LanguageXMLSerializer;
}*/