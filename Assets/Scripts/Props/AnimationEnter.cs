using UnityEngine;
using System.Collections;

public class AnimationEnter : MonoBehaviour {

	private Animation animation;
	void Awake () {
		animation = gameObject.GetComponent<Animation> ();
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")) {
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
		animation.enabled = false;
		this.enabled = false;
	}
}
