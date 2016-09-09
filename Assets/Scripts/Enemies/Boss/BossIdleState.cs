using UnityEngine;
using System.Collections;
using Enums;

public class BossIdleState : IBossState {

	private readonly StatePatternBoss Boss;

	public BossIdleState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
	}

	public void UpdateState() {
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {}

	public void ToMoveState(){
		Boss.actualState = BossState.MOVE_STATE;
		Boss.currentState = Boss.moveState;
	}
		
	public void ToPunchState() {
		Boss.actualState = BossState.PUNCH_STATE;
		Boss.currentState = Boss.punchState;
	}

	public void ToDamagedState() {
		Boss.actualState = BossState.DAMAGED_STATE;
		Boss.currentState = Boss.damagedState;
	}

}