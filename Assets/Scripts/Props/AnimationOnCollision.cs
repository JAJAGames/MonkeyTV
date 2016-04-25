using UnityEngine;
using System.Collections;

public class AnimationOnCollision : MonoBehaviour {

	private Animation anim;

	void Awake () {
		anim = gameObject.GetComponent<Animation> ();
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player") || other.CompareTag ("Enemy")) {
			StartAnimation ();
		}
	}

	void OnTriggerExit (Collider other){
		if (other.CompareTag ("Player") || other.CompareTag ("Enemy")) {
			StartAnimation ();
		}
	}

	private void StartAnimation() {
		if (anim.isPlaying) {
			return;
		}
		anim.Play ();
		Invoke ("StopAnim",anim.clip.length); 
	}

	private void StopAnim(){
		anim.Stop();
	}
}
