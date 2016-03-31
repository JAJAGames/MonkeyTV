﻿using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState {

	private readonly StatePatternEnemy enemy;
	private Vector3 destination;
	private float timer;
	private float time;
	public PatrolState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
		enemy.eState = enemyState.PATROL;
		enemy.nextWayPoint = 0;
		timer = 0;
		time = 0;
	}

	public void UpdateState()
	{
		Patrol ();
		time += Time.deltaTime;
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
		enemy.currentState = enemy.idleState;
	}

	public void ToPatrolState ()
	{
		// Can't transition to same state

	}

	public void ToAlertState ()
	{
		enemy.eState = enemyState.ALERT;
		enemy.currentState = enemy.alertState;
	}

	public void ToChaseState ()
	{
		enemy.eState = enemyState.CHASE;
		enemy.currentState = enemy.chaseState;
	}
	

	void Patrol()
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
	
}
