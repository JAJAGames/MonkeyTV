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
using UnityEngine.UI;
using System.Collections;
using Enums;


public class PlayerStats : MonoBehaviour {

	public Texture oscarTexture;
	public Texture chefTexture;
	public playerState state;
	public GameObject oscar;

	private SkinnedMeshRenderer mesh;
	void Awake () {

		mesh = oscar.GetComponent<SkinnedMeshRenderer> ();
		state = playerState.PLAYER_STATE_MORTAL;
		mesh.material.SetTexture ("_MainTex", oscarTexture);
	}

	public void activeBonus (float time) {
		StartCoroutine (bonusCooldown(time));
	}

	private IEnumerator bonusCooldown(float time) {
		state = playerState.PLAYER_STATE_BONUS_UNIFORM;
		mesh.material.SetTexture ("_MainTex", chefTexture);
		yield return new WaitForSeconds (time);
		state = playerState.PLAYER_STATE_MORTAL;
		mesh.material.SetTexture ("_MainTex", oscarTexture);
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