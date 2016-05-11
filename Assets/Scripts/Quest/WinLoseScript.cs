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

public class WinLoseScript : MonoBehaviour {

	Animator anim;
	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (gamestate.Instance.GetState () == Enums.state.STATE_WIN)
			StartCoroutine(WinTheGame (3));
		if (gamestate.Instance.GetState () == Enums.state.STATE_LOOSE)
			StartCoroutine (LoseTheGame(2));
	}

	private IEnumerator LoseTheGame (float t) {
		
		anim.SetTrigger("Dead");												//activate death animation 
		gamestate.Instance.SetState (Enums.state.STATE_INIT);
		yield return new WaitForSeconds (t);

		gamestate.Instance.SetLevel (0);
	}

	private IEnumerator WinTheGame(float t) {
		
		anim.SetTrigger("Win");
		gamestate.Instance.SetState (Enums.state.STATE_INIT);
		yield return new WaitForSeconds(t);
		gamestate.Instance.SetLevel (0);

	}
		

	//OnGUI deprecatet and efficience low --> We Must remove this in future
	void OnGUI() {
		
		if (gamestate.Instance.GetState () == Enums.state.STATE_WIN) {
			//Text properties
			int w = Screen.width, h = Screen.height;

			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = h / 10;
			style.fontStyle = FontStyle.Bold;
			style.normal.textColor = Color.green;

			Rect rect = new Rect (w / 2 - 50, h / 2 - 25, 100, 50);

			GUI.Label (rect, "YOU WIN!", style);
		}

		if (gamestate.Instance.GetState () == Enums.state.STATE_LOOSE) {
			//Text properties
			int w = Screen.width, h = Screen.height;

			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = h / 10;
			style.fontStyle = FontStyle.Bold;
			style.normal.textColor = Color.green;

			Rect rect = new Rect (w / 2 - 50, h / 2 - 25, 100, 50);

			GUI.Label (rect, "GAME OVER", style);
		}
	}
}
