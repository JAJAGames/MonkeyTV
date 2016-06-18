using UnityEngine;
using System.Collections;

public class OnEnterEnable : MonoBehaviour {

	public GameObject otherObject;

	void Awake (){
		otherObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player"))
			otherObject.SetActive(true);
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player"))
			otherObject.SetActive(false);
	}
}
