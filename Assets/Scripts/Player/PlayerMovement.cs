using UnityEngine;
using System.Collections;

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
	public bool grounded;
#endif

	private void Awake ()  {
		controller = GetComponent<CharacterController> ();
		anim = transform.GetChild(0).GetComponent<Animator>();
		pStats = gameObject.GetComponent<PlayerStats> ();
#if UNITY_5_3
		emWalk = walkParticles.emission;
		emWalk.enabled = false;
		emJump = jumpParticles.emission;
		emJump.enabled = false;
		grounded = false;
#endif
	}

	private void FixedUpdate ()  {
																			//no update for deads... no Zombies please!
		if (pStats.isDead)
			return;
#if UNITY_5_3
		if (emJump.enabled)
			emJump.enabled = false;
#endif																		//if grounded get input
		if (controller.isGrounded) {
			anim.SetBool("jump",false);
#if UNITY_5_3
			if (!grounded) {
				grounded = true;
				emJump.enabled = true;
			} 
#endif
			moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			moveDirection *= movementSpeed;
			moveDirection = transform.TransformDirection(moveDirection);
			if (moveDirection != Vector3.zero)
				anim.SetBool("walk", true);
			else
				anim.SetBool("walk", false);
#if UNITY_5_3
			if (moveDirection != Vector3.zero) 								//enable or disable emitter of ParticleSystem
			{
				emWalk.enabled = true;
			}
			else
				emWalk.enabled = false;
#endif
			if (Input.GetButton ("Jump")) 									//jump go up y axis!! and no particles...
			{
				anim.SetBool("jump",true);
				//anim.GetCurrentAnimatorStateInfo(0).length;
				moveDirection.y = jumpSpeed;
				moveDirection.y = jumpSpeed;
#if UNITY_5_3
				grounded = false;
				emWalk.enabled = false;
#endif
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

	float ShowCurrentClipLength()
	{
		return ( anim.GetCurrentAnimatorStateInfo(0).length);
	}

}