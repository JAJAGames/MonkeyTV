using UnityEngine;
using System.Collections;

/* PLAYERMOVEMENT.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * Control the player movement
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

public class PlayerMovement : MonoBehaviour  {

	public bool IsDead = false;

	public float movementSpeed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;
	private Vector3 moveDirection = Vector3.zero;
	CharacterController controller;

	public bool IsMoving {
		get {
			return !IsDead && (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0);
		}
	}

	private CharacterController mController;

	public void KillPlayer() {
		if (IsDead)
			return;

		StartCoroutine (RestartGame ());

		IsDead = true;
	}
		
	private void Awake ()  {
		controller = GetComponent<CharacterController> ();
	}

	private void Update ()  {
		if (IsDead)
			return;

		if (controller.isGrounded) {
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= movementSpeed;

			if (Input.GetButton("Jump"))
				moveDirection.y = jumpSpeed;
		}

		moveDirection.y -= gravity * Time.deltaTime;
		controller.Move(moveDirection * Time.deltaTime);
	}

	private IEnumerator RestartGame() {
		yield return new WaitForSeconds (3);
		//Application.LoadLevel (0);
	}
}
