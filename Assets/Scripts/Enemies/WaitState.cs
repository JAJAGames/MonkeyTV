using UnityEngine;
using System.Collections;

public class WaitState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public WaitState(StatePatternEnemy statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.state = enemyState.WAIT;
	}

	public void UpdateState() {

	}

	public void OnTriggerEnter (Collider other) {
		
	}

	public void ToWaitState() {
		// Can't transition to same state
	}

	public void ToIdleState() {
		enemy.state = enemyState.IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState() {
		enemy.navMeshAgent.Resume ();
		enemy.state = enemyState.PATROL;
		enemy.currentState = enemy.patrolState;
	}

	public void ToAlertState() {
		enemy.navMeshAgent.Resume ();
		enemy.state = enemyState.ALERT;
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState() {
		if (enemy.navMeshAgent.enabled)
			enemy.navMeshAgent.Resume ();
		enemy.state = enemyState.CHASE;
		enemy.currentState = enemy.chaseState;
	}
}