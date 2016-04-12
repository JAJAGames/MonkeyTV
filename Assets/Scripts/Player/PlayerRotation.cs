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
 * 12/04/2016	DELETED PLANE MASK
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	void Update () {
		// get the mouse point 
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		Physics.Raycast (camRay, out floorHit, 100);

		// look to point
		Vector3 lookTo = floorHit.point;
		lookTo.y = transform.position.y;
		transform.LookAt(lookTo);
	}
}