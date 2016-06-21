/* PLAYERMOVEMENT.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
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
 * 06/04/2016	Added ParticleSystem to player. The new code allow to enable and disable emitter 
 * ------------------------------------------------------------------------------------------------------------------------------------
 */
using System;
using UnityEngine;
using System.Collections;
using InControl;

public class PlayerMovement : MonoBehaviour  {

	public float movementSpeed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;


	public Vector3 moveDirection = Vector3.zero;
	private Vector3 externalForce = Vector3.zero;
	public CharacterController controller;
	public GameObject walkParticles;
	public GameObject jumpParticles;
	private Animator anim;
	private PlayerStats pStats;
	public bool grounded;
	private void Awake ()  {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator>();
		walkParticles.SetActive (false);
		jumpParticles.SetActive (false);
		grounded = false;
	}

	private void FixedUpdate ()  {
		
					
		if (gamestate.Instance.GetState () == Enums.state.STATE_LOSE || gamestate.Instance.GetState() == Enums.state.STATE_PLAYER_PAUSED)		//skip update for game Losed 
			return;
																			//no update for deads... no Zombies please!
		if (controller.isGrounded) {
			anim.SetBool ("Jump", false);

			if (!grounded) {
				grounded = true;
				jumpParticles.SetActive (true);
				StartCoroutine (ParticlesJump ());
			}

			var inputDevice = InputManager.ActiveDevice;
			if (InputManager.ActiveDevice == InputDevice.Null)
				moveDirection = new Vector3 (Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			else
				moveDirection = new Vector3 (inputDevice.LeftStickX, 0, inputDevice.LeftStickY);
			//Make the player face his movement direction. We ought to disable rotation script
			if (moveDirection != Vector3.zero)							//Look to rotation viewing is not Zero
				transform.rotation = Quaternion.LookRotation (moveDirection);

			moveDirection *= movementSpeed;
			//moveDirection = transform.TransformDirection(moveDirection);  //This line set transform from local space movement to world movement 

			if (moveDirection != Vector3.zero)
				anim.SetBool ("Walk", true);
			else
				anim.SetBool ("Walk", false);

			if (moveDirection == Vector3.zero) 								//enable or disable emitter of ParticleSyste
				walkParticles.SetActive (false);
			else
				walkParticles.SetActive (true);

			if ((inputDevice.Action1 || Input.GetButton("Jump")) && externalForce == Vector3.zero) { 									//jump go up y axis!! and no particles...
				anim.SetBool ("Jump", true);
				moveDirection.y = jumpSpeed;
				grounded = false;

			} 

		} else {
			grounded = false;
		}

		if (externalForce.y != 0) {
			moveDirection.y = 0;
		}

		moveDirection += externalForce;
		externalForce = Vector3.zero;

		moveDirection.y -= gravity * Time.deltaTime;							//calculate translation

		controller.Move(moveDirection * Time.deltaTime);

		if (transform.position.y < -100) 											//falling under floor dies;
			gamestate.Instance.SetState (Enums.state.STATE_LOSE);
	}

	IEnumerator ParticlesJump(){
		yield return new WaitForSeconds(0.3f);
		jumpParticles.SetActive (false);
	}

	public void StopPlayer(){
		anim.SetBool("Walk", false);
		walkParticles.SetActive (false);
		jumpParticles.SetActive (false);
		this.enabled = false;
	}

	public void AddForce(Vector3 force){
		
		externalForce += force;
	}
		
}