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

	public 	Texture 			oscarTexture;
	public 	Texture 			chefTexture;
	public 	playerState 		state;
	public 	GameObject 			oscar;
	public	GameObject 			_particles;
	private Animator			anim;
	private SkinnedMeshRenderer mesh;
	void Awake () {

		mesh = oscar.GetComponent<SkinnedMeshRenderer> ();
		state = playerState.PLAYER_STATE_MORTAL;
		mesh.material.SetTexture ("_MainTex", oscarTexture);
		anim = GetComponent<Animator> ();
		_particles.SetActive (false);
	}

	public void activeBonus (float time) {
		StartCoroutine (bonusCooldown(time));
	}

	private IEnumerator bonusCooldown(float time) {
		state = playerState.PLAYER_STATE_BONUS_UNIFORM;
		transform.localScale = new Vector3 (1,1,1);
		mesh.material.SetTexture ("_MainTex", chefTexture);
		anim.SetTrigger ("Win");
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		yield return new WaitForSeconds (1.0f);
		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		_particles.SetActive (true);
		yield return new WaitForSeconds (time);
		transform.localScale = new Vector3 (0.6f,0.6f,0.6f);
		state = playerState.PLAYER_STATE_MORTAL;
		_particles.SetActive (false);
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