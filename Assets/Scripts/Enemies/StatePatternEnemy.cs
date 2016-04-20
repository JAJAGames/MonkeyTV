using UnityEngine;
using System.Collections;

public enum enemyType {Simple,Patrol, JumperSimple, JumperPatrol}
public enum enemyState {WAIT,IDLE,PATROL,CHASE,ALERT}

public class StatePatternEnemy : MonoBehaviour {

	//NEW
	[Header ("Prefab")]
	public GameObject prefab;
	//END NEW

	[Header ("Target")]
	public Transform player;
	[Header ("Fx movement")]
	public ParticleSystem walkParticles;
	public ParticleSystem jumpParticles;
	ParticleSystem.EmissionModule emWalk;
	ParticleSystem.EmissionModule emJump;
	private bool stopJumpParticles;

	[Header ("Enemy Settings")]
	public enemyType type;
	public enemyState state;

	[Header ("NavMeshAgent Points and References")]
	public Transform eyes;
	public Transform[] wayPoints;
	public int lastWayPoint, nextWayPoint;


	const float GRAVITY = 20f;  
	[Header("Physics definitions")]
	public float yVelocity;
	[Range(2,10)]	public float speed = 4f; 
	[Range(10,30)]	public float jumpForce = 20;
	[Range(0,5)]	public float searchingDuration = 1f;
	[Range(1,100)]	public float sightRange = 10f;

	[Header("Navigation Variables & Values")]
	public bool hasPath;
	public bool isOnNavMesh;
	public NavMeshPathStatus pathStatus;
	public Vector3 steeringTarget;
	public bool isPathStale;
	public bool autoRepath;
	public float remainingDistance;

	[HideInInspector]	public Rigidbody body;
	[HideInInspector]	public Transform chaseTarget;
	[HideInInspector]	public IEnemyState currentState;
	[HideInInspector]	public WaitState waitState;
	[HideInInspector]	public AlertState alertState;
	[HideInInspector]	public ChaseState chaseState;
	[HideInInspector]	public PatrolState patrolState;
	[HideInInspector]	public IdleState idleState;
	[HideInInspector]	public NavMeshAgent navMeshAgent;
	[HideInInspector]	public Vector3 startPosition;




	void Awake () {

		waitState	= new WaitState	(this);										//define states
		alertState	= new AlertState  (this);
		chaseState	= new ChaseState  (this);
		patrolState	= new PatrolState (this);
		idleState	= new IdleState (this);

		startPosition = transform.position;										//start position for idle enemies

		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.speed = speed;

		body = GetComponent<Rigidbody> ();									//get phisics
		body.isKinematic = true;
		body.detectCollisions = true;

		emWalk = walkParticles.emission;										//get particles
		emWalk.enabled = false;
		emJump = jumpParticles.emission;
		emJump.enabled = false;
		stopJumpParticles = false;

	}


	void Start ()
	{
		if (type == enemyType.Patrol)												//for non patrol enemies they must being in idle state
			currentState = patrolState;
		else 
			currentState = idleState;
	}


	void Update () 
	{
		if (stopJumpParticles) {													//stop fx landing effect
			stopJumpParticles = false;
			emJump.enabled = false;
		}

		if (navMeshAgent.enabled) {													//navmeshagent is enabled only for landed enemies

			if (emJump.enabled)														//next frame disable landing particles
				stopJumpParticles = true;
														
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

								
			if (type == enemyType.JumperSimple && !blocked)							//can jump with clear path
				Jump ();

		} else {																	//enemy with disabled agent is the same as landed enemy
			emWalk.enabled = false;
			yVelocity = body.velocity.y;
		}
		body.AddForce (Vector3.down * speed * GRAVITY);						//gravity
	}

	private void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);										//execute triggers of states
	}

	private void  OnCollisionStay(Collision collision)
	{
		GameObject other = collision.collider.gameObject;

		if (other.CompareTag ("Floor")) {											//when touch the floor set landed mode:
			emJump.enabled = true;													//enable agent, disable physics and resume navigation
			navMeshAgent.enabled = true;
			body.isKinematic = true;
				
			if (currentState == patrolState)										//if the enemy was in patrol mode he needs to recover the last waypoint.
				navMeshAgent.destination = wayPoints [lastWayPoint].position;
		}else if( body.velocity.y <=0)
			ForceJump ();															//if the other object is not the floor force monkey to jump.
	}

	private void OnCollisionEnter(Collision collision)
	{
		GameObject other = collision.collider.gameObject;

		if (other.CompareTag("Enemy"))												//colliding with other enemies needs to set backward force.
			body.AddForce (- transform.forward * 100);
	}
    

    public void Jump()
	{
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
		body.velocity = Vector3.zero;
		Vector3 velocity = Vector3.up * (jumpForce + Random.Range(-10.0F, 10.0F))   + transform.forward * speed;
		body.AddForce(velocity, ForceMode.VelocityChange);
	}
}
