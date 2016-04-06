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
 * 06/04/2016	Added ParticleSystem to player. The new code allow to enable and disable emitter 
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class PlayerMovement : MonoBehaviour  {

	public bool IsDead = false;
	public float movementSpeed = 6.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;
	public ParticleSystem walkParticles;
	public ParticleSystem jumpParticles;
	ParticleSystem.EmissionModule emWalk;
	ParticleSystem.EmissionModule emJump;
	public bool grounded;

	private void Awake ()  {
		controller = GetComponent<CharacterController> ();
		emWalk = walkParticles.emission;
		emWalk.enabled = false;
		emJump = jumpParticles.emission;
		emJump.enabled = false;
		grounded = false;
	}

	private void Update ()  {
																			//no update for deads... no Zombies please!
		if (IsDead)
			return;
		if (emJump.enabled)
			emJump.enabled = false;
																			//if grounded get input
		if (controller.isGrounded) {
			if (!grounded) {
				grounded = true;
				emJump.enabled = true;
			} 
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= movementSpeed;

			if (moveDirection != Vector3.zero) 								//enable or disable emitter of ParticleSystem
			{
				emWalk.enabled = true;
			}
			else
				emWalk.enabled = false;

			if (Input.GetButton ("Jump")) 									//jump go up y axis!! and no particles...
			{
				moveDirection.y = jumpSpeed;
				grounded = false;
				emWalk.enabled = false;
			} 

		}

		moveDirection.y -= gravity * Time.deltaTime;						//calculate translation
		controller.Move(moveDirection * Time.deltaTime);
	}
}