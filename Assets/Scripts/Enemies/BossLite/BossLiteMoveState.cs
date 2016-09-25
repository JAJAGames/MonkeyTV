using UnityEngine;
using System.Collections;
using Enums;

public class BossLiteMoveState : IBossLiteState {

	private readonly StatePatternBossLite statePattern;

	public BossLiteMoveState(StatePatternBossLite statePatternBossLite) {
		statePattern = statePatternBossLite;
	}

	public void UpdateState() {
		if (statePattern.navMeshAgent.remainingDistance == 0)
			ToIdleState ();
	}

	public void ToIdleState () {
		statePattern.currentState = statePattern.idleState;
		statePattern.actualState = BossLiteState.BOSSLITE_IDLE_STATE;
	}

	public void ToMoveState() {
		//Can't change to same State
	}
}
