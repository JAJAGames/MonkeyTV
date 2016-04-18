using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.IO;


public class Loading : MonoBehaviour {

	public TypeOfData output;
	private LanguageXMLSerializer xml;
	public List<Words> listData;

	void Awake () {

		//initialize file
		xml = new LanguageXMLSerializer ();

		if (!File.Exists (Path.Combine (Application.dataPath, "Languages.xml")))
			return;
		xml = LanguageXMLSerializer.Load (Path.Combine (Application.dataPath, "Languages.xml"));
		GetList ();
	}
		
	private void GetList ()
	{
		for (int i = 0; i < xml.languages.Count; ++i) {
			listData.Add (xml.languages [i]);
		}
	}
}