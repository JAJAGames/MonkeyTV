using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState {
	
	//NEW
	float timer;

	private readonly StatePatternEnemy enemy;
	public ChaseState(StatePatternEnemy statePatternEnemy)
	{
		enemy = statePatternEnemy;
	}

	//NEW
	void Start() {
		PoolManager.instance.CreatePool (enemy.prefab, 10);
	}


	public void UpdateState() {
		enemy.animator.SetBool("Walk",true);

		Vector3 lookAt = enemy.navMeshAgent.destination;
		lookAt.y = enemy.transform.position.y;
		enemy.transform.LookAt (lookAt);

		enemy.navMeshAgent.destination = enemy.player.position;

		if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial || enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
			ToIdleState();			
	}

	public void OnTriggerEnter (Collider other)
	{}

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

