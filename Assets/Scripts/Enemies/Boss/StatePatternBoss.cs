using UnityEngine;
using System.Collections;
using Enums;

public class StatePatternBoss : MonoBehaviour {



	[Header("Boss Settings")]

	public BossState actualState;

	[Header("Player References")]
	private GameObject player;
	public PlayerMovement playerMovement;
	public NavMeshObstacle playerObstacle;

	[Header("Camera Settings")]
	public CameraFollow cameraFollowing;
	public Transform BossCameraPosition;


	[HideInInspector]	public IBossState currentState;
	[HideInInspector]	public BossMoveState moveState;
	[HideInInspector]	public BossIdleState idleState;
	[HideInInspector]	public BossPunchState punchState;
	[HideInInspector]	public BossDamagedState damagedState;
	[HideInInspector]	public NavMeshAgent navMeshAgent;

	void Awake () {
		player = GameObject.Find ("Player");
		playerMovement = player.GetComponent<PlayerMovement>();
		playerObstacle = player.GetComponent<NavMeshObstacle> ();
		cameraFollowing = Camera.main.GetComponent<CameraFollow>();

		idleState			= new BossIdleState(this);
		moveState			= new BossMoveState(this);
		punchState			= new BossPunchState(this);
		damagedState 		= new BossDamagedState (this);

		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.destination = transform.position;

	}


	void Start (){
		
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);

		currentState = idleState;
		actualState = BossState.IDLE_SATE;

	}


	void Update () {
		Debug.Log ("Boss "+actualState);
		currentState.UpdateState ();
	}	
		

	private void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);										//execute triggers of states
	}
		
}
