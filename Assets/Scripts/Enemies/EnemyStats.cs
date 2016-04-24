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
	public GameObject EnemySideLine;
	private SkinnedMeshRenderer meshRenderer;
	private Color originalColor;

	void Awake () {
		currentHealth = startingHealth;
		//meshRenderer = gameObject.GetComponent<MeshRenderer> ();
		meshRenderer = transform.GetChild(0).GetChild(0).gameObject.GetComponent<SkinnedMeshRenderer> ();
		originalColor = meshRenderer.material.color;
	}

	public void TakeDamage (int damage) {
		if(isDead)
			return;
		currentHealth -= damage;

		meshRenderer.material.color = Color.white;
		Invoke ("Recolor",0.2f);

		if(currentHealth <= 0) {
			GetComponent<StatePatternEnemy>().animator.SetBool("Dead",true);
			gameObject.SetActive (false);
		}
	}

	private void Recolor(){
		meshRenderer.material.color = originalColor;
	}
}