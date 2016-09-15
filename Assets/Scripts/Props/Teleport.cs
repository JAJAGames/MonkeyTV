using UnityEngine;
using System.Collections;
using InControl;

public class Teleport : MonoBehaviour {

	public GameObject destination;

	void Start() {
		
	}

	private void OnTriggerStay (Collider other) {
		var inputDevice = InputManager.ActiveDevice;

		if (other.CompareTag ("Player")) {
			if ((Input.GetButton("Pick") || inputDevice.Action3)) {
				StartCoroutine(TeleportEntity(other));
			}
		}
	}

	IEnumerator TeleportEntity(Collider other) {
		yield return new WaitForSeconds(1.0f);
		other.transform.position = destination.transform.position;
	}
}
