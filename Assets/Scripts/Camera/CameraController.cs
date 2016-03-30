using UnityEngine;
using System.Collections;

/* CAMERACONTROLLER.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * Move the camera relative to the player.
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * XX/XX/XXXX	XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 * 30/03/2016   Static camera while player jump and fall at same place.
 * 30/03/2016	Don't follow player while is not grounded. And old method comented
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class CameraController : MonoBehaviour {

	public PlayerMovement Player;
	//private Vector3 mPreviousPosition;
	private Vector3 initDifference;
	private float smooth = 10;

	//Don't follow while jump
	private void Start() 
	{
		initDifference = transform.position - Player.transform.position;
	}

	private void LateUpdate() {

		if (!Player.controller.isGrounded)
			return;
	
		Vector3 difference = transform.position - Player.transform.position;

		if (difference != initDifference)
			transform.position = Vector3.MoveTowards (transform.position, Player.transform.position + initDifference, smooth * Time.deltaTime);

	}


/*----------------------------------------------------------------- OLD METHOD ------		
 * follow while jump but stay on place if jumps and falls at same place


	private void Awake() {
		mPreviousPosition = Player.transform.position;
	}

	private void LateUpdate() {

		Vector3 difference = Player.transform.position - mPreviousPosition;

		//the camera remain in place when player jumps without forward direction
		if (!Player.controller.isGrounded && difference.x == 0 && difference.z == 0) 	
			return;

		transform.position += difference;
		mPreviousPosition = Player.transform.position;
	}
*/

}