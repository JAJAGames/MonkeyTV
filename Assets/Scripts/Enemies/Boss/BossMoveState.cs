using UnityEngine;
using System.Collections;
using Enums;

public class BossMoveState : IBossState {

	private readonly StatePatternBoss Boss;
	private Vector3 destination;

	public BossMoveState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
	}

	public void UpdateState() {
		Boss.BossCameraPosition.LookAt (Boss.transform.position);
		Boss.navMeshAgent.destination = Boss.patrolWayPoints [Boss.nextPatrolWayPoint].position;
		Boss.navMeshAgent.Resume ();

		Vector3 goal = Boss.navMeshAgent.destination;
		goal.y = 0;

		if (Boss.navMeshAgent.remainingDistance <= Boss.navMeshAgent.stoppingDistance && destination == goal) {
			Boss.lastPatrolWayPoint = Boss.nextPatrolWayPoint;
			Boss.nextPatrolWayPoint = (Boss.lastPatrolWayPoint + 1) % Boss.patrolWayPoints.Length;
			if (Boss.lastPatrolWayPoint == 0)
				ToPunchState ();
		} else {
			destination = Boss.patrolWayPoints [Boss.nextPatrolWayPoint].position;
			destination.y = 0;
		}
	}

	public void OnTriggerEnter (Collider other) {
		
	}

	public void ToIdleState () {
		Boss.currentState = Boss.idleState;
		Boss.actualState = BossState.IDLE_SATE;
	}

	public void ToMoveState(){}

	public void ToPunchState() {
		Boss.anim.SetBool ("Walk", false);
		Boss.navMeshAgent.baseOffset = 0;
		gamestate.Instance.SetState (state.STATE_SWAP_CAMERA);
		Boss.actualState = BossState.PUNCH_STATE;
		Boss.currentState = Boss.punchState;
	}

	public void ToDamagedState() {
		Boss.actualState = BossState.DAMAGED_STATE;
		Boss.currentState = Boss.damagedState;
	}
		
}