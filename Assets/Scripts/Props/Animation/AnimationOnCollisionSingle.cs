using UnityEngine;
using System.Collections;

public class AnimationOnCollisionSingle : MonoBehaviour {

	private Animation anim;
	private bool isAnimated;

	void Awake () {
		anim = gameObject.GetComponent<Animation> ();
		isAnimated = false;
	}

	void OnTriggerEnter (Collider other){
		if (!isAnimated && other.CompareTag ("Player")) {
			StartAnimation ();
		}
	}

	private void StartAnimation() {
		isAnimated = true;
		anim.Play ();
		Invoke ("StopAnim",anim.clip.length); 
	}

	private void StopAnim(){
		anim.Stop();
		this.enabled = false;
	}
}
