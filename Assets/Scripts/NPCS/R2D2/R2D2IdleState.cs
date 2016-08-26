using UnityEngine;
using System.Collections;
using Enums;

public class R2D2IdleState : IR2D2State {


	private readonly StatePatternR2D2 R2D2;


	public R2D2IdleState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
	}

	public void UpdateState() {

	}

	public void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Player")) {
			Debug.Log ("enter player");
			ToMoveState ();
		}
	}

	public void ToIdleState () {}

	public void ToMoveState(){
		
		R2D2.currentPoint++;
		R2D2.navMeshAgent.destination = R2D2.controlPoints [R2D2.currentPoint].transform.position;
		R2D2.arrow.transform.position = R2D2.controlPoints [R2D2.currentPoint].transform.position;
		R2D2.arrow.SetActive (false);
		R2D2.actualState = R2D2State.MOVE_STATE;
		R2D2.currentState = R2D2.moveState;
		R2D2.player.enabled = false;
		R2D2.cameraFollowing.target = R2D2.R2D2CameraPosition;
		gamestate.Instance.SetState(Enums.state.STATE_STATIC_CAMERA);
	}


	public void ToAskForItemsState() {
		R2D2.actualState = R2D2State.ASK_FOR_ITEMS_STATE;
	}

	public void ToReceiveItemsState() {}


}