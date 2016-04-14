using UnityEngine;
using System.Collections;

public enum enemyType {Simple,Patrol, JumperSimple, JumperPatrol}
public enum enemyState {WAIT,IDLE,PATROL,CHASE,ALERT}

public class StatePatternEnemy : MonoBehaviour {

	const float GRAVITY = 20f;

	public float speed = 4f;
	float timeLeftJump = 0.0f;
	public float jumpForce = 7;
	public float searchingDuration = 10f;
	public float sightRange = 10f;
	public Transform eyes;
	public Transform[] wayPoints;
	public int lastWayPoint, nextWayPoint;
	public enemyType eType;
	public enemyState eState;

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
		if (eType == enemyType.Patrol)
			currentState = patrolState;
		else 
			currentState = idleState;
	}


	void Update () 
	{

		if (navMeshAgent.enabled) 
		{
			currentState.UpdateState ();
			if (eType == enemyType.JumperSimple && timeLeftJump < 0)
				Jump();

		}
		rigidbody.AddForce (Vector3.down * speed * GRAVITY);
		timeLeftJump -= Time.deltaTime;
	

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
            Vector3 velocity = Vector3.zero;
            rigidbody.velocity = velocity;
            velocity = Vector3.up * jumpForce + transform.forward * speed;
            rigidbody.AddForce(velocity, ForceMode.VelocityChange);
        }
    }

    private void Jump()
	{
		Vector3 dest = navMeshAgent.destination - transform.position;
		dest.y = 0;
		float alpha = Vector3.Angle(dest.normalized, transform.forward.normalized);
		if (alpha < 5) {
			navMeshAgent.enabled = false;
			rigidbody.isKinematic = false;
			Vector3 velocity = Vector3.up * jumpForce + transform.forward * speed;
			rigidbody.AddForce (velocity, ForceMode.VelocityChange);
		}
	}
}
