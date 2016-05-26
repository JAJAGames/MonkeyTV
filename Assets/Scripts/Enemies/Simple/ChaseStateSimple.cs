using UnityEngine;
using System.Collections;
using Enums;

public class ChaseStateSimple : IEnemyStateSimple {

	private readonly StatePatternEnemySimple enemy;
	private Vector3 destination;
	float timer;
	public PickItems playerItems;
	public Transform jail;

	public ChaseStateSimple(StatePatternEnemySimple statePatternEnemy) {
		enemy = statePatternEnemy;
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_CHASE;
		enemy.nextWayPoint = 0;
		playerItems = enemy.player.GetComponent<PickItems> ();
		jail = GameObject.FindWithTag ("Jail").transform;
	}

	public void UpdateState() {
		enemy.animator.SetBool("Walk",true);

		Vector3 lookAt = enemy.navMeshAgent.destination;
		lookAt.y = enemy.transform.position.y;
		enemy.transform.LookAt (lookAt);

		enemy.navMeshAgent.destination = enemy.player.position;

		timer += Time.deltaTime;

		if (enemy.playerStats.uniformBonusActive()) {
			ToEscapeState ();
		}
	}

	public void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player") && playerItems.haveItem()) {
			enemy.player.transform.position = jail.position;
		}
	}

	public void OnTriggerStay (Collider other) {
		if (other.gameObject.CompareTag ("Player") && playerItems.haveItem()) {
			enemy.player.transform.position = jail.position;
		}
	}

	public void ToEscapeState() {
		enemy.navMeshAgent.Resume ();
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_ESCAPE;
		enemy.currentState = enemy.escapeState;
	}

	public void ToIdleState () {
		enemy.actualState = enemyStateSimple.SIMPLE_STATE_IDLE;
		enemy.currentState = enemy.idleState;
	}

	public void ToChaseState () {
		// Can't transition to same state
	}

	public void Attack() {
		//timer = 0f;
		//enemy.animator.SetTrigger("Attack");
		//PoolManager.instance.ReuseObject (enemy.prefab, enemy.transform.position + new Vector3(0.0f, 1.0f, 0.0f), enemy.transform.rotation);
	}
}