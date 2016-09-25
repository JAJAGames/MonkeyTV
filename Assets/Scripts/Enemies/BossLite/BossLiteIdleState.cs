using UnityEngine;
using System.Collections;
using Enums;

public class BossLiteIdleState : IBossLiteState {

	private readonly StatePatternBossLite statePattern;

	public BossLiteIdleState(StatePatternBossLite statePatternBossLite) {
		statePattern = statePatternBossLite;
	}

	public void UpdateState() {
	
	}

	public void ToIdleState () {
		//Can't change to same State
	}

	public void ToMoveState() {
		statePattern.currentState = statePattern.moveState;
		statePattern.actualState = BossLiteState.BOSSLITE_MOVE_STATE;
	}
}
