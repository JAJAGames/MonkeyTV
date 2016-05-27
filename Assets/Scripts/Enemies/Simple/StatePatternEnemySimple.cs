using UnityEngine;
using System.Collections;
using Enums;

public class StatePatternEnemySimple : MonoBehaviour {
	[Header ("Target")]
	public Transform player;
	[HideInInspector] public PlayerStats playerStats;

	[Header ("Enemy Settings")]
	public enemyTypeSimple type;
	public enemyStateSimple actualState;
	public enemyStateSimple inititalState;
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
		switch (inititalState) {
		case enemyStateSimple.SIMPLE_STATE_IDLE:
			currentState = idleState;
			break;
		case enemyStateSimple.SIMPLE_STATE_CHASE:
			currentState = chaseState;
			break;
		case enemyStateSimple.SIMPLE_STATE_ESCAPE:
			currentState = escapeState;
			break;
		}
	}


	void Update () {

		CustomUpdate ();
		body.AddForce (Vector3.down * speed * GRAVITY);						//gravity

	}	

	public virtual void CustomUpdate(){
		currentState.UpdateState ();											//do the update loop of the current state
		//Look at
		Vector3 lookAt = navMeshAgent.destination;
		lookAt.y = transform.position.y;
		transform.LookAt (lookAt);
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
