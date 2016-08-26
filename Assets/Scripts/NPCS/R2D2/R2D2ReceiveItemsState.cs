using UnityEngine;
using System.Collections;
using Enums;

public class R2D2ReceiveItemsState : IR2D2State {

	private readonly StatePatternR2D2 R2D2;


	public R2D2ReceiveItemsState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
	}

	public void UpdateState() {

	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {
		R2D2.actualState = R2D2State.IDLE_STATE;
	}

	public void ToMoveState(){
		R2D2.actualState = R2D2State.MOVE_STATE;
	}


	public void ToAskForItemsState() {
		R2D2.actualState = R2D2State.ASK_FOR_ITEMS_STATE;
	}

	public void ToReceiveItemsState() {
		R2D2.actualState = R2D2State.RECEIVE_ITEMS_STATE;
	}
}