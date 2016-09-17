using UnityEngine;
using System.Collections;
using Enums;

public class BossDamagedState : IBossState {

	private readonly StatePatternBoss Boss;

	public BossDamagedState(StatePatternBoss statePatternBoss) {
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

	public void ToPunchState() {
		Boss.actualState = BossState.PUNCH_STATE;
		Boss.currentState = Boss.punchState;
	}

	public void ToDamagedState() {}
}