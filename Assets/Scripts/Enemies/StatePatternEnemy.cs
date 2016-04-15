using UnityEngine;
using System.Collections;

public enum enemyType {Simple,Patrol, JumperSimple, JumperPatrol}
public enum enemyState {WAIT,IDLE,PATROL,CHASE,ALERT}

public class StatePatternEnemy : MonoBehaviour {

	public Transform player;

	[Header ("Enemy Settings")]
	public enemyType type;
	public enemyState state;

	[Header ("NavMeshAgent Points and References")]
	public Transform eyes;
	public Transform[] wayPoints;
	public int lastWayPoint, nextWayPoint;


	const float GRAVITY = 20f;  
	[Header("Physics definitions")]
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

	[HideInInspector]	public Rigidbody rigidbody;
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

		waitState	= new WaitState	(this);
		alertState	= new AlertState  (this);
		chaseState	= new ChaseState  (this);
		patrolState	= new PatrolState (this);
		idleState	= new IdleState (this);
		navMeshAgent = GetComponent<NavMeshAgent>();
		navMeshAgent.enabled = true;
		rigidbody = GetComponent<Rigidbody> ();
		rigidbody.isKinematic = true;
		rigidbody.detectCollisions = true;
		navMeshAgent.speed = speed;
		startPosition = transform.position;

	}


	void Start ()
	{
		if (type == enemyType.Patrol)
			currentState = patrolState;
		else 
			currentState = idleState;
	}


	void Update () 
	{
		if (navMeshAgent.enabled) 
		{
																			//Trace Navmesh Agent status on inspector
			hasPath 		= navMeshAgent.hasPath;
			isOnNavMesh 	= navMeshAgent.isOnNavMesh;
			pathStatus		= navMeshAgent.pathStatus;
			steeringTarget 	= navMeshAgent.steeringTarget;
			isPathStale 	= navMeshAgent.isPathStale;
			autoRepath		= navMeshAgent.autoRepath;
			remainingDistance = navMeshAgent.remainingDistance;


			currentState.UpdateState ();

			NavMeshHit hit;
			bool blocked = false;
			blocked = NavMesh.Raycast(eyes.position, navMeshAgent.destination, out hit, NavMesh.AllAreas);
			Debug.DrawLine(eyes.position, navMeshAgent.destination, blocked ? Color.red : Color.green);

			if (blocked)
				Debug.DrawRay(hit.position, Vector3.up, Color.red);

			if (type == enemyType.JumperSimple && !blocked)
				Jump();

		}
		rigidbody.AddForce (Vector3.down * speed * GRAVITY);
	

	}

	private void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);
	}

	private void  OnCollisionStay(Collision collision)
	{
		GameObject other = collision.collider.gameObject;

		if (other.CompareTag ("Floor")) {
			navMeshAgent.enabled = true;
			rigidbody.isKinematic = true;
			navMeshAgent.Resume ();
			if (currentState == patrolState)
				navMeshAgent.destination = wayPoints [lastWayPoint].position;
		}
	}

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.collider.gameObject;

        if (!other.CompareTag("Floor") && rigidbody.velocity.y <=0)
        {
			ForceJump ();
        }
    }

    public void Jump()
	{
		Vector3 dest = navMeshAgent.destination - transform.position;
		dest.y = 0;
		float alpha = Vector3.Angle(dest.normalized, transform.forward.normalized);
		if (alpha < 5) {
			navMeshAgent.enabled = false;
			rigidbody.isKinematic = false;
			Vector3 velocity = Vector3.up * (jumpForce + Random.Range(-5.0F, 5.0F)) + transform.forward * speed;
			rigidbody.AddForce (velocity, ForceMode.VelocityChange);
		}
	}

	public void ForceJump()
	{
		Vector3 velocity = Vector3.zero;
		rigidbody.velocity = velocity;
		velocity = Vector3.up * (jumpForce + Random.Range(-5.0F, 5.0F))   + transform.forward * speed;
		rigidbody.AddForce(velocity, ForceMode.VelocityChange);
	}
}
