using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;


public class Loading : MonoBehaviour {

	public TypeOfData output;
	public DataRead[] data;

	int amount = 0;

	void Awake () {
		
		output = TypeOfData.english;
		if (Application.systemLanguage.ToString() == "Spanish")
			output = TypeOfData.spanish;
		if (Application.systemLanguage.ToString() == "Catalan")
			output = TypeOfData.catalan;
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

	public void SetOutput (int value)
	{
		output = (TypeOfData) value;
	}

}