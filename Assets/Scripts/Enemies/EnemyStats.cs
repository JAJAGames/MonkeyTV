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

	public GameObject EnemySideLine;

	CapsuleCollider capsuleCollider;
	bool isDead;

	void Awake () {
		capsuleCollider = GetComponent <CapsuleCollider> ();
		currentHealth = startingHealth;
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
		yield return new WaitForSeconds(1.5f);
		Debug.Log (EnemySideLine.transform.position);
		transform.position = EnemySideLine.transform.position;
	}

	void Rebirth () {
		isDead = false;
		currentHealth = startingHealth;
		//Destroy (gameObject, 0f);
	}
}