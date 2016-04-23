using UnityEngine;
using System.Collections;

public class PropGrenade : MonoBehaviour {

	public IGUGrenade iguGrenade;

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")) {
			//other.gameObject.GetComponent<PlayerGrenadeShoot> ().grenadeActive = true;

		}
	}
}
