using UnityEngine;
using System.Collections;

public enum enemyType {Simple, Simple_Shooter, Patrol, Patrol_Shooter}
public enum enemyState {IDLE,PATROL,CHASE,ALERT, MELEATTACK}

public class StatePatternEnemy : MonoBehaviour {
	
	//NEW
	[Header ("Prefab")]
	public GameObject prefab;
	//END NEW
	
	[Header ("Target")]
	public Transform player;

	[Header ("Enemy Settings")]
	public enemyType type;
	public enemyState state;
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
	[HideInInspector]	public IEnemyState currentState;
	[HideInInspector]	public MeleAttackState meleAttack;
	[HideInInspector]	public AlertState alertState;
	[HideInInspector]	public ChaseState chaseState;
	[HideInInspector]	public PatrolState patrolState;
	[HideInInspector]	public IdleState idleState;
	[HideInInspector]	public NavMeshAgent navMeshAgent;
	[HideInInspector]	public Vector3 startPosition;
	
	
	

	void Awake () {

		meleAttack = new MeleAttackState (this);
		alertState	= new AlertState(this);
		chaseState	= new ChaseState(this);
		patrolState	= new PatrolState(this);
		idleState	= new IdleState (this);
		
		startPosition = transform.position;										//start position for idle enemies
		
		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.speed = speed;
		
		body = GetComponent<Rigidbody> ();									//get phisics
		body.isKinematic = true;
		body.detectCollisions = true;

	}
	
	
	void Start (){
		if (type == enemyType.Patrol)												//for non patrol enemies they must being in idle state
			currentState = patrolState;
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
