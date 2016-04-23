/* CAMERAMANAGER.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * THIS IS THE CORE LOGIC FOR THE INPUTDATA SCENE TOOL WHERE WE CAN READ AND STORE NEW INFO IN OUR XML FILE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * Update ()
 * SaveWords ()
 * LoadWords (int)
 * LoadWordsRight ()
 * LoadWordsLeft ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 15/04/2016	USEN IN INPUTDATA SCENE TOOL
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;


public class Savig : MonoBehaviour {

	public DataAplication[]data;
	public Text visualDisplayIndex;
	int currentIndex = 0;
	int amount = 0;


	void Awake () {
		LoadWords (0);
	}
	
	// Update is called once per frame
	void Update () {
		visualDisplayIndex.text = "Index: " + (currentIndex +1) + "/" + amount;
	}

	public void SaveWords(){

		LanguageXMLSerializer xml = new LanguageXMLSerializer ();
		if (File.Exists (Path.Combine (Application.dataPath, "Languages.xml")))
			xml.languages = LanguageXMLSerializer.Load (Path.Combine (Application.dataPath, "Languages.xml")).languages;
		
		if (currentIndex > xml.languages.Count - 1)
				xml.languages.Add (new Words ());
		else
			for (int i = 0; i < data.Length; i++) {
				data [i].SaveChanges (xml.languages [currentIndex]);
			}

		xml.Save (Path.Combine (Application.dataPath, "Languages.xml"));
		LoadWords (currentIndex);
	}


	public void LoadWords (int index)
	{
		
		LanguageXMLSerializer xml = new LanguageXMLSerializer ();

		if (!File.Exists (Path.Combine (Application.dataPath, "Languages.xml")))
			SaveWords ();
		xml = LanguageXMLSerializer.Load (Path.Combine (Application.dataPath, "Languages.xml"));
		amount = xml.languages.Count;

		if (index > amount - 1)
			return;
		
		for (int i = 0; i < data.Length; i++) {
			data [i].LoadChanges (xml.languages [index]);
		}
	}

	public void LoadWordsRight ()
	{
		if (currentIndex >= amount - 1)
			return;

		LoadWords (++currentIndex);
		SaveWords ();
	}

	public void LoadWordsLeft ()
	{
		if (currentIndex == 0)
			return;

		LoadWords (--currentIndex);
		SaveWords ();
	}

	public void NewWord ()
	{
		LoadWords (++currentIndex);
		SaveWords ();
	}

	public void RemoveWord ()
	{
		if (File.Exists (Path.Combine (Application.dataPath, "Languages.xml"))) {
			LanguageXMLSerializer xml = LanguageXMLSerializer.Load (Path.Combine (Application.dataPath, "Languages.xml"));
			xml.languages.RemoveAt (currentIndex);

			xml.Save (Path.Combine (Application.dataPath, "Languages.xml"));
			if (currentIndex > xml.languages.Count - 1) {
				currentIndex--;
			}

			if (currentIndex < 0) {
				currentIndex = 0;
				xml.languages.Add (new Words());
			}
			if (amount > 1)
				amount--;

			xml.Save (Path.Combine (Application.dataPath, "Languages.xml"));
			LoadWords (currentIndex);
		}
	}

	void OnApplicationQuit(){
		SaveWords ();
	}
}