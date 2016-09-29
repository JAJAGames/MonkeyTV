using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class R2D2AskForItemsState : IR2D2State {


	private readonly StatePatternR2D2 R2D2;


	public R2D2AskForItemsState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
	}

	public void UpdateState() {
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action3 || Input.GetButton("Pick")) {
			ToReceiveItemsState ();
		}
			
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {
		R2D2.currentState = R2D2.idleState;
		R2D2.actualState = R2D2State.IDLE_STATE;
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
	}

	public void ToMoveState(){
		R2D2.currentPoint++;
		R2D2.navMeshAgent.destination = R2D2.controlPoints [(int) R2D2.currentPoint].transform.position;

		R2D2.arrow.SetActive (false);
		R2D2.playerMovement.enabled = false;
		R2D2.playerObstacle.enabled = true;

		R2D2.cameraFollowing.target = R2D2.R2D2CameraPosition;
		gamestate.Instance.SetState(Enums.state.STATE_SWAP_CAMERA);



		R2D2.actualState = R2D2State.MOVE_STATE;
		R2D2.currentState = R2D2.moveState;
	}


	public void ToAskForItemsState() {
		
	}

	public void ToReceiveItemsState() {
		
		//R2D2.canvas.SetActive (false);
		//R2D2.playerMovement.enabled = true;

		//gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);

		R2D2.actualState = R2D2State.RECEIVE_ITEMS_STATE;
		R2D2.currentState = R2D2.receiveItemsState;

		R2D2.newCraft.StartNewCraft();

	}
}