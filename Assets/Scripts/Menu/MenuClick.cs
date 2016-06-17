/* MENUCLICK.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * EVENTS AND ACTIONS SCRIPT OF MENU BUTTONS  
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * LoadScene 	(int)
 * ToggleCredits()
 * ToggleOptions()
 * CloseGame	()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 23/03/2016	CODE BASE MATCHED TO MENU BUTTONS OF THE MENU SCENE
 * 22/04/2016	ADDED COMPILER DIRECTIVES
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using Enums;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class MenuClick : MonoBehaviour {

	// 3 MENU PANELS WITCH WE CAN TOGGLE 
	public GameObject panelMenu;
	public GameObject panelCredits;
	public GameObject panelOptions;
	public GameObject panelControls;
	public GameObject logo;

	void Awake(){
		sceneLevel level =  gamestate.Instance.GetLevel ();
		AudioManager.Instance.SetFxVolume (0.5f);
		AudioManager.Instance.SetMusicVolume (0.5f);
		AudioManager.Instance.PlayMusic (level);
		logo = GameObject.Find ("LogoTV");
	}

	//CHANGE TO NEW SCENE. IN INSPECTOR WE CAN SET THE BUILD INDEX OF THE NEW SCENE.
	public void LoadScene(int level)
	{
		AudioManager.Instance.PlayFX (fxClip.BUTTON_PRESSED);
		logo.SetActive (false);
		panelMenu.SetActive (false);
		panelControls.SetActive (true);
	}

	//TOGGLE PANEL MENU WITH PANEL CREDITS AND BACK
	public void ToggleCredits()
	{
		panelMenu.SetActive (!panelMenu.activeSelf);
		panelCredits.SetActive (!panelCredits.activeSelf);
	}

	//TOGGLE PANEL MENU WITH PANEL OPTIONS AND BACK
	public void ToggleOptions()
	{
		panelMenu.SetActive (!panelMenu.activeSelf);
		panelOptions.SetActive (!panelOptions.activeSelf);
	}

	//CLOSE GAME
	public void CloseGame()
	{
		AudioManager.Instance.PlayFX (fxClip.BUTTON_PRESSED);
		Application.Quit ();
	}
}
