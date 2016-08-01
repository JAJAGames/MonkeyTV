using UnityEngine;
using System.Collections;
using Enums;

public class EscapeStateComplex : IEnemyStateComplex {

	private readonly StatePatternEnemyComplex enemy;
	private Vector3 destination;
	private float timePlayerChasing;

	public EscapeStateComplex(StatePatternEnemyComplex statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_ESCAPE;
		enemy.nextEscapeWayPoint = Random.Range (0, enemy.escapeWayPoints.Length - 1);
	}

	public void UpdateState() {
		if (!enemy.animator.GetBool("Escape"))
			enemy.animator.SetBool("Escape",true);

		enemy.navMeshAgent.destination = enemy.escapeWayPoints [enemy.nextEscapeWayPoint].position;
		enemy.navMeshAgent.Resume ();

		if (enemy.navMeshAgent.remainingDistance <= enemy.navMeshAgent.stoppingDistance)
			changeWayPoint ();
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

		enemy.nextEscapeWayPoint = Random.Range (0, enemy.escapeWayPoints.Length - 1);
		while (Vector3.Distance(enemy.escapeWayPoints[enemy.nextEscapeWayPoint].position, enemy.transform.position) <= enemy.navMeshAgent.stoppingDistance)
			enemy.nextEscapeWayPoint = Random.Range (0, enemy.escapeWayPoints.Length - 1);
		enemy.navMeshAgent.destination = enemy.escapeWayPoints[enemy.nextEscapeWayPoint].position;
	}

	public void ToEscapeState() {
		// Can't transition to same state
	}

	public void ToIdleState() {
		enemy.animator.SetBool("Escape",false);
		enemy.animator.SetBool("Walk",false);

		enemy.actualState = enemyStateComplex.COMPLEX_STATE_IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToChaseState() {
		enemy.animator.SetBool("Escape",false);
		enemy.animator.SetBool("Walk",true);

		enemy.admirationStick.SetActive (true);
		enemy.admirationSphere.SetActive (true);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_CHASE;
		enemy.currentState = enemy.chaseState;
	}

	public void ToAttackState() {
		// Can't transition to this state
	}

	public void ToPatrolState() {
		// Can't transition to this state
	}
}