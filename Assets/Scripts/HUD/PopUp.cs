using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PopUp : MonoBehaviour {

	private DataRead _text;
	private Animator _anim;
	private bool once;
	// Use this for initialization
	void Awake () {
		_text = transform.FindChild ("Text").GetComponent <DataRead>();
		_anim = GetComponent<Animator> ();
		once = false;
	}
	
	// Update is called once per frame
	public void ShowPopUp (int code) {
		_text.ChangeText (code);
		_anim.SetTrigger ("Show");
	}

	public void ShowPopUpOnce (int code) {
		if (once)
			return;
		_text.ChangeText (code);
		_anim.SetTrigger ("Show");
		once = true;
	}
}
