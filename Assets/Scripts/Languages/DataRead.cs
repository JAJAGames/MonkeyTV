using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.IO;

public class DataRead : MonoBehaviour {

	private Loading loading;
	public TypeOfData type;
	public Text text;

	public int idKey;

	void Awake (){
		loading = Camera.main.GetComponent<Loading> ();
		loading.LoadWords (idKey);
	}

	void Update (){
		
		if (loading.output != this.type) {
			this.type = loading.output;
			loading.LoadWords (idKey);
		}	
	}


	public void LoadChanges (Words index){

		if (index.key != idKey)
			return;
		switch(type)
		{
		case TypeOfData.key:
			text.text = index.key.ToString();
			break;
		case TypeOfData.english:
			text.text = index.english;
			break;
		case TypeOfData.spanish:
			text.text = index.spanish;
			break;
		case TypeOfData.catalan:
			text.text = index.catalan;
			break;
		case TypeOfData.galician:
			text.text = index.galician;
			break;

		}

	}
}
