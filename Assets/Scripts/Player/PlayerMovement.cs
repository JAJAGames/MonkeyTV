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
	ParticleSystem.EmissionModule em;
	private void Awake ()  {
		controller = GetComponent<CharacterController> ();
		em = walkParticles.emission;
		em.enabled = false;
	}

	private void Update ()  {
																			//no update for deads... no Zombies please!
		if (IsDead)
			return;
		
																			//if grounded get input
		if (controller.isGrounded) {
			
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection = transform.TransformDirection(moveDirection);
			moveDirection *= movementSpeed;

			if (moveDirection != Vector3.zero) 								//enable or disable emitter of ParticleSystem
			{
				em.enabled = true;
			}
			else
				em.enabled = false;

			if (Input.GetButton ("Jump")) 									//jump go up y axis!! and no particles...
			{
				moveDirection.y = jumpSpeed;
				em.enabled = false;
			} 

		}

		moveDirection.y -= gravity * Time.deltaTime;						//calculate translation
		controller.Move(moveDirection * Time.deltaTime);
	}
}