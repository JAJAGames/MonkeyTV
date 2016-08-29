using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using InterfaceMovement;

public class SliderControl : MonoBehaviour {

	public Slider slider;
	private ButtonManager manager;
	private float timer;

	void Awake(){
		manager = GameObject.Find ("InControl").GetComponent < ButtonManager> ();
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
		if ((inputDevice.Direction.Left|| Input.GetKeyDown (KeyCode.LeftArrow)) && timer == 0) {
			AudioManager.Instance.PlayFX (Enums.fxClip.FX_BUTTON_HOVER);
			slider.value -= 0.1f;
			timer += 0.1f;
		}

		if ((inputDevice.Direction.Right || Input.GetKeyDown (KeyCode.RightArrow)) && timer == 0) {
			AudioManager.Instance.PlayFX (Enums.fxClip.FX_BUTTON_HOVER);
			slider.value += 0.1f;
			timer += 0.1f;
		}

	}
}
