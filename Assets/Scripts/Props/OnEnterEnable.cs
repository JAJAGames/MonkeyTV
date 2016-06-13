using UnityEngine;
using System.Collections;

public class OnEnterEnable : MonoBehaviour {

	public GameObject otherObject;
	public float waitTime;

	void Awake (){
		otherObject.SetActive(false);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player"))
			StartCoroutine(EnableAndWaitForDisbale());
	}

	IEnumerator EnableAndWaitForDisbale() {
		otherObject.SetActive(true);
		yield return new WaitForSeconds(waitTime);
		otherObject.SetActive(false);
	}
}
