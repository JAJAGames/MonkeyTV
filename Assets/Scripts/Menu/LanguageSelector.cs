using UnityEngine;
using System.Collections;
using InterfaceMovement;
using InControl;

public class LanguageSelector : MonoBehaviour {


	public Loading language;
	public int value;
	private ButtonManager manager;

	void Awake(){
		manager = GameObject.Find ("InControl").GetComponent < ButtonManager> ();
		value = (int) language.output;
	}
	
	// Update is called once per frame
	void Update () {
		if (GetComponent<ButtonMenu> () != manager.focusedButton)
			return;
		// Use last device which provided input.
		var inputDevice = InputManager.ActiveDevice;

		// Move focus with directional inputs.
		if ((inputDevice.Direction.Left|| Input.GetKeyDown (KeyCode.LeftArrow)) && value > 1) {
			AudioManager.Instance.PlayFX (Enums.fxClip.BUTTON_HOVER);
			value -= 1;
			language.output = (TypeOfData) value;
			Debug.Log ("Next");
		}

		if ((inputDevice.Direction.Right || Input.GetKeyDown (KeyCode.RightArrow)) && value < (int) TypeOfData.max_laguages ) {
			AudioManager.Instance.PlayFX (Enums.fxClip.BUTTON_HOVER);
			value += 1;
			language.output = (TypeOfData) value;
			Debug.Log ("Prev");
		}

	}
}
