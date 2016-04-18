﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;

public class DataRead : MonoBehaviour {

	private Loading loading;
	public TypeOfData type;
	//public Text text;

	public int idKey;

	void Start (){
		loading = Camera.main.GetComponent<Loading> ();
	}

	void Update (){
		
		if (loading.output != type) {
			type = loading.output;
			LoadChanges (loading.listData[idKey]);
		}	
	}


	public void LoadChanges (Words index){

		if (index.key != idKey)
			return;
		switch(type)
		{
		case TypeOfData.key:
			gameObject.GetComponent<Text>().text = index.key.ToString();
			break;
		case TypeOfData.english:
			gameObject.GetComponent<Text>().text = index.english;
			break;
		case TypeOfData.spanish:
			gameObject.GetComponent<Text>().text = index.spanish;
			break;
		case TypeOfData.catalan:
			gameObject.GetComponent<Text>().text = index.catalan;
			break;
		case TypeOfData.galician:
			gameObject.GetComponent<Text>().text = index.galician;
			break;

		}

	}
}
