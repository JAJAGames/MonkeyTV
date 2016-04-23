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
 * 22/04/2016	added compiler directives and animation events
 * 23/04/2016	disable shoots when player take damage
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
	private PlayerShoot pShoot;

	void Awake () {
		anim = transform.GetChild(0).GetComponent<Animator>();
		currentHealth = startingHealth;
		GOD = false;
		isDead = false;
		pMove = gameObject.GetComponent<PlayerMovement> ();
		pShoot = gameObject.GetComponent<PlayerShoot> ();
	}

	public void TakeDamage (int damage) {
		if(isDead || GOD)
			return;

		pMove.enabled = false;																//disable movement and shoots when take damage
		pShoot.enabled = false;

		anim.SetTrigger ("Damaged");														//activate damaged animation 					
		Invoke ("EnableMoveAndShoots", anim.GetCurrentAnimatorStateInfo(0).length);			//enable movement and shoots when animation is over

		panelFX.SetActive (true);
		Invoke ("StopFX", 0.1f);

		currentHealth -= damage;

		if(currentHealth <= 0) {
			StartCoroutine (Death());
		}
	}

	private IEnumerator Death () {
		isDead = true;																//is dead
										
		anim.SetBool ("Dead", true);												//activate death animation 
		Invoke ("StopAnimator", anim.GetCurrentAnimatorStateInfo(0).length);		//stop animator just at end of death

		yield return new WaitForSeconds (2.0f);
#if UNITY_5_3_OR_NEWER
		SceneManager.LoadScene(MENUID);
#else
		Application.LoadLevel(MENUID);
#endif
	}

	private void EnableMoveAndShoots(){
		if (isDead)
			return;
		pMove.enabled = true;
		pShoot.enabled = true;
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

}