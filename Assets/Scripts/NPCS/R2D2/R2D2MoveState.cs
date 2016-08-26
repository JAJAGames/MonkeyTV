using UnityEngine;
using System.Collections;
using Enums;

public class R2D2MoveState : IR2D2State {

	private readonly StatePatternR2D2 R2D2;


	public R2D2MoveState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
	}

	public void UpdateState() {
		
	}

	public void OnTriggerEnter (Collider other) {
		
	}

	public void ToIdleState () {
		R2D2.currentState = R2D2.idleState;
		R2D2.actualState = R2D2State.IDLE_STATE;
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		R2D2.cameraFollowing.target = R2D2.player;
	}

	public void ToMoveState(){}


	public void ToAskForItemsState() {}

	public void ToReceiveItemsState() {}
		
}