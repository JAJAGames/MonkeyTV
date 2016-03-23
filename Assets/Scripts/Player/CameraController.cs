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
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class CameraController : MonoBehaviour {

	public PlayerMovement Player;
	private Vector3 mPreviousPosition;

	private void Awake() {
		mPreviousPosition = Player.transform.position;
	}

	private void LateUpdate() {
		if (!Player.IsMoving)
			return;

		Vector3 difference = Player.transform.position - mPreviousPosition;

		transform.position += difference;

		mPreviousPosition = Player.transform.position;
	}
}