using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private Vector3 destination;
	public PatrolState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
		enemy.state = enemyState.PATROL;
		enemy.nextWayPoint = 0;
	}

	public void UpdateState()
	{
		enemy.navMeshAgent.destination = enemy.wayPoints [enemy.nextWayPoint].position;
		enemy.navMeshAgent.Resume ();

		Vector3 goal = enemy.navMeshAgent.destination;
		goal.y = 0;

		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance && destination == goal) {
			enemy.nextWayPoint = (enemy.nextWayPoint + 1) % enemy.wayPoints.Length;
		} else {
			destination = enemy.wayPoints [enemy.nextWayPoint].position;
			destination.y = 0;
		}
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
		enemy.state = enemyState.IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState ()
	{
		// Can't transition to same state

	}

	public void ToAlertState ()
	{
		enemy.state = enemyState.ALERT;
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState ()
	{
		enemy.state = enemyState.CHASE;
		enemy.currentState = enemy.chaseState;
	}
	
}
