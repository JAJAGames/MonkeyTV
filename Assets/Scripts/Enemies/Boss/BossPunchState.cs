using UnityEngine;
using System.Collections;
using Enums;

public class BossPunchState : IBossState {

	private readonly StatePatternBoss Boss;
	private Vector3 cameraPosition;
	private float shake = 10f;
	private float  shakeAmount = 0.7f;
	private float decreaseFactor = 1.0f;
	private float clock = 5f;

	public BossPunchState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
		cameraPosition = Boss.BossCameraPosition.position;
	}

	public void UpdateState() {
		VisualPunch ();
		clock -= Time.deltaTime;

		if (clock <= 3)
			gamestate.Instance.SetState (state.STATE_CAMERA_FOLLOW_PLAYER);
		
		if (clock <= 0)
			ToIdleState ();
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {
		clock = 5f;
		Boss.currentState = Boss.idleState;
		Boss.actualState = BossState.IDLE_SATE;
	}

	public void ToMoveState(){
		Boss.actualState = BossState.MOVE_STATE;
		Boss.currentState = Boss.moveState;
	}

	public void ToPunchState() {}

	public void ToDamagedState() {
		Boss.actualState = BossState.DAMAGED_STATE;
		Boss.currentState = Boss.damagedState;
	}

	private void ShakeCamera(){
		
		if (shake > 0) {
			Boss.BossCameraPosition.position += Random.insideUnitSphere * shakeAmount;
			shake -= Time.deltaTime * decreaseFactor;

		} else {
			shake = 0.0f;

		}
	}

	private void VisualPunch(){
		if (clock > 3 && clock < 4)
			ShakeCamera ();
		else
			Boss.BossCameraPosition.position = cameraPosition;
	}

}