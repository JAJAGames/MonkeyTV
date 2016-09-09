using UnityEngine;
using System.Collections;
using Enums;

public class BossMoveState : IBossState {

	private readonly StatePatternBoss Boss;

	public BossMoveState(StatePatternBoss statePatternBoss) {
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

	public void ToMoveState(){}

	public void ToPunchState() {
		Boss.actualState = BossState.PUNCH_STATE;
		Boss.currentState = Boss.punchState;
	}

	public void ToDamagedState() {
		Boss.actualState = BossState.DAMAGED_STATE;
		Boss.currentState = Boss.damagedState;
	}
		
}