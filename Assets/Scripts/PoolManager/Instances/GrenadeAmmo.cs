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

	[Range (2,7)]
	public float impulse = 2.0f;
	// Add the tag PlayerShoot to use it in props and other scripts.
	private bool move;
	private Rigidbody body;
	private Transform player;

	void Awake (){
		tag = "Grenade";
		body = gameObject.GetComponent<Rigidbody> ();
		player = GameObject.Find ("Player").transform.GetChild (0).transform;
		move = false;
	}

	//When enabled we add new force to throw grenades 
	public void OnEnable(){
		body.AddForce ((player.forward.normalized + Vector3.up) * impulse, ForceMode.Impulse);
	}

	// Update Gravity
	void Update(){
		body.AddForce ( 20f * Vector3.down);
	}

	//Cos the enemies have one big sphere collider we ought to verify the distance from enemy position and not the start collision.
	void OnTriggerStay (Collider other) {

		if (other.CompareTag ("Enemy")) {
				//other.gameObject.GetComponent<EnemyStats> ().TakeDamage (1);
				//other.transform.position = (other.transform.position - other.transform.forward * 2);
				gameObject.SetActive (false);
		}
	}
}
