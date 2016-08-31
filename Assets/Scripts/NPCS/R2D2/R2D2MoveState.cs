using UnityEngine;
using System.Collections;
using Enums;

public class R2D2MoveState : IR2D2State {

	private readonly StatePatternR2D2 R2D2;


	public R2D2MoveState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
	}

	public void UpdateState() {
		if (R2D2.navMeshAgent.remainingDistance == 0)
			ToIdleState ();
	}

	public void OnTriggerEnter (Collider other) {
		
	}

	public void ToIdleState () {
		R2D2.currentState = R2D2.idleState;
		R2D2.actualState = R2D2State.IDLE_STATE;
		R2D2.arrow.GetComponent<BouncingItems> ().ResetPositionY (R2D2.arrow.transform.position.y);
		R2D2.arrow.SetActive (true);
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		R2D2.playerMovement.enabled = true;
		R2D2.playerObstacle.enabled = false;
	}

	public void ToMoveState(){}


	public void ToAskForItemsState() {}

	public void ToReceiveItemsState() {}
		
}