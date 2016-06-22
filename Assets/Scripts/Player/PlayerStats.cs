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

	public 	GameObject 			message;
	public 	Text 				textCountDown;

	public 	Texture 			oscarTexture;
	public 	Texture 			chefTexture;
	public 	playerState 		state;
	public 	GameObject 			oscar;
	public	GameObject 			_particles;
	public  bool 				suitUsed = false;
	private Animator			anim;
	private SkinnedMeshRenderer mesh;
	private float 				countDown = 0f;	
	private  bool 				beep = false;

	void Awake () {

		mesh = oscar.GetComponent<SkinnedMeshRenderer> ();
		state = playerState.PLAYER_STATE_MORTAL;
		mesh.material.SetTexture ("_MainTex", oscarTexture);
		anim = GetComponent<Animator> ();
		_particles.SetActive (false);
	}

	void Update(){
		if (countDown <= 0)
			return;
		textCountDown.text = string.Format("{00:00}:{1:00}",
			Mathf.Floor(countDown / 60),//minutes
			Mathf.Floor(countDown) % 60);//seconds
		countDown -= Time.deltaTime;

		if (beep) {
			AudioManager.Instance.PlayFX (fxClip.PICK_CLOK_KEY);
			beep = false;
			StartCoroutine (PlayClock ());
		}
	}
	public void activeBonus (float time) {
		StartCoroutine (bonusCooldown(time));
	}

	private IEnumerator bonusCooldown(float time) {
		suitUsed = true;
		state = playerState.PLAYER_STATE_BONUS_UNIFORM;
		transform.localScale = new Vector3 (1,1,1);
		mesh.material.SetTexture ("_MainTex", chefTexture);
		anim.SetTrigger ("Win");
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		yield return new WaitForSeconds (1.0f);
		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		_particles.SetActive (true);
		countDown = time;
		message.SetActive (true);
		beep = true;	
		yield return new WaitForSeconds (time);
		message.SetActive (false);
		transform.localScale = new Vector3 (0.6f,0.6f,0.6f);
		state = playerState.PLAYER_STATE_MORTAL;
		_particles.SetActive (false);
		mesh.material.SetTexture ("_MainTex", oscarTexture);
	}

	private IEnumerator PlayClock(){
		yield return new WaitForSeconds (1f);
		beep = true;
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