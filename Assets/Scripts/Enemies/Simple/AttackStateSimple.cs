using UnityEngine;
using System.Collections;
using Enums;

public class AttackStateSimple : IEnemyStateSimple {

	private readonly StatePatternEnemySimple enemy;
	public bool attackDone;

	public AttackStateSimple(StatePatternEnemySimple statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_ATTACK;
		enemy.nextWayPoint = 0;

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
		enemy.admirationSphere.SetActive (false);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_ESCAPE;
		enemy.currentState = enemy.escapeState;
	}

	public void ToIdleState() {
		enemy.animator.SetBool("Walk",false);

		enemy.actualState = enemyStateSimple.SIMPLE_STATE_IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToChaseState() {
		enemy.animator.SetBool("Escape",false);
		enemy.animator.SetBool("Walk",true);

		enemy.admirationStick.SetActive (true);
		enemy.admirationSphere.SetActive (true);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_CHASE;
		enemy.currentState = enemy.chaseState;
	}

	public void ToAttackState() {
		// Can't transition to same state
	}

	private void AttackPlayer(){
		attackDone = enemy.AttackPlayer (enemy.jail);
	}
}
