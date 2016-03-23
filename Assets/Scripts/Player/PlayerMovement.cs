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

	public float MovementSpeed = 1;
	public float RotationSpeed = 1;


	/* NEW */
	public float jumpSpeed = 8.0f;
	public float gravity = 20.0f;

	Vector3 direction;

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
		mController = GetComponent<CharacterController> ();
	}

	private void Update ()  {
		if (IsDead)
			return;
		
		if (mController.isGrounded) {
			direction = new Vector3 (Input.GetAxisRaw ("Horizontal"), 0, Input.GetAxisRaw ("Vertical"));

			if (Input.GetButton("Jump"))
				direction.y = jumpSpeed;
		}

		if (direction == Vector3.zero)
			return;
		transform.rotation = Quaternion.Lerp (transform.rotation, Quaternion.LookRotation (direction), Time.deltaTime * RotationSpeed);

		//direction.y -= gravity * Time.deltaTime;

		mController.SimpleMove (direction * MovementSpeed);
	}

	private IEnumerator RestartGame() {
		yield return new WaitForSeconds (3);
		Application.LoadLevel (0);
	}
}
