/* IGUBARLIFE.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * this script actuallizes the hud bar of player life.	
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake()
 * Update()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 19/04/2016	this script is attached to bar life gameObject in dynamic HUD. 
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGUBarlife : MonoBehaviour {

	PlayerStats playerStats;
	RectTransform rTransform;

	void Awake ()
	{
		 rTransform = gameObject.GetComponent<RectTransform> ();
		playerStats = GameObject.FindWithTag ("Player").GetComponent<PlayerStats> ();
	}

	void Update(){
		
		Vector3 scale = Vector3.one;

		//scale.x = (float)playerStats.currentHealth / playerStats.startingHealth;
		rTransform.localScale = scale;

	}
}
