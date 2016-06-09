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

	private Vector3 moveDirection = Vector3.zero;
	public CharacterController controller;
	public ParticleSystem walkParticles;
	public ParticleSystem jumpParticles;
	private Animator anim;
	private PlayerStats pStats;
#if UNITY_5_3
	ParticleSystem.EmissionModule emWalk;
	ParticleSystem.EmissionModule emJump;
#endif	
	public bool grounded;
	private void Awake ()  {
		controller = GetComponent<CharacterController> ();
		anim = GetComponent<Animator>();
#if UNITY_5_3
		emWalk = walkParticles.emission;
		emWalk.enabled = false;
		emJump = jumpParticles.emission;
		emJump.enabled = false;
#endif
		grounded = false;
	}

	private void FixedUpdate ()  {
		
					
		if (gamestate.Instance.GetState () == Enums.state.STATE_LOSE || gamestate.Instance.GetState() == Enums.state.STATE_PLAYER_PAUSED)		//skip update for game Losed 
			return;
																			//no update for deads... no Zombies please!
#if UNITY_5_3
		if (emJump.enabled)
			emJump.enabled = false;
#endif																		//if grounded get input
		if (controller.isGrounded) {
			anim.SetBool("Jump",false);

			if (!grounded) {
				grounded = true;
#if UNITY_5_3
				emJump.enabled = true;
#endif
			} 

			var inputDevice = InputManager.ActiveDevice;
			moveDirection = new Vector3(inputDevice.LeftStickX, 0, inputDevice.LeftStickY);
																		//Make the player face his movement direction. We ought to disable rotation script
			if (moveDirection != Vector3.zero)							//Look to rotation viewing is not Zero
				transform.rotation = Quaternion.LookRotation( moveDirection);

			moveDirection *= movementSpeed;
			//moveDirection = transform.TransformDirection(moveDirection);  //This line set transform from local space movement to world movement 

			if (moveDirection != Vector3.zero)
				anim.SetBool("Walk", true);
			else
				anim.SetBool("Walk", false);
#if UNITY_5_3
			if (moveDirection != Vector3.zero) 								//enable or disable emitter of ParticleSystem
			{
				emWalk.enabled = true;
			}
			else
				emWalk.enabled = false;
#endif
			if (inputDevice.Action1) 									//jump go up y axis!! and no particles...
			{
				anim.SetBool("Jump",true);
				//anim.GetCurrentAnimatorStateInfo(0).length;
				moveDirection.y = jumpSpeed;
				grounded = false;
#if UNITY_5_3
				emWalk.enabled = false;
#endif
			} 
				
		}

		moveDirection.y -= gravity * Time.deltaTime;							//calculate translation
		controller.Move(moveDirection * Time.deltaTime);

		if (transform.position.y < -100) 											//falling under floor dies;
			gamestate.Instance.SetState (Enums.state.STATE_LOSE);
		
	}
	
	//Adding phisics to player... 
	void OnControllerColliderHit(ControllerColliderHit other) {
																					//if the player collides with one enemy he can move him depending on their mass
		if (other.gameObject.CompareTag ("Enemy") && controller.isGrounded) { 		//the player must be grounded

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

	float ShowCurrentClipLength()
	{
		return ( anim.GetCurrentAnimatorStateInfo(0).length);
	}

	public void StopPlayer(){
		anim.SetBool("Walk", false);
#if UNITY_5_3
		emWalk.enabled = false;
#endif
		this.enabled = false;

	}
}