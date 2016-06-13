using UnityEngine;
using System.Collections;

public class CagedMonkeyAnimation : MonoBehaviour {

	private Animator animator;

	void Awake () {
		animator = GetComponent<Animator>();
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag("Player"))
			animator.SetBool("Ask for Help",true);
	}

	void OnTriggerExit (Collider other){
		//ADD SOUNDS
		if (other.CompareTag("Player"))
			animator.SetBool("Ask for Help",false);
	}
}
