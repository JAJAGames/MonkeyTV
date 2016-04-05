using UnityEngine;
using System.Collections;

public class WaitState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public WaitState(StatePatternEnemy statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.eState = enemyState.WAIT;
	}

	public void UpdateState() {

	}

	public void OnTriggerEnter (Collider other) {
		
	}

	public void ToWaitState() {
		// Can't transition to same state
	}

	public void ToIdleState() {
		enemy.eState = enemyState.IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState() {
		enemy.navMeshAgent.Resume ();
		enemy.eState = enemyState.PATROL;
		enemy.currentState = enemy.patrolState;
	}

	public void ToAlertState() {
		enemy.navMeshAgent.Resume ();
		enemy.eState = enemyState.ALERT;
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState() {
		if (enemy.navMeshAgent.enabled)
			enemy.navMeshAgent.Resume ();
		enemy.eState = enemyState.CHASE;
		enemy.currentState = enemy.chaseState;
	}
}