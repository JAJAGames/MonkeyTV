/* HELPERMETHODS.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * EVENTS AND ACTIONS SCRIPT OF MENU BUTTONS  
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * GetChildren 	(TRANSFORM)
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 28/03/2016	GENERIC METHODS TO USE IN SCRIPTS
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HelperMethods
{
	public static GameObject[] GetChildren(Transform go)
	{
		List<GameObject> childrenList = new List<GameObject> ();

		foreach (Transform goChild in go) {
			childrenList.Add (goChild.gameObject);
		}
		return childrenList.ToArray();
	}
}