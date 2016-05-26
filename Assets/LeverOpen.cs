using UnityEngine;
using System.Collections;


public class LeverOpen : MonoBehaviour {
	
	public Transform lever; 

	void OnTriggerStay (Collider other){
		if (Input.GetKeyDown (KeyCode.E) && other.CompareTag ("Player")) {
			lever.Rotate (100, 0, 0);
			gameObject.GetComponent<OpenDoor> ().enabled = true;
			//gameObject.GetComponent<OpenDoor> ().SetCloset (false);
			Invoke ("DisableSelf", 1f);
		}
	}

	void DisableSelf()
	{
		gameObject.SetActive (false);
	}
}