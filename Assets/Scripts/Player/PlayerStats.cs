﻿/* PLAYERSTATS.CS
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
 * 22/04/2016	added compiler directives
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class PlayerStats : MonoBehaviour {

	public int startingHealth = 4;            
	public int currentHealth;
	public bool isDead;
	public GameObject panelFX;
	public bool GOD;
	private const int MENUID = 0;
	private Animator anim;
	private PlayerMovement pMove;

	void Awake () {
		anim = transform.GetChild(0).GetComponent<Animator>();
		currentHealth = startingHealth;
		GOD = false;
		isDead = false;
		pMove = gameObject.GetComponent<PlayerMovement> ();
	}

	public void TakeDamage (int damage) {
		if(isDead || GOD)
			return;
		pMove.enabled = false;
		anim.SetTrigger ("Damaged");
		Invoke ("EnableMove", ShowCurrentClipLength());

		panelFX.SetActive (true);
		Invoke ("StopFX", 0.1f);

		currentHealth -= damage;

		if(currentHealth <= 0) {
			StartCoroutine (Death());
		}
	}

	private IEnumerator Death () {
		isDead = true;
		anim.SetBool ("Dead", true);
		Invoke ("StopAnimator", anim.GetCurrentAnimatorStateInfo(0).length);
		yield return new WaitForSeconds (2.0f);
#if UNITY_5_3_OR_NEWER
		SceneManager.LoadScene(MENUID);
#else
		Application.LoadLevel(MENUID);
#endif
	}

	private void EnableMove(){
		pMove.enabled = true;
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

	void StopAnimator()
	{
		anim.Stop ();
	}

	float ShowCurrentClipLength()
	{
		return ( anim.GetCurrentAnimatorStateInfo(0).length);
	}

}