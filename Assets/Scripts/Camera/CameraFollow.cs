using UnityEngine;
using System.Collections;

/* CAMERAFOLLOW.CS
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
	private bool playerVisible;
    private float timer;                //minimum waiting time between combo jumps.
	//NEVER FOLLOW PLAYER JUMPING

	private void Start() 
	{
		initDifference = transform.position - playerMove.transform.position; 				//get the initial offset
		smooth = playerMove.gameObject.GetComponent<PlayerMovement>().movementSpeed * 3;	//get the speed of camera. It should be faster than player
		playerVisible = true;                                                               //in initial offset athe player is visible
    }

	private void Update() {
		 
		//get the position of player in screen
		Vector3 screenPoint = Camera.main.WorldToViewportPoint (playerMove.transform.position); 

		//is visible?
		if (screenPoint.z > 0.1 && screenPoint.x > 0.1 && screenPoint.x < 0.9 && screenPoint.y > 0.1 && screenPoint.y < 0.9)
			playerVisible = true;
		else
			playerVisible = false;

        //visible and jumping the camera remains in same place --> stop update
        if (!playerMove.controller.isGrounded)
        {
            timer = 0.1f;                                                                   //on air reset timer
            if (playerVisible)
                return;
        }
		// current offset
		Vector3 difference = transform.position - playerMove.transform.position;

        timer -= Time.deltaTime;
        //player visible and not jumping -> move camera.
        if (difference != initDifference && playerVisible && timer < 0)
            transform.position = Vector3.MoveTowards(transform.position, playerMove.transform.position + initDifference, smooth * Time.deltaTime);
        //is out of screen set position with initial offset to camera
        if (!playerVisible)
			transform.position =  playerMove.transform.position + initDifference;
	}
}