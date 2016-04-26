using UnityEngine;
using System.Collections;

/* ENEMYSTATS.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * Manages the next Enemy Stats:
 * - Health
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * TakeDamage ()
 * Recolor ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 19/04/2016	Added feedback changing color on TakeDamage
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class EnemyStats : MonoBehaviour {

	public int startingHealth = 4;            
	public int currentHealth;
	public bool isDead;
	private SkinnedMeshRenderer skin;

	void Awake () {
		currentHealth = startingHealth;
		skin = GetComponentInChildren<SkinnedMeshRenderer> ();
	}

	public void TakeDamage (int damage) {
		if(isDead)
			return;
		currentHealth -= damage;

		skin.material.color = Color.red;
		Invoke ("Recolor",0.2f);

		if(currentHealth <= 0) {
			GetComponent<StatePatternEnemy>().animator.SetBool("Dead",true);
			gameObject.SetActive (false);
		}
	}

	private void Recolor(){
		skin.material.color = Color.white;
	}
		
}