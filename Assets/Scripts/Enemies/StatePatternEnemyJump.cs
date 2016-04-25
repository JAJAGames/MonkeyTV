using UnityEngine;
using System.Collections;

public class StatePatternEnemyJump : StatePatternEnemy {


	[Range(5,15)]	public float jumpForce = 10;
			
	override public void CustomUpdate(){ 

		if (navMeshAgent.enabled) {													//navmeshagent is enabled only for landed enemies
			//Trace Navmesh Agent status on inspector
			hasPath = navMeshAgent.hasPath;
			isOnNavMesh = navMeshAgent.isOnNavMesh;
			pathStatus = navMeshAgent.pathStatus;
			steeringTarget = navMeshAgent.steeringTarget;
			isPathStale = navMeshAgent.isPathStale;
			autoRepath = navMeshAgent.autoRepath;
			remainingDistance = navMeshAgent.remainingDistance;

			currentState.UpdateState ();											//do the update loop of the current state

			//forward direction guizmo
			NavMeshHit hit;
			bool blocked = false;
			blocked = NavMesh.Raycast (eyes.position, navMeshAgent.destination, out hit, NavMesh.AllAreas);
			Debug.DrawLine (eyes.position, navMeshAgent.destination, blocked ? Color.red : Color.green);

					
			if (!blocked)														//can jump with clear path
				Jump ();
		}
		else {																	//enemy with disabled agent is the same as landed enemy

			yVelocity = body.velocity.y;
		}
	}


	private void  OnCollisionStay(Collision collision)
	{
		GameObject other = collision.collider.gameObject;
		if (other.CompareTag ("Floor") && currentState != idleState) {											//when touch the floor set landed mode:

			navMeshAgent.enabled = true;
			body.isKinematic = true;
			animator.SetBool("Jump",false);
			if (currentState == patrolState)										//if the enemy was in patrol mode he needs to recover the last waypoint.
				navMeshAgent.destination = wayPoints [lastWayPoint].position;
		}else if( body.velocity.y <=0)
			ForceJump ();															//if the other object is not the floor force monkey to jump.
	}
    

    public void Jump()
	{
		animator.SetBool("Jump",true);
		Vector3 dest = navMeshAgent.destination - transform.position;				//get the angle between direction and destination
		dest.y = 0;
		float alpha = Vector3.Angle(dest.normalized, transform.forward.normalized);
		if (alpha < 5) {															//if the angles is lower than 5 degrees the monkey enemy can jump
			navMeshAgent.enabled = false;
			body.isKinematic = false;
			Vector3 velocity = Vector3.up * (jumpForce + Random.Range(-10.0F, 10.0F)) + transform.forward * speed; //we add a random value to the jump force.
			body.AddForce (velocity, ForceMode.VelocityChange);
		}
	}

	public void ForceJump()															//to do the jumps is necessary to set their velocity to 0 first;
	{
		animator.SetBool("Jump",true);
		body.velocity = Vector3.zero;
		Vector3 velocity = Vector3.up * (jumpForce + Random.Range(-jumpForce/4, jumpForce/2))   + transform.forward * speed;
		body.AddForce(velocity, ForceMode.VelocityChange);
	}
}
