using UnityEngine;
using System.Collections;

public class IdleState : IEnemyState {

	private readonly StatePatternEnemy enemy;

	public IdleState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
		enemy.state = enemyState.IDLE;
	}

	public void UpdateState()
	{
		enemy.animator.SetBool("Walk",false);

		if (enemy.transform.position != enemy.startPosition) {
			enemy.navMeshAgent.destination = enemy.startPosition; 
			enemy.navMeshAgent.Resume ();
		} else
			enemy.navMeshAgent.Stop ();
	}

	public void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag ("Player"))
		{
			enemy.chaseTarget = other.transform;
			ToChaseState();
		}
	}

	public void ToMeleAttackState() {
		enemy.state = enemyState.MELEATTACK;
		enemy.currentState = enemy.meleAttack;
	}

	public void ToIdleState ()
	{
		// Can't transition to same state
	}

	public void ToPatrolState ()
	{
		enemy.navMeshAgent.Resume ();
		enemy.state = enemyState.PATROL;
		enemy.currentState = enemy.patrolState;
	}


	public void ToAlertState ()
	{
		enemy.navMeshAgent.Resume ();
		enemy.state = enemyState.ALERT;
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState ()
	{
		if (enemy.navMeshAgent.enabled)
				enemy.navMeshAgent.Resume ();
		enemy.state = enemyState.CHASE;
		enemy.currentState = enemy.chaseState;
	}

}