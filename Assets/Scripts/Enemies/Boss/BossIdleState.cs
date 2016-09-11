using UnityEngine;
using System.Collections;
using Enums;

public class BossIdleState : IBossState {

	private readonly StatePatternBoss Boss;

	public BossIdleState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
	}

	public void UpdateState() {
		
		if (Boss.batteries > 0)
			ToMoveState ();
		else {
			Boss.anim.SetBool ("Dead", true);
			Boss.actualState = BossState.DEAD_STATE;
		}
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {}

	public void ToMoveState(){
		Boss.navMeshAgent.enabled = true;
		Boss.anim.SetBool ("Walk", true);
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