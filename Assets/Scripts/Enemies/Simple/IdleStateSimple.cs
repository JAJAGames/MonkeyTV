using UnityEngine;
using System.Collections;

public class IdleStateSimple : IEnemyStateSimple {

	private readonly StatePatternEnemySimple enemy;

	public IdleStateSimple(StatePatternEnemySimple statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.state = enemyStateSimple.SIMPLE_STATE_IDLE;
	}

	public void UpdateState() {
		enemy.animator.SetBool("Walk",false);

		if (enemy.transform.position != enemy.startPosition) {
			enemy.navMeshAgent.destination = enemy.startPosition; 
			enemy.navMeshAgent.Resume ();
		} else
			enemy.navMeshAgent.Stop ();

		if (enemy.playerStats.uniformBonusActive()) {
			ToEscapeState ();
		}
	}

	public void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			enemy.chaseTarget = other.transform;
			ToChaseState();
		}
	}

	public void ToEscapeState() {
		enemy.navMeshAgent.Resume ();
		enemy.state = enemyStateSimple.SIMPLE_STATE_ESCAPE;
		enemy.currentState = enemy.escapeState;
	}

	public void ToIdleState () {
		// Can't transition to same state
	}

	public void ToChaseState () {
		enemy.navMeshAgent.Resume ();
		enemy.state = enemyStateSimple.SIMPLE_STATE_CHASE;
		enemy.currentState = enemy.chaseState;
	}
}