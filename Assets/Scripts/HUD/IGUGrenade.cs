/* IGUGRENADE.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * Show enabled and disabled grenade in IGU.
 * Only uses public function Setactive with a boolean to change color
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake()
 * SetActive(bool)
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 23/04/2016	this script is attached to grenade gameObject in dynamic HUD. 
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGUGrenade : MonoBehaviour {

	private Image image;

	// Use this for initialization
	void Awake () {
		image = gameObject.GetComponent<Image> ();
		SetActive (false);
	}
	

	public void SetActive(bool bolean){

		Color color = Color.green;

		if (bolean)
			color.a = 1f;
		else
			color.a = 0.25f;

		image.color = color;
	}


}
