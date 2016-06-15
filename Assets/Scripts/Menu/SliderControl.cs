using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using InterfaceMovement;

public class SliderControl : MonoBehaviour {

	public Slider slider;
	private ButtonManager manager;
	void Awake(){
		manager = GameObject.Find ("InControl").GetComponent < ButtonManager> ();
	}
	// Update is called once per frame
	void Update () {

		if (GetComponent<ButtonMenu> () != manager.focusedButton)
			return;
		// Use last device which provided input.
		var inputDevice = InputManager.ActiveDevice;

		// Move focus with directional inputs.
		if (inputDevice.Direction.Left.WasPressed || Input.GetKeyDown (KeyCode.LeftArrow)) {
			AudioManager.Instance.PlayFX (Enums.fxClip.BUTTON_HOVER);
			slider.value -= 0.1f;
		}

		if (inputDevice.Direction.Right.WasPressed || Input.GetKeyDown (KeyCode.RightArrow)) {
			AudioManager.Instance.PlayFX (Enums.fxClip.BUTTON_HOVER);
			slider.value += 0.1f;
		}
	}
}
