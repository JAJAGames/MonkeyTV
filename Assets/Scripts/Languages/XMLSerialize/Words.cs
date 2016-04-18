using UnityEngine;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System;

[Serializable]
public class Words {
	
	[XmlAttribute ("key")]
	public int key;
	[XmlAttribute ("english")]
	public string english;
	[XmlAttribute ("spanish")]
	public string spanish;
	[XmlAttribute ("catalan")]
	public string catalan;
	[XmlAttribute ("galician")]
	public string galician;

	public Words(){
		key = 0;
		english = "";
		spanish = "";
		catalan = "";
		galician = "";
	}
}
