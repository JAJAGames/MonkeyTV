using UnityEngine;
using System.Collections;
using Enums;

public class StatePatternR2D2 : MonoBehaviour {



	[Header("R2D2 Settings")]

	public R2D2State actualState;
	public bool	isControlPoint;

	[HideInInspector]	public IR2D2State currentState;
	[HideInInspector]	public R2D2MoveState moveState;
	[HideInInspector]	public R2D2IdleState idleState;
	[HideInInspector]	public R2D2AskForItemsState askForItemsState;
	[HideInInspector]	public R2D2ReceiveItemsState receiveItemsState;
	[HideInInspector]	public NavMeshAgent navMeshAgent;

	[Header("Control Points Settings")]
	public Transform R2D2Route;
	public R2D2Poin currentPoint;
	public GameObject[] controlPoints;

	[Header("Player References")]
	private GameObject player;
	public PlayerMovement playerMovement;
	public NavMeshObstacle playerObstacle;

	[Header("Camera Settings")]
	public CameraFollow cameraFollowing;
	public Transform R2D2CameraPosition;

	[Header ("User Interface Items")]
	public GameObject arrow;
	public GameObject canvas;

	void Awake () {
		player = GameObject.Find ("Player");
		playerMovement = player.GetComponent<PlayerMovement>();
		playerObstacle = player.GetComponent<NavMeshObstacle> ();
		cameraFollowing = Camera.main.GetComponent<CameraFollow>();



		idleState			= new R2D2IdleState(this);
		moveState			= new R2D2MoveState(this);
		askForItemsState	= new R2D2AskForItemsState(this);
		receiveItemsState 	= new R2D2ReceiveItemsState (this);

		navMeshAgent = GetComponent<NavMeshAgent>();							//get de agent
		navMeshAgent.enabled = true;
		navMeshAgent.destination = transform.position;

		arrow.SetActive (true);
		canvas.SetActive (false);
	}


	void Start (){
		
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);

		currentState = idleState;
		actualState = R2D2State.IDLE_STATE;



		controlPoints = new GameObject[R2D2Route.childCount];			//get number of controlPoints
		controlPoints = HelperMethods.GetChildren (R2D2Route);			//get the points as gameObjects
		currentPoint = R2D2Poin.INIT;
		isControlPoint = false;

		arrow.transform.position = controlPoints [(int)currentPoint].transform.position;
	}


	void Update () {
		Debug.Log ("R2D2 "+actualState);
		currentState.UpdateState ();
	}	
		

	private void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);										//execute triggers of states
	}
		
}
