using UnityEngine;
using System.Collections;

public class AlertState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private float searchTime;

	public AlertState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
		enemy.eState = enemyState.ALERT;
	}


	public void UpdateState()
	{
		Look();
		Search();
	}

	public void OnTriggerEnter (Collider other)
	{

		if (other.gameObject.CompareTag ("Player"))
		{
			enemy.chaseTarget = other.transform;
			ToChaseState();
		}
	}

	public void ToIdleState ()
	{
		enemy.eState = enemyState.IDLE;
		searchTime = 0;
		enemy.navMeshAgent.Resume ();
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState ()
	{
		enemy.eState = enemyState.PATROL;
		searchTime = 0;
		enemy.navMeshAgent.Resume ();
		enemy.currentState = enemy.patrolState;

	}

	public void ToAlertState ()
	{
		// Can't transition to same state
	}

	public void ToChaseState ()
	{
		enemy.eState = enemyState.CHASE;
		enemy.navMeshAgent.Resume ();
		enemy.currentState = enemy.chaseState;
		searchTime = 0;
	}

	private void Look()
	{
		RaycastHit hit;
		if (Physics.Raycast(enemy.eyes.transform.position, enemy.transform.forward, out hit, enemy.sightRange))
		{
			if (hit.collider.CompareTag ("Player"))
			{
				enemy.chaseTarget = hit.collider.transform;
				ToChaseState();
			}
			Debug.DrawLine(enemy.eyes.transform.position, hit.point, Color.blue);
		}
			
	}

	private void Search()
	{
		enemy.navMeshAgent.Stop ();
		enemy.transform.Rotate (0, enemy.navMeshAgent.angularSpeed * Time.deltaTime, 0);
		searchTime += Time.deltaTime;

		if (searchTime >= enemy.searchingDuration) 
		{
			if (enemy.eType == enemyType.Simple || enemy.eType == enemyType.JumperSimple)
				ToIdleState ();
			else
				ToPatrolState ();
		}
	}
}
