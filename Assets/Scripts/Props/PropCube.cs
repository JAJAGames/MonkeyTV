using UnityEngine;
using System.Collections;

public class PropCube : MonoBehaviour {

	private Animation anim;
	private bool isAnimated, falling;

	private SphereCollider sphereCollider;

	void Awake () {
		anim = gameObject.GetComponent<Animation> ();
		sphereCollider = GetComponent<SphereCollider> ();
		isAnimated = false;
		falling = false;
	}
		
	private void StartAnimation() {
		
		if (tag == "Water Cube")
			AudioManager.Instance.PlayFX (Enums.fxClip.WATER_CUBE);

		isAnimated = true;
		anim.Play ();
		Invoke ("StopAnim",anim.clip.length); 

	}

	private void StopAnim(){
		anim.Stop();
		this.enabled = false;
	}

	void OnTriggerEnter (Collider other){

		if (other.CompareTag ("Player")) {
			
			if (isAnimated) {
				
				PlayerMovement player = other.GetComponent<PlayerMovement> ();

				Vector3 newVector = player.moveDirection * 2;
				newVector.y += 5; 
				player.AddForce (newVector);
				falling = true;
			} else {
				sphereCollider.center = new Vector3 (0.27f, 0.1f, 0f);
				sphereCollider.radius = 0.8f;
				StartAnimation ();
			}
		}
	}

	void OnTriggerExit (Collider other){

		if (other.CompareTag ("Player") && falling) {
			falling = false;
			PlayerMovement player = other.GetComponent<PlayerMovement> ();
			player.grounded = true;
			player.AddForce (Vector3.zero);
		}
	}

}
