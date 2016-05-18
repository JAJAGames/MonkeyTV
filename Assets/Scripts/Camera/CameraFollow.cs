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

using UnityEngine;
using System.Collections;
using Enums;

public class CameraFollow : MonoBehaviour {

	public PlayerMovement playerMove; 	//used in both methods

	public Transform target;

	private Vector3 initDifference;
	private Quaternion initRotation;
	private float smooth, smoothSpeed;


	private void Awake(){
		smoothSpeed = playerMove.gameObject.GetComponent<PlayerMovement>().movementSpeed;		//get the speed of camera. It should be faster than player
		smooth = smoothSpeed;
		gamestate.Instance.SetState (state.STATE_INIT);
		initRotation = transform.rotation;
	}

	//we need to be sure that player is Awake so we bust load player in Start().
	private void Start() 
	{
		initDifference = transform.position - playerMove.transform.position; 					//get the initial offset
		//gamestate.Instance.SetState (state.STATE_CAMERA_FOLLOW_PLAYER);
    }

	private void Update() 
	{
		if (gamestate.Instance.GetState () == state.STATE_CAMERA_FOLLOW_PLAYER) {
			Vector3 difference = transform.position - playerMove.transform.position;

			if (smooth == smoothSpeed) {
				//get the position of player in screen
				Vector3 screenPoint = Camera.main.WorldToViewportPoint (playerMove.transform.position); 
				//is visible?
				if (screenPoint.z > 0.1 && screenPoint.x > 0.1 && screenPoint.x < 0.9 && screenPoint.y > 0.1 && screenPoint.y < 0.9)
					smooth = smoothSpeed;
				else
					smooth = smoothSpeed * 2f;
			} else if (difference == initDifference)
				smooth = smoothSpeed;
			
		        
			//player visible and not jumping -> move camera.
			if (difference != initDifference) {
				if (playerMove.grounded)
					transform.position = Vector3.MoveTowards (transform.position, playerMove.transform.position + initDifference, smooth * Time.deltaTime);
				else {
					difference = playerMove.transform.position + initDifference;
					difference.y = transform.position.y;
					transform.position = Vector3.MoveTowards (transform.position, difference, smooth * Time.deltaTime);
				}
			}
			transform.rotation = Quaternion.Slerp(transform.rotation, initRotation, Time.deltaTime);
		}
		if (gamestate.Instance.GetState () == state.STATE_STATIC_CAMERA) {
			transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime );
			transform.rotation = Quaternion.Slerp(transform.rotation, target.rotation, Time.deltaTime);
		}
	}
	
}