using UnityEngine;
using System.Collections;

public enum enemyTypeLanded {Simple, Simple_Shooter, Patrol, Patrol_Shooter}
public class StatePatternEnemy : MonoBehaviour {
	
	//NEW
	[Header ("Prefab")]
	public GameObject prefab;
	//END NEW
	
	[Header ("Target")]
	public Transform player;
	[Header ("Fx movement")]
	public ParticleSystem walkParticles;
	#if UNITY_5_3
	ParticleSystem.EmissionModule emWalk;
	#endif
	[Header ("Enemy Settings")]
	public enemyTypeLanded type;
	public enemyState state;
	
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
	[HideInInspector]	public Wait waitState;
	[HideInInspector]	public Alert alertState;
	[HideInInspector]	public Chase chaseState;
	[HideInInspector]	public Patrol patrolState;
	[HideInInspector]	public Idle idleState;
	[HideInInspector]	public NavMeshAgent navMeshAgent;
	[HideInInspector]	public Vector3 startPosition;
	
	
	
	
	void Awake () {

		waitState = new Wait (this);
		alertState	= new Alert(this);
		chaseState	= new Chase(this);
		patrolState	= new Patrol(this);
		idleState	= new Idle (this);
		
		startPosition = transform.position;										//start position for idle enemies
		
		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.speed = speed;
		
		body = GetComponent<Rigidbody> ();									//get phisics
		body.isKinematic = true;
		body.detectCollisions = true;
		#if UNITY_5_3
		emWalk = walkParticles.emission;										//get particles
		emWalk.enabled = false;
		#endif
	}
	
	
	void Start (){
		if (type == enemyTypeLanded.Patrol)												//for non patrol enemies they must being in idle state
			currentState = patrolState;
		else 
			currentState = idleState;
	}
	
	
	void Update () {
		currentState.UpdateState ();											//do the update loop of the current state
			
		//forward direction guizmo
		NavMeshHit hit;
		bool blocked = false;
		blocked = NavMesh.Raycast (eyes.position, navMeshAgent.destination, out hit, NavMesh.AllAreas);
		Debug.DrawLine (eyes.position, navMeshAgent.destination, blocked ? Color.red : Color.green);

		body.AddForce (Vector3.down * speed * GRAVITY);						//gravity
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
