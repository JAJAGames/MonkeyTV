/* PLAYERSTATS.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * Manages the next player Stats:
 * - bonusUniform
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * Awake		()
 * bonusActive ()
 * bonusCooldown ()
 * godMode ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 *
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour {
	private bool bonusUniform;

	void Awake () {
		bonusUniform = false;
	}

	public void activeBonus (float time) {
		StartCoroutine (bonusCooldown(time));
	}

	private IEnumerator bonusCooldown(float time) {
		bonusUniform = true;
		yield return new WaitForSeconds (time);
		bonusUniform = false;
	}

	public void godMode() {
		bonusUniform = true;
	}

	public bool uniformBonusActive() {
		return bonusUniform;
	}
}