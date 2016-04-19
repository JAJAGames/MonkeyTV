/* PLAYERSTATS.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * Manages the next player Stats:
 * - Health
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * Awake		()
 * TakeDamage	(int)
 * Death		()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 19/04/2016	Var GOD added to enable or disable mode GOD
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int startingHealth = 4;            
	public int currentHealth;
	public bool isDead;
	public GameObject panelFX;
	public bool GOD;

	void Awake () {
		currentHealth = startingHealth;
		GOD = false;
	}

	public void TakeDamage (int damage) {
		if(isDead || GOD)
			return;

		panelFX.SetActive (true);
		Invoke ("StopFX", 0.1f);

		currentHealth -= damage;

		if(currentHealth <= 0) {
			StartCoroutine (Death());
		}
	}

	private IEnumerator Death () {
		isDead = true;
		yield return new WaitForSeconds(1.0f);
	}

	private void StopFX(){
		panelFX.SetActive (false);
	}
}