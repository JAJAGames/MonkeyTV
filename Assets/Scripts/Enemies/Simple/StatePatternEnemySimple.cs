using UnityEngine;
using System.Collections;
using Enums;

public class StatePatternEnemySimple : MonoBehaviour {
	[Header ("Target")]
	public Transform player;
	[HideInInspector] public PlayerStats playerStats;
	public GameObject psPlayer;
	public Transform jail;

	[Header ("Enemy Settings")]
	public enemyTypeSimple type;
	public enemyStateSimple actualState;
	public enemyStateSimple inititalState;
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

	public GameObject admirationStick;
	public GameObject admirationSphere;

	[HideInInspector]	public Rigidbody body;
	[HideInInspector]	public Transform chaseTarget;
	[HideInInspector]	public IEnemyStateSimple currentState;
	[HideInInspector]	public ChaseStateSimple chaseState;
	[HideInInspector]	public IdleStateSimple idleState;
	[HideInInspector]	public EscapeStateSimple escapeState;
	[HideInInspector]	public AttackStateSimple attackState;
	[HideInInspector]	public NavMeshAgent navMeshAgent;
	[HideInInspector]	public Vector3 startPosition;
	[HideInInspector]	public Animator animator;
	[HideInInspector]	public SceneFadeInOut sceneFadeInOut;

	void Awake () {
		playerStats = GameObject.FindWithTag ("Player").GetComponent<PlayerStats> ();
		psPlayer = GameObject.Find ("PS_Player_change");
		psPlayer.GetComponent<ParticleSystem>().Stop();
		chaseState	= new ChaseStateSimple(this);
		idleState	= new IdleStateSimple(this);
		escapeState	= new EscapeStateSimple(this);
		attackState = new AttackStateSimple (this);

		startPosition = transform.position;										//start position for idle enemies

		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.speed = speed;

		body = GetComponent<Rigidbody> ();									//get phisics
		body.isKinematic = true;
		body.detectCollisions = true;

		//animator = GetComponent<Animator>();
		animator = transform.GetChild(0).GetComponent<Animator>();

		jail = GameObject.FindWithTag ("Jail").transform;
		sceneFadeInOut = GameObject.Find ("Cameras").GetComponent<SceneFadeInOut> ();
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
		case enemyStateSimple.SIMPLE_STATE_ATTACK:
			currentState = attackState;
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

	public bool AttackPlayer(Transform jail) {
		StartCoroutine (AttackCoroutine(jail));
		return true;
	}

	private IEnumerator AttackCoroutine(Transform jail) {

		admirationStick.SetActive (false);
		admirationSphere.SetActive (false);

		animator.SetBool("Walk",false);
		navMeshAgent.Stop ();
		animator.SetTrigger ("Attack");

		Enums.state previousState = gamestate.Instance.GetState ();
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		player.GetComponent<Animator> ().SetBool ("Walk", false);

		yield return new WaitForSeconds (0.5f);

		player.GetComponent<PickItems> ().throwItem ();
		psPlayer.SetActive (true);
		player.GetComponent<Animator> ().SetTrigger ("Captured");

		yield return new WaitForSeconds (1.0f);

		sceneFadeInOut.FadeToBlack ();
		Vector3 PunchForce = transform.forward;
		PunchForce = PunchForce.normalized * 100;
		player.GetComponent<PlayerMovement> ().AddForce (PunchForce);
		yield return new WaitForSeconds (1.0f);

		actualState = enemyStateSimple.SIMPLE_STATE_IDLE;
		currentState = idleState;
		attackState.attackDone = false;
		player.position = jail.position;

		gamestate.Instance.SetState (previousState);

		yield return new WaitForSeconds (0.5f);
		sceneFadeInOut.FadeToClear ();
	}
}
