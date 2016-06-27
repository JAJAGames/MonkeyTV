using UnityEngine;
using System.Collections;
using Enums;

public class ChaseStateSimple : IEnemyStateSimple {

	private readonly StatePatternEnemySimple enemy;
	private Vector3 destination;
	float timer;
	public PickItems playerItems;

	public bool notAttacking;

	public ChaseStateSimple(StatePatternEnemySimple statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_CHASE;
		enemy.nextWayPoint = 0;
		playerItems = enemy.player.GetComponent<PickItems> ();

		notAttacking = true;
	}

	public void UpdateState() {
		enemy.animator.SetBool ("Walk", true);

		Vector3 lookAt = enemy.navMeshAgent.destination;
		lookAt.y = enemy.transform.position.y;
		enemy.transform.LookAt (lookAt);

		enemy.navMeshAgent.destination = enemy.player.position;

		timer += Time.deltaTime;

		if (enemy.playerStats.uniformBonusActive ()) {
			ToEscapeState ();
		}

		if (enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathPartial || enemy.navMeshAgent.pathStatus == NavMeshPathStatus.PathInvalid)
			ToIdleState ();	
		
	}

	public void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player") && playerItems.haveItem() && !enemy.playerStats.godModeActive()) {
			ToAttackState ();

		}
	}

	public void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player") && playerItems.haveItem() && !enemy.playerStats.godModeActive()) {
			ToAttackState ();
		}
	}

	public void ToEscapeState() {
		enemy.admirationStick.SetActive (false);
		enemy.admirationSphere.SetActive (false);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_ESCAPE;
		enemy.currentState = enemy.escapeState;
	}

	public void ToIdleState () {
		enemy.admirationStick.SetActive (false);
		enemy.admirationSphere.SetActive (false);

		enemy.actualState = enemyStateSimple.SIMPLE_STATE_IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToChaseState () {
		// Can't transition to same state
	}

	public void ToAttackState() {
		enemy.admirationStick.SetActive (false);
		enemy.admirationSphere.SetActive (false);

		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_ATTACK;
		enemy.currentState = enemy.attackState;
	}
}