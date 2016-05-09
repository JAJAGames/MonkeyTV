/* PROPGRENADE.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * INTERACT OF GRENADE WITH PLAYER AND SHOW IT ON IGU
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * OnTriggerEnter()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 23/04/2016	this script is attached to grenade gameObject in dynamic HUD. 
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class PropGrenade : MonoBehaviour {

	void OnTriggerEnter (Collider other){
		
		if (other.CompareTag ("Player")) {
			
			other.gameObject.GetComponent<PlayerGrenadeShoot> ().SetActive(true);	
			gameObject.SetActive (false);

		}

	}
}
