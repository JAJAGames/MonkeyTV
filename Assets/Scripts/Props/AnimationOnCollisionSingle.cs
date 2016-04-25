using UnityEngine;
using System.Collections;

public class AnimationOnCollisionSingle : MonoBehaviour {

	private Animation animation;
	private bool isAnimated;

	void Awake () {
		animation = gameObject.GetComponent<Animation> ();
		isAnimated = false;
	}

	void OnTriggerEnter (Collider other){
		if (!isAnimated && other.CompareTag ("Player")) {
			StartAnimation ();
		}
	}

	private void StartAnimation() {
		isAnimated = true;
		animation.Play ();
		Invoke ("StopAnim",animation.clip.length); 
	}

	private void StopAnim(){
		animation.Stop();
		this.enabled = false;
	}
}
