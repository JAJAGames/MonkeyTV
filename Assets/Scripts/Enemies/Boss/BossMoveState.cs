﻿using UnityEngine;
using System.Collections;
using Enums;

public class BossMoveState : IBossState {

	private readonly StatePatternBoss Boss;
	private Vector3 destination;

	public BossMoveState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
	}

	public void UpdateState() {

		Boss.navMeshAgent.destination = Boss.patrolWayPoints [Boss.nextPatrolWayPoint].position;
		Boss.navMeshAgent.Resume ();

		Vector3 goal = Boss.navMeshAgent.destination;
		goal.y = 0;

		if (Boss.navMeshAgent.remainingDistance <= Boss.navMeshAgent.stoppingDistance && destination == goal) {
			Boss.lastPatrolWayPoint = Boss.nextPatrolWayPoint;
			Boss.nextPatrolWayPoint = (Boss.lastPatrolWayPoint + 1) % Boss.patrolWayPoints.Length;
			if (Boss.lastPatrolWayPoint == 0) {
				if (Boss.phase == BossPhase.OUT_OF_COMBAT)
					ToIdleState ();
				else
					ToPunchState ();
			}
		} else {
			destination = Boss.patrolWayPoints [Boss.nextPatrolWayPoint].position;
			destination.y = 0;
		}
	}

	public void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			if (Boss.lastPatrolWayPoint != 0)
				Boss.nextPatrolWayPoint = Boss.lastPatrolWayPoint;
			ToPunchState ();
		}
	}

	public void ToIdleState () {
		Boss.anim.SetBool ("Walk", false);
		Boss.currentState = Boss.idleState;
		Boss.actualState = BossState.IDLE_SATE;
	}

	public void ToMoveState(){}

	public void ToPunchState() {
				
		Boss.anim.SetTrigger ("Punch");
		Boss.anim.SetBool ("Walk", false);
		gamestate.Instance.SetState (state.STATE_SWAP_CAMERA);
		Boss.actualState = BossState.PUNCH_STATE;
		Boss.currentState = Boss.punchState;
	}

	public void ToDamagedState() {
		Boss.actualState = BossState.DAMAGED_STATE;
		Boss.currentState = Boss.damagedState;
	}
		
}