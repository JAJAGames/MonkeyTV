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
			Vector3 jump = other.transform.forward;
			jump.y += 4;
			other.GetComponent<PlayerMovement> ().controller.Move (jump);
			anim.Play ();
		}

	}
}
