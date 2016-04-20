﻿using UnityEngine;
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

	private void FixedUpdate ()  {
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
			moveDirection *= movementSpeed;
			moveDirection = transform.TransformDirection(moveDirection);


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

	//Adding phisics to player... 
	void OnControllerColliderHit(ControllerColliderHit other) {
																					//if the player collides with one enemy he can move him depending on their mass
		if (other.gameObject.CompareTag ("Enemy") && controller.isGrounded) { 		//the player must be grtounded

			//Vector direction 
			Vector3 direction = other.transform.position - transform.position;	
			direction.Normalize();
			direction.y = 0;
			direction.z = 0;

			//smoothness
			float smoothPush = 0.5f / movementSpeed;
			// and mass
			float mass = other.gameObject.GetComponent<Rigidbody> ().mass;
			other.transform.position += direction * mass * smoothPush; 
		}		

		if (other.gameObject.CompareTag ("Prop")) {
			Vector3 direction = other.transform.position - transform.position;	
			direction.y = 0;
			direction.Normalize();
			other.gameObject.GetComponent<Rigidbody> ().AddForce(direction ,ForceMode.Impulse); 
		}
	}
}