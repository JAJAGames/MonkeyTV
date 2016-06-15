using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InterfaceMovement;

public class LogoImage : MonoBehaviour {

	private  ButtonManager buttonManager;
	private  ButtonMenu buttonParent;

	void Awake(){
		
		buttonManager = GameObject.Find ("InControl").GetComponent<ButtonManager> ();
		buttonParent = buttonManager.focusedButton;

	}



	void Update(){
		buttonParent = buttonManager.focusedButton;
		Vector3 pos = buttonParent.transform.position;
		float width = buttonParent.GetComponent<RectTransform> ().sizeDelta.x;
		pos.x -= width/2;
		pos.y += 3;
		transform.position = pos;
	}
}
