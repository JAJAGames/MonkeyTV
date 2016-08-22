using UnityEngine;
using System.Collections;
using Enums;

public class AttackStateComplex : IEnemyStateComplex {

	private readonly StatePatternEnemyComplex enemy;
	public bool attackDone;

	public AttackStateComplex(StatePatternEnemyComplex statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_ATTACK;
		enemy.nextEscapeWayPoint = 0;
		enemy.nextPatrolWayPoint = 0;

		attackDone = false;
	}

	public void UpdateState() {
		if (!attackDone) {
			AttackPlayer ();
		}
	}

	public void OnTriggerEnter (Collider other) {
		
	}

	public void ToEscapeState() {
		enemy.admirationStick.SetActive (false);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_ESCAPE;
		enemy.currentState = enemy.escapeState;
	}

	public void ToIdleState() {
		enemy.animator.SetBool("Walk",false);

		enemy.actualState = enemyStateComplex.COMPLEX_STATE_IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToChaseState() {
		enemy.animator.SetBool("Escape",false);
		enemy.animator.SetBool("Walk",true);

		enemy.admirationStick.SetActive (true);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_CHASE;
		enemy.currentState = enemy.chaseState;
	}

	public void ToAttackState() {
		// Can't transition to same state
	}

	public void ToPatrolState() {
		enemy.admirationStick.SetActive (false);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateComplex.COMPLEX_STATE_PATROL;
		enemy.currentState = enemy.patrolState;
	}

	private void AttackPlayer(){
		attackDone = enemy.AttackPlayer (enemy.jail);
	}
}
