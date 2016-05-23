﻿using UnityEngine;
using System.Collections;

public class EscapeStateSimple : IEnemyStateSimple {

	private readonly StatePatternEnemySimple enemy;
	private Vector3 destination;
	private float timePlayerChasing;
	private int auxWayPoint;

	public EscapeStateSimple(StatePatternEnemySimple statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.state = enemyStateSimple.SIMPLE_STATE_ESCAPE;
		enemy.nextWayPoint = Random.Range (0, enemy.wayPoints.Length - 1);
	}

	public void UpdateState() {
		enemy.animator.SetBool("Walk",true);

		enemy.navMeshAgent.destination = enemy.wayPoints [enemy.nextWayPoint].position;
		enemy.navMeshAgent.Resume ();

		if (!enemy.playerStats.uniformBonusActive()) {
			ToChaseState ();
		}
	}

	public void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			changeWayPoint();
		}
	}

	public void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			changeWayPoint();
		}
	}

	private void changeWayPoint() {
		auxWayPoint = Random.Range (0, enemy.wayPoints.Length - 1);
		if (auxWayPoint == enemy.nextWayPoint)
			++auxWayPoint;
		enemy.nextWayPoint = auxWayPoint;
	}

	public void ToEscapeState() {
		// Can't transition to same state
	}

	public void ToIdleState() {
		enemy.state = enemyStateSimple.SIMPLE_STATE_IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToChaseState() {
		enemy.navMeshAgent.Resume ();
		enemy.state = enemyStateSimple.SIMPLE_STATE_CHASE;
		enemy.currentState = enemy.chaseState;
	}
}