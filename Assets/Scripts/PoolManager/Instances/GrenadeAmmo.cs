/* GENADEAMMO.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * This script allows the behaviour of player grenades
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * Update ()
 * OntriggerStay ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 28/03/2016	GENERIC METHODS TO USE IN SCRIPTS
 * 20/04/2016	Added playershot tag
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class GrenadeAmmo : MonoBehaviour {

	[Range (1,3)]
	public float speed = 2.0f;

	// Add the tag PlayerShoot to use it in props and other scripts.
	void Awake (){
		gameObject.tag = "PlayerShoot";	
	}

	// Update is called once per frame
	void Update () {/*
		Vector3 direction = Vector3.forward;
		direction.y += -9.81f * Time.deltaTime;
		gameObject.transform.Translate(direction * speed/5);*/
	}

	//Cos the enemies have one big sphere collider we ought to verify the distance from enemy position and not the start collision.
	void OnTriggerStay (Collider other) {

		if (other.CompareTag ("Enemy")) {
			if (Vector3.Distance (gameObject.transform.position, other.gameObject.transform.position) < 1) {
				//other.gameObject.GetComponent<EnemyStats> ().TakeDamage (1);
				other.transform.position = (other.transform.position - other.transform.forward * 2);
				gameObject.SetActive (false);
			}
		}
	}
}
