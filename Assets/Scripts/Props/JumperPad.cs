using UnityEngine;
using System.Collections;

public class JumperPad : MonoBehaviour {

	Animation anim;
	void Awake(){
		anim = GetComponent<Animation> ();
	}
	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {

			other.GetComponent<PlayerMovement> ().AddForce (Vector3.up * 35);
			anim.Play ();
		}

	}
}
