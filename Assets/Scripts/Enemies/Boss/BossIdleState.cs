using UnityEngine;
using System.Collections;
using Enums;

public class BossIdleState : IBossState {

	private readonly StatePatternBoss Boss;
	private float clock = Constants.BOSS_IDDLE_TIME;

	public BossIdleState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
	}

	public void UpdateState() {

		if (Boss.batteries > 0) {
			if (Boss.phase != BossPhase.OUT_OF_COMBAT)
				ToMoveState ();
		}
		else {
			Boss.anim.SetBool ("Dead", true);
			Boss.actualState = BossState.DEAD_STATE;
		}

		TimerCountDown ();
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {}

	public void ToMoveState(){
		Boss.anim.SetBool ("Walk", true);
		Boss.actualState = BossState.MOVE_STATE;
		Boss.currentState = Boss.moveState;
	}
		
	public void ToPunchState() {
		if (Boss.phase == BossPhase.OUT_OF_COMBAT)
			return;
		Boss.actualState = BossState.PUNCH_STATE;
		Boss.currentState = Boss.punchState;
	}

	public void ToDamagedState() {
		Boss.actualState = BossState.DAMAGED_STATE;
		Boss.currentState = Boss.damagedState;
	}

	private void TimerCountDown(){
		clock -= Time.deltaTime;
		if (clock <= 0) {
			clock = Constants.BOSS_IDDLE_TIME;
			ToMoveState ();
		}
	}

}