using UnityEngine;
using System.Collections;
using Enums;

public class BossPunchState : IBossState {

	private readonly StatePatternBoss Boss;

	public BossPunchState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
	}

	public void UpdateState() {
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {
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

}