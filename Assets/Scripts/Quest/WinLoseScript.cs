/* WinLoseScript.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 *This code manages the win and lose events in game. It Shows the text and animations related to each state
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * Update ()
 * LoseTheGame (float)
 * WinTheGame (float)
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 11/05/2016	WinLose Script aded to Player GameObject
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WinLoseScript : MonoBehaviour {

	public Text text;
	Animator anim;

	private AudioSource _lose;

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		text.gameObject.SetActive (false);
		_lose = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gamestate.Instance.GetState () == Enums.state.STATE_WIN)
			StartCoroutine(WinTheGame (3));
		if (gamestate.Instance.GetState () == Enums.state.STATE_LOSE)
			StartCoroutine (LoseTheGame(2));
	}

	private IEnumerator LoseTheGame (float t) {
		_lose.Play ();
		anim.SetTrigger("Dead");												//activate death animation 
		text.text = "GAME OVER";
		text.gameObject.SetActive(true);
		gamestate.Instance.SetState (Enums.state.STATE_INIT);
		yield return new WaitForSeconds (t);

		gamestate.Instance.SetLevel (0);
	}

	private IEnumerator WinTheGame(float t) {
		
		anim.SetTrigger("Win");
		text.text = "YOU WIN!";
		text.gameObject.SetActive(true);
		gamestate.Instance.SetState (Enums.state.STATE_INIT);
		yield return new WaitForSeconds(t);
		gamestate.Instance.SetLevel (0);

	}
		
}
