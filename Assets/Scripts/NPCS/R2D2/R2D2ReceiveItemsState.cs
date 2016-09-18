using UnityEngine;
using System.Collections;
using Enums;

public class R2D2ReceiveItemsState : IR2D2State {

	private readonly StatePatternR2D2 R2D2;
	public bool firstTime;

	public R2D2ReceiveItemsState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
		firstTime = true;
	}
		
	public void UpdateState() {
		if (firstTime) {
			R2D2.propDropItemGeneric.ActualizeIngredientsBarNow ();
			firstTime = false;
		}
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {
		R2D2.actualState = R2D2State.IDLE_STATE;
		firstTime = true;
	}

	public void ToMoveState(){
		R2D2.actualState = R2D2State.MOVE_STATE;
		firstTime = true;
	}


	public void ToAskForItemsState() {
		R2D2.actualState = R2D2State.ASK_FOR_ITEMS_STATE;
		firstTime = true;
	}

	public void ToReceiveItemsState() {
		R2D2.actualState = R2D2State.RECEIVE_ITEMS_STATE;
		firstTime = true;
	}
}