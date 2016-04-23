/* CAMERAMANAGER.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * CLASS FOR GET/SET VALUE USING THE INDEX KEY
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * LoadChanges (Words)
 * SaveChanges (Words)
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 15/04/2016	USEN IN INPUTDATA SCENE
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;

public enum TypeOfData
{
	key,
	english,
	spanish,
	catalan,
	galician,
}

public class DataAplication : MonoBehaviour {

	public TypeOfData type;
	public InputField input;

	public void LoadChanges (Words index){

		switch(type)
		{
		case TypeOfData.key:
			input.text = index.key.ToString();
			break;
		case TypeOfData.english:
			input.text = index.english;
			break;
		case TypeOfData.spanish:
			input.text = index.spanish;
			break;
		case TypeOfData.catalan:
			input.text = index.catalan;
			break;
		case TypeOfData.galician:
			input.text = index.galician;
			break;

		}

	}

	public void SaveChanges (Words index){
		switch (type) {
		case TypeOfData.key:
			index.key = int.Parse(input.text);
			break;		
		case TypeOfData.english:
			index.english = input.text;
			break;
		case TypeOfData.spanish:
			index.spanish = input.text;		
			break;
		case TypeOfData.catalan:
			index.catalan = input.text;
			break;
		case TypeOfData.galician:
			index.galician = input.text;
			break;
		}
	}
}
