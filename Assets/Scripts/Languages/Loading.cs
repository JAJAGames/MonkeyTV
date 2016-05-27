/* CAMERAMANAGER.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * READ THE XML FILE "Languages.xml" AND STORE IT IN listData
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * GetList ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 15/04/2016	USEN IN MENU SCENE
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

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

		if (!File.Exists (Path.Combine (Application.dataPath + "/Resources/", "Languages.xml")))
			return;
		xml = LanguageXMLSerializer.Load (Path.Combine (Application.dataPath + "/Resources/", "Languages.xml"));
		GetList ();
	}
		
	private void GetList ()
	{
		for (int i = 0; i < xml.languages.Count; ++i) {
			listData.Add (xml.languages [i]);
		}
	}
}