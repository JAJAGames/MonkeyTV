using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUp : MonoBehaviour {

	private DataRead _text;
	private Animator _anim;
	// Use this for initialization
	void Awake () {
		_text = transform.FindChild ("Text").GetComponent <DataRead>();
		_anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	public void ShowPopUp (int code) {
		_text.ChangeText (code);
		_anim.SetTrigger ("Show");
	}
}
