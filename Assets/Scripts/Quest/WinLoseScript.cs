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

	private Transform _badgetsBackground;
	private Color _color;
	private bool _active = false;
	// Use this for initialization
	void Awake () {

		_badgetsBackground = transform.GetChild (0);
		_color = Color.black;
		_color.a = 0;

		ShowBadgets (_active);
		_color.a = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (_active)
			return;
		if (gamestate.Instance.GetState () == Enums.state.STATE_WIN)
			WinTheGame ();
		if (gamestate.Instance.GetState () == Enums.state.STATE_LOSE)
			LoseTheGame();
	}

	private void LoseTheGame () {
		_active = true;
		ShowBadgets (_active);
		AudioManager.Instance.PlayMusic(Enums.sceneLevel.LOSE);
	}

	private void WinTheGame() {
		_active = true;
		ShowBadgets (_active);
		AudioManager.Instance.PlayMusic(Enums.sceneLevel.WIN);
	}

	private void ShowBadgets(bool b){
		GetComponent<Image> ().color = _color;
		_badgetsBackground.gameObject.SetActive (b);
	}
}
