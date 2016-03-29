/* MENUCLICK.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
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
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuClick : MonoBehaviour {

	// 3 MENU PANELS WITCH WE CAN TOGGLE 
	public GameObject panelMenu;
	public GameObject panelCredits;
	public GameObject panelOptions;

	//CHANGE TO NEW SCENE. IN INSPECTOR WE CAN SET THE BUILD INDEX OF THE NEW SCENE.
	public void LoadScene(int level)
	{
		SceneManager.LoadScene(level);
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
		Application.Quit ();
	}
}
