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
		
		Vector3 lookAt = enemy.navMeshAgent.destination;
		lookAt.y = enemy.transform.position.y;
		enemy.transform.LookAt (lookAt);

		enemy.navMeshAgent.destination = enemy.player.position;

		//NEW
		timer += Time.deltaTime;
		if (enemy.type == enemyType.Simple_Shooter || enemy.type == enemyType.Patrol_Shooter)
			if (Vector3.Distance(enemy.transform.position, enemy.player.position) < 20.0f && timer >= enemy.shootsColldown) {
				Shoot ();
			}
					
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

	//NEW
	public void Shoot() {
		timer = 0f;
		PoolManager.instance.ReuseObject (enemy.prefab, enemy.transform.position + new Vector3(0.0f, 1.0f, 0.0f), enemy.transform.rotation);
	}
		
}

