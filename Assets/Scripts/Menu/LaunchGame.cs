using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;
using InterfaceMovement;
public class LaunchGame : MonoBehaviour {
	
	// Update is called once per frame
	public ButtonManager interfaceController;
	void OnEnable(){
		GetComponent<Image> ().color = Color.white;
		StartCoroutine (ToGreen());
		interfaceController.enabled = false;
	}

	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action1 && GetComponent<Image> ().color == Color.green)
			gamestate.Instance.SetLevel (Enums.sceneLevel.LEVEL_1);
	}

	IEnumerator ToGreen(){
		yield return new WaitForSeconds (2f);
		GetComponent<Image> ().color = Color.green;
	}
}
