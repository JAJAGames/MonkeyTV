using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	public ChaseState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
	}


	public void UpdateState()
	{
		Vector3 lookAt = enemy.navMeshAgent.destination;
		lookAt.y = enemy.transform.position.y;
		enemy.transform.LookAt (lookAt);

		enemy.navMeshAgent.destination = enemy.player.position;
	}

	public void OnTriggerEnter (Collider other)
	{}

	public void ToWaitState() {
		enemy.state = enemyState.WAIT;
		enemy.currentState = enemy.waitState;
	}

	public void ToIdleState ()
	{
		enemy.state = enemyState.IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState ()
	{
		enemy.state = enemyState.PATROL;
		enemy.currentState = enemy.patrolState;
	}

	public void ToAlertState ()
	{
		enemy.state = enemyState.ALERT;
		enemy.currentState = enemy.alertState;
	}


	public void ToChaseState ()
	{
		// Can't transition to same state
	}
		
}

