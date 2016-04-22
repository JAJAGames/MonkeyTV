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
 * 19/04/2016	Var GOD added to enable or disable mode GOD
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
#if UNITY_5_3
using UnityEngine.SceneManagement;
#endif

public class PlayerStats : MonoBehaviour {

	public int startingHealth = 4;            
	public int currentHealth;
	public bool isDead;
	public GameObject panelFX;
	public bool GOD;
	private const int MENUID = 0;

	void Awake () {
		currentHealth = startingHealth;
		GOD = false;
	}

	public void TakeDamage (int damage) {
		if(isDead || GOD)
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
		yield return new WaitForSeconds(2.0f);
#if UNITY_5_3
		SceneManager.LoadScene(MENUID);
#endif
#if UNITY_5_2
		Application.LoadLevel(MENUID);
#endif
	}

	private void StopFX(){
		panelFX.SetActive (false);
	}

	void OnGUI() {
		if (isDead) {
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