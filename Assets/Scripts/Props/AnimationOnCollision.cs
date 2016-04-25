using UnityEngine;
using System.Collections;

public class AnimationOnCollision : MonoBehaviour {

	private Animation animation;

	void Awake () {
		animation = gameObject.GetComponent<Animation> ();
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
		if (animation.isPlaying) {
			return;
		}
		animation.Play ();
		Invoke ("StopAnim",animation.clip.length); 
	}

	private void StopAnim(){
		animation.Stop();
	}
}
