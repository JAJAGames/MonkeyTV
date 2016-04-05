using UnityEngine;
using System.Collections;

/* ENEMYSTATS.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * Manages the next Enemy Stats:
 * - Health
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * TakeDamage	(int)
 * Death		()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * XX/XX/XXXX	XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class EnemyStats : MonoBehaviour {

	public int startingHealth = 4;            
	public int currentHealth;
	public bool isDead;
	public GameObject EnemySideLine;

	public GameObject player;

	private StatePatternEnemy enemy;


	void Awake () {
		currentHealth = startingHealth;
		enemy = GetComponent<StatePatternEnemy> ();
	}

	void Update() {

		if (isDead) StartCoroutine (Rebirth());
	}

	public void TakeDamage (int damage) {
		if(isDead)
			return;

		currentHealth -= damage;

		if(currentHealth <= 0) {
			StartCoroutine (Death());
		}
	}

	private IEnumerator Death () {
		isDead = true;

		yield return new WaitForSeconds(1.0f);

		ToWaitState();
		transform.position = EnemySideLine.transform.position;
		enabled = false;
	}

	private IEnumerator Rebirth() {
		

		yield return new WaitForSeconds(4.0f);
		isDead = false;

		currentHealth = startingHealth;

		transform.position = player.transform.position;

		enabled = true;
		enemy.navMeshAgent.enabled = true;
		enemy.eState = enemyState.PATROL;
		enemy.currentState = enemy.patrolState;

	}

	public void ToWaitState() {
		enemy.navMeshAgent.enabled = false;
		enemy.eState = enemyState.WAIT;
		enemy.currentState = enemy.waitState;
	}
}