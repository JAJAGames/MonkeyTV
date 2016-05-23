using UnityEngine;
using System.Collections;

public enum enemyTypeSimple {SIMPLE_TYPE_RUNNER, SIMPLE_TYPE_JUMPER}
public enum enemyStateSimple {SIMPLE_STATE_IDLE, SIMPLE_STATE_CHASE, SIMPLE_STATE_ESCAPE}

public class StatePatternEnemySimple : MonoBehaviour {
	[Header ("Target")]
	public Transform player;
	public PlayerStats playerStats;

	[Header ("Enemy Settings")]
	public enemyTypeSimple type;
	public enemyStateSimple state;
	public float shootsColldown = 2.0f;
	[Header ("NavMeshAgent Points and References")]
	public Transform eyes;
	public Transform[] wayPoints;
	public int lastWayPoint, nextWayPoint;

	const float GRAVITY = 20f;  
	[Header("Physics definitions")]
	public float yVelocity;
	[Range(2,10)]	public float speed = 4f; 
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
	[HideInInspector]	public IEnemyStateSimple currentState;
	[HideInInspector]	public ChaseStateSimple chaseState;
	[HideInInspector]	public IdleStateSimple idleState;
	[HideInInspector]	public EscapeStateSimple escapeState;
	[HideInInspector]	public NavMeshAgent navMeshAgent;
	[HideInInspector]	public Vector3 startPosition;
	[HideInInspector]	public Animator animator;

	void Awake () {
		playerStats = GameObject.FindWithTag ("Player").GetComponent<PlayerStats> ();

		chaseState	= new ChaseStateSimple(this);
		idleState	= new IdleStateSimple(this);
		escapeState	= new EscapeStateSimple(this);

		startPosition = transform.position;										//start position for idle enemies

		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.speed = speed;

		body = GetComponent<Rigidbody> ();									//get phisics
		body.isKinematic = true;
		body.detectCollisions = true;

		//animator = GetComponent<Animator>();
		animator = transform.GetChild(0).GetComponent<Animator>();
	}


	void Start (){
		if (type == enemyTypeSimple.SIMPLE_TYPE_RUNNER)
			currentState = chaseState;
		else 
			currentState = idleState;
	}


	void Update () {
		CustomUpdate ();
		body.AddForce (Vector3.down * speed * GRAVITY);						//gravity
	}

	public virtual void CustomUpdate(){
		currentState.UpdateState ();											//do the update loop of the current state

		//forward direction guizmo
		NavMeshHit hit;
		bool blocked = false;
		blocked = NavMesh.Raycast (eyes.position, navMeshAgent.destination, out hit, NavMesh.AllAreas);
		Debug.DrawLine (eyes.position, navMeshAgent.destination, blocked ? Color.red : Color.green);

	}

	private void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);										//execute triggers of states
	}


	private void OnCollisionEnter(Collision collision)
	{
		GameObject other = collision.collider.gameObject;

		if (other.CompareTag("Enemy"))												//colliding with other enemies needs to set backward force.
			body.AddForce (- transform.forward * 100);
	}
}
