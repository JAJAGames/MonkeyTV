using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;


public class Loading : MonoBehaviour {

	public DataRead[] data;
	public TypeOfData output;

	public int currentIndex = 0;
	int amount = 0;

	void Awake () {
		output = TypeOfData.english;
	}

	public void LoadWords (int index)
	{

		LanguageXMLSerializer xml = new LanguageXMLSerializer ();

		if (!File.Exists (Path.Combine (Application.dataPath, "Languages.xml")))
			return;
		xml = LanguageXMLSerializer.Load (Path.Combine (Application.dataPath, "Languages.xml"));
		amount = xml.languages.Count;

		if (index > amount - 1)
			return;

		for (int i = 0; i < data.Length; i++) {
			data [i].LoadChanges (xml.languages [index]);
		}
	}


}