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
 * XX/XX/XXXX	XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public int startingHealth = 4;            
	public int currentHealth;
	public bool isDead;
	public GameObject panelFX;

	void Awake () {
		currentHealth = startingHealth;
	}

	public void TakeDamage (int damage) {
		if(isDead)
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