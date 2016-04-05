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
		Look ();
		Chase ();
	}

	public void OnTriggerEnter (Collider other)
	{}

	public void ToWaitState() {
		enemy.eState = enemyState.WAIT;
		enemy.currentState = enemy.waitState;
	}

	public void ToIdleState ()
	{
		enemy.eState = enemyState.IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState ()
	{
		enemy.eState = enemyState.PATROL;
		enemy.currentState = enemy.patrolState;
	}

	public void ToAlertState ()
	{
		enemy.eState = enemyState.ALERT;
		enemy.currentState = enemy.alertState;
	}


	public void ToChaseState ()
	{
		// Can't transition to same state
	}

	private void Look()
	{
		RaycastHit hit;
		Vector3 enemyToTarget = enemy.chaseTarget.position  - enemy.eyes.transform.position;

		if (Physics.Raycast (enemy.eyes.transform.position, enemyToTarget, out hit, enemy.sightRange)) {
			if (hit.collider.CompareTag ("Player"))
					enemy.chaseTarget = hit.transform;
		}	else
				ToAlertState ();
		Debug.DrawRay(enemy.eyes.transform.position, enemyToTarget, Color.red);

	}

	private void Chase()
	{
		enemy.navMeshAgent.destination = enemy.chaseTarget.position;
	}
}

