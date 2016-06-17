using UnityEngine;
using System.Collections;
using InControl;
public class ContinueButton : MonoBehaviour {

	void Update(){
		var inputDevice = InputManager.ActiveDevice;
		if (Input.GetButton ("Pick") || inputDevice.Action3)
			gameObject.SetActive (false);
	}
}
