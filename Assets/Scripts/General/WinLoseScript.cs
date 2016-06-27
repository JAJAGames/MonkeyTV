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

	private Transform _winPanel, _losePanel;
	// Use this for initialization
	void Awake () {

		_winPanel = transform.GetChild (0);
		_losePanel = transform.GetChild (1);
		Color color = GetComponent<Image> ().color;
		color.a = 0f;
		GetComponent<Image> ().color = color;
	}
	
	// Update is called once per frame
	void Update () {
		if (gamestate.Instance.GetState () == Enums.state.STATE_WIN)
			WinTheGame ();
		if (gamestate.Instance.GetState () == Enums.state.STATE_LOSE)
			LoseTheGame();
	}

	private void LoseTheGame () {
		Color color = GetComponent<Image> ().color;
		color.a = 0.5f;
		GetComponent<Image> ().color = color;
		_losePanel.gameObject.SetActive (true);
		AudioManager.Instance.PlayMusic(Enums.sceneLevel.LOSE);
		this.enabled = false;
	}

	private void WinTheGame() {
		Color color = GetComponent<Image> ().color;
		color.a = 0.5f;
		GetComponent<Image> ().color = color;
		_winPanel.gameObject.SetActive (true);
		AudioManager.Instance.PlayMusic(Enums.sceneLevel.WIN);
		this.enabled = false;
	}
}
