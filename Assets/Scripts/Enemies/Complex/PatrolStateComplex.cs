using UnityEngine;
using System.Collections;
using Enums;

public class PatrolStateComplex : IEnemyStateComplex {

	private readonly StatePatternEnemyComplex enemy;
	private Vector3 destination;

	public PatrolStateComplex(StatePatternEnemyComplex statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_PATROL;

		enemy.nextEscapeWayPoint = 0;
		enemy.nextPatrolWayPoint = 0;
	}

	public void UpdateState() {
		enemy.animator.SetBool ("Walk", true);

		enemy.navMeshAgent.destination = enemy.patrolWayPoints [enemy.nextPatrolWayPoint].position;
		enemy.navMeshAgent.Resume ();

		Vector3 goal = enemy.navMeshAgent.destination;
		goal.y = 0;

		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && destination == goal) {
			enemy.nextPatrolWayPoint = (enemy.nextPatrolWayPoint + 1) % enemy.patrolWayPoints.Length;
		} else {
			destination = enemy.patrolWayPoints [enemy.nextPatrolWayPoint].position;
			destination.y = 0;
		}
	}

	public void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Enemy")) {
			StatePatternEnemyComplex oState = other.GetComponent<StatePatternEnemyComplex> ();
			if (oState.currentState == oState.chaseState) {
				enemy.chaseTarget = oState.chaseTarget;
				ToChaseState();
			} else {
				enemy.nextPatrolWayPoint = (enemy.nextPatrolWayPoint + 1) % enemy.patrolWayPoints.Length;
			}

		}
		if (other.gameObject.CompareTag ("Player")) {
			enemy.chaseTarget = other.transform;
			ToChaseState();
		}
	}

	public void ToEscapeState() {
		enemy.admirationStick.SetActive (false);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_ESCAPE;
		enemy.currentState = enemy.escapeState;
	}

	public void ToIdleState() {
		enemy.animator.SetBool("Walk",false);

		enemy.actualState = enemyStateComplex.COMPLEX_STATE_IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToChaseState() {
		enemy.animator.SetBool("Escape",false);
		enemy.animator.SetBool("Walk",true);

		enemy.admirationStick.SetActive (true);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_CHASE;
		enemy.currentState = enemy.chaseState;
	}

	public void ToAttackState() {
		enemy.admirationStick.SetActive (false);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_ATTACK;
		enemy.currentState = enemy.attackState;
	}

	public void ToPatrolState() {
		// Can't transition to same state
	}
}
