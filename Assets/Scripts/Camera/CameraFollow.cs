using UnityEngine;
using System.Collections;

/* CAMERAFOLLOW.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
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
 * 30/03/2016   ----- OLD METHOD -------------------> Static camera while player jump and fall at same place.
 * 30/03/2016	----- NEVER FOLLOW PLAYER JUMPING --> camera never follow player while jump. If player goes out of screen the camera 
 * 				reset their position to player position plus initial offset.
 * 30/03/2016	added cooldown timer to move camera between jumps.
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class CameraFollow : MonoBehaviour {

	public PlayerMovement playerMove; 	//used in both methods

	private Vector3 initDifference;
	private float smooth;
	//NEVER FOLLOW PLAYER JUMPING

	private void Start() 
	{
		initDifference = transform.position - playerMove.transform.position; 				//get the initial offset
		smooth = playerMove.gameObject.GetComponent<PlayerMovement>().movementSpeed * 3;	//get the speed of camera. It should be faster than player
    }

	private void Update() 
	{
		 
		Vector3 difference = transform.position - playerMove.transform.position;
        
		//player visible and not jumping -> move camera.
		if (difference != initDifference) 
		{
			if (playerMove.controller.isGrounded)
				transform.position = Vector3.MoveTowards (transform.position, playerMove.transform.position + initDifference, smooth * Time.deltaTime);
			else 
			{
				difference = playerMove.transform.position + initDifference;
				difference.y = transform.position.y;
				transform.position = Vector3.MoveTowards (transform.position, difference, smooth * Time.deltaTime);
			}
		}
	}
	
}