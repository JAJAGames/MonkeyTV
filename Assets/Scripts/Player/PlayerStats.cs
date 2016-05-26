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
using Enums;

public class PlayerStats : MonoBehaviour {
	//private bool bonusUniform;
	public playerState state;

	void Awake () {
		state = playerState.PLAYER_STATE_MORTAL;
	}

	public void activeBonus (float time) {
		StartCoroutine (bonusCooldown(time));
	}

	private IEnumerator bonusCooldown(float time) {
		state = playerState.PLAYER_STATE_BONUS_UNIFORM;
		yield return new WaitForSeconds (time);
		state = playerState.PLAYER_STATE_MORTAL;
	}

	public void godMode() {
		switch (state) {
		case playerState.PLAYER_STATE_GOD:
			state = playerState.PLAYER_STATE_MORTAL;
			break;
		default:
			state = playerState.PLAYER_STATE_GOD;
			break;
		}
	}

	public bool uniformBonusActive() {
		return state == playerState.PLAYER_STATE_BONUS_UNIFORM;
	}

	public bool godModeActive() {
		return state == playerState.PLAYER_STATE_GOD;
	}
}