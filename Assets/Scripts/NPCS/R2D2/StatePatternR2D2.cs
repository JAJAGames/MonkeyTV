using UnityEngine;
using System.Collections;
using Enums;

public class StatePatternR2D2 : MonoBehaviour {


	public R2D2State actualState;
	[HideInInspector]	public IR2D2State currentState;
	[HideInInspector]	public R2D2NextPointState nextPointState;
	[HideInInspector]	public R2D2IdleState idleState;
	[HideInInspector]	public R2D2RepairLevelerState repairLevelerState;



	void Awake () {

		idleState			= new R2D2IdleState(this);
		nextPointState		= new R2D2NextPointState(this);
		repairLevelerState	= new R2D2RepairLevelerState(this);

		actualState = R2D2State.IDLE_STATE;
	}


	void Start (){
		
		currentState = idleState;
	}


	void Update () {
		currentState.UpdateState ();
	}	
		

	private void OnTriggerEnter (Collider other)
	{
		currentState.OnTriggerEnter (other);										//execute triggers of states
	}
		
}
