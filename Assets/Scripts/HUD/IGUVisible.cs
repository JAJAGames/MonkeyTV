using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class IGUVisible : MonoBehaviour {

	private Image img;
	public bool visible;

	void Awake(){
		img = GetComponent<Image> ();
		SetVisible (visible);
	}

	public void SetVisible (bool visible){
		Color color = Color.white;
		color.a = Convert.ToUInt16 (visible);
		img.color = color;
	}
}
