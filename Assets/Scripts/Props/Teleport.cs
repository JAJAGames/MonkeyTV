using UnityEngine;
using System.Collections;
using InControl;

public class Teleport : MonoBehaviour {

	public GameObject destination;
	private Transform canvas;
	private GameObject R2D2;
	public NavMeshObstacle playerObstacle;

	void Start() {
		canvas = transform.FindChild ("Canvas");
		R2D2 = GameObject.Find ("R2D2");
		playerObstacle = GameObject.Find ("Player").GetComponent<NavMeshObstacle> ();
	}

	private void OnTriggerStay (Collider other) {
		var inputDevice = InputManager.ActiveDevice;

		if (other.CompareTag ("Player")) {
			if ((Input.GetButton("Pick") || inputDevice.Action3)) {
				StartCoroutine(TeleportEntity(other));
			}
		}
	}

	private void OnTriggerEnter (Collider other) {
		canvas.gameObject.SetActive (true);
	}

	private void OnTriggerExit (Collider other) {
		canvas.gameObject.SetActive (false);
	}

	IEnumerator TeleportEntity(Collider other) {
		yield return new WaitForSeconds(1.0f);
		other.transform.position = destination.transform.position;
		R2D2.SetActive (false);
		playerObstacle.enabled = false;
	}
}
