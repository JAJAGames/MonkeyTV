/* ARCHIVE_NAME.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * PRESS ESC TO GO TO MENU SECENE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * Update()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 22/04/2016	XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using InControl;
public class EscapeKey : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if (Input.GetKeyDown (KeyCode.Escape) || inputDevice.GetControl( InputControlType.Start ).WasPressed )
			gamestate.Instance.SetLevel (Enums.sceneLevel.MENU);
	}
}
