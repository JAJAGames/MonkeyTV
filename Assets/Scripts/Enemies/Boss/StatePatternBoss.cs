using UnityEngine;
using System.Collections;
using Enums;

public class StatePatternBoss : MonoBehaviour {



	[Header("Boss Settings")]
	[HideInInspector] public Animator anim;
	public BossState actualState;

	[Header("Boss Route")]
	public Transform[] patrolWayPoints;
	public int lastPatrolWayPoint, nextPatrolWayPoint;
	[HideInInspector]	public NavMeshAgent navMeshAgent;

	[Header("Physics definitions")]
	[Range(2,10)]	public float speed = 4f; 

	[Header("Player References")]
	private GameObject player;
	public PlayerMovement playerMovement;

	[Header("Camera Settings")]
	public CameraFollow cameraFollowing;
	public Transform BossCameraPosition;

	[HideInInspector]	public IBossState currentState;
	[HideInInspector]	public BossMoveState moveState;
	[HideInInspector]	public BossIdleState idleState;
	[HideInInspector]	public BossPunchState punchState;
	[HideInInspector]	public BossDamagedState damagedState;


	void Awake () {

		player = GameObject.Find ("Player");
		playerMovement = player.GetComponent<PlayerMovement>();
		cameraFollowing = Camera.main.GetComponent<CameraFollow>();
		//BossCameraPosition = GameObject.Find ("Boss Camera").transform;
		cameraFollowing.target = BossCameraPosition;
		anim = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent

		navMeshAgent.enabled = true;
		navMeshAgent.speed = speed;
		navMeshAgent.destination = transform.position;

		idleState			= new BossIdleState(this);
		moveState			= new BossMoveState(this);
		punchState			= new BossPunchState(this);
		damagedState 		= new BossDamagedState (this);
		navMeshAgent.baseOffset = 0;
		anim.SetBool ("Walk", false);

	}


	void Start (){
		
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);

		currentState = idleState;
		actualState = BossState.IDLE_SATE;

	}


	void Update () {

		currentState.UpdateState ();
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
		blocked = NavMesh.Raycast (transform.position, navMeshAgent.destination, out hit, NavMesh.AllAreas);
		Debug.DrawLine (transform.position, navMeshAgent.destination, blocked ? Color.red : Color.green);

	}

	private void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);										//execute triggers of states
	}
		
}
