using UnityEngine;
using System.Collections;

public class JumperPad : MonoBehaviour {

	public float jumpForce;

	Animation anim;
	void Awake(){
		anim = GetComponent<Animation> ();
	}
	// Update is called once per frame
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			AudioManager.Instance.PlayFX (Enums.fxClip.FX_JUMPER_PAD);
			other.GetComponent<PlayerMovement> ().AddForce (new Vector3(0,jumpForce,0));
			anim.Play ();
		}

	}
}
