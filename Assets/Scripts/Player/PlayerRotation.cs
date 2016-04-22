/* PLAYERROTATION.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * MOVEMENT OF THE FREE CAMERA USING MOUSE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Update ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * XX/XX/2016	CODE ATTACHED TO RENDER OBJECT OF PREFAB PLAYER
 * 12/04/2016	DELETED PLANE MASK.
 * 12/04/2016	SET A SELECTIVE VALUE TO NOT HIT PLAYER OR ENEMIES
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	private PlayerStats pStats;

	void Awake (){
		pStats = transform.GetComponentInParent<PlayerStats> ();
	}
	void FixedUpdate () {
		if (pStats.isDead)
			return;

		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition); 	// get the mouse point 
		int layerMask = 1; 													// default layer
		RaycastHit floorHit;
		if (Physics.Raycast (camRay, out floorHit, 100, layerMask)) {
			Vector3 lookTo = floorHit.point;
			lookTo.y = transform.position.y;								//cos always look towards the y axis must be fixed
			transform.LookAt (lookTo);
		}
	}
}