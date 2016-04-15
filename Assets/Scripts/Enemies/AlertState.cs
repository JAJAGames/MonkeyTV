using UnityEngine;
using System.Collections;

public class AlertState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private float searchTime;

	public AlertState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
		enemy.state = enemyState.ALERT;
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

	public void ToWaitState() {
		enemy.state = enemyState.WAIT;
		enemy.currentState = enemy.waitState;
	}

	public void ToIdleState ()
	{
		enemy.state = enemyState.IDLE;
		searchTime = 0;
		enemy.navMeshAgent.Resume ();
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState ()
	{
		enemy.state = enemyState.PATROL;
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
		enemy.state = enemyState.CHASE;
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
			if (enemy.type == enemyType.Simple || enemy.type == enemyType.JumperSimple)
				ToIdleState ();
			else
				ToPatrolState ();
		}
	}
}
