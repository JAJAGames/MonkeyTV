using UnityEngine;
using System.Collections;
using Enums;

public class StatePatternBossLite : MonoBehaviour {
	[Header ("Boss Settings")]
	public BossLiteState actualState;
	public BossLiteState inititalState;

	[HideInInspector]	public Animator animator;

	[Header ("NavMeshAgent Points and References")]
	[HideInInspector]	public NavMeshAgent navMeshAgent;
	[Range(2,10)]	public float speed = 4f; 

	[Header ("Boss States")]
	[HideInInspector]	public IBossLiteState currentState;
	[HideInInspector]	public BossLiteIdleState idleState;
	[HideInInspector]	public BossLiteMoveState moveState;

	// Use this for initialization
	void Awake () {
		idleState	= new BossLiteIdleState(this);
		moveState	= new BossLiteMoveState(this);

		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.speed = speed;

		animator = GetComponent<Animator>();
	}

	void Start() {
		currentState = idleState;
		actualState = BossLiteState.BOSSLITE_IDLE_STATE;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
