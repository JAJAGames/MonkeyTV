using UnityEngine;
using System.Collections;

public class OnEnterEnable : MonoBehaviour {

	public GameObject otherObject;
	private bool tutorial = true;

	void Awake (){
		otherObject.SetActive(false);
	}


	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player") && tutorial)
			otherObject.SetActive(true);
	}

	void OnTriggerExit(Collider other) {
		if (other.CompareTag("Player") && otherObject.activeSelf)
			otherObject.SetActive(false);
	}

	public void DisableOther(){
		otherObject.SetActive(false);
	}

	public void SetTutorialOff (bool onOff)	{
		tutorial = onOff;
	}

	public bool GetTutorial ()	{
		return tutorial;
	}
}
