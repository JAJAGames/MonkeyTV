using UnityEngine;
using System.Collections;
using InterfaceMovement;
using InControl;

public class LanguageSelector : MonoBehaviour {


	public Loading language;
	public int value;
	private ButtonManager manager;
	private float timer;

	void Awake(){
		manager = GameObject.Find ("InControl").GetComponent < ButtonManager> ();
		value = (int) language.output;
		timer = 0;
	}
	
	// Update is called once per frame
	void Update () {

		if (timer > 0)
			timer -= Time.deltaTime;
		else
			timer = 0;
		
		if (GetComponent<ButtonMenu> () != manager.focusedButton)
			return;
		// Use last device which provided input.
		var inputDevice = InputManager.ActiveDevice;

		// Move focus with directional inputs.
		if (((inputDevice.Direction.Left|| Input.GetKeyDown (KeyCode.LeftArrow)) && value > 1) && timer == 0) {
			AudioManager.Instance.PlayFX (Enums.fxClip.FX_BUTTON_HOVER);
			value -= 1;
			language.output = (TypeOfData) value;
			Debug.Log ("Next");
			timer += 0.2f;
		}

		if (((inputDevice.Direction.Right || Input.GetKeyDown (KeyCode.RightArrow)) && value < (int) TypeOfData.max_laguages - 1 ) && timer == 0) {
			AudioManager.Instance.PlayFX (Enums.fxClip.FX_BUTTON_HOVER);
			value += 1;
			language.output = (TypeOfData) value;
			Debug.Log ("Prev");
			timer += 0.2f;
		}

		if ((inputDevice.Action1 && inputDevice.Action1.HasChanged)) {
			PlayerPrefs.SetInt("Player Language", value);
		}
	}
}
