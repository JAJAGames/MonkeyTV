using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class MenuClick : MonoBehaviour {

	public GameObject panelMenu;
	public GameObject panelCredits;
	public GameObject panelOptions;

	public void LoadScene(int level)
	{
		SceneManager.LoadScene(level);
	}


	public void ToggleCredits()
	{
		panelMenu.SetActive (!panelMenu.activeSelf);
		panelCredits.SetActive (!panelCredits.activeSelf);
	}

	public void ToggleOptions()
	{
		panelMenu.SetActive (!panelMenu.activeSelf);
		panelOptions.SetActive (!panelOptions.activeSelf);
	}

	public void CloseGame()
	{
		Application.Quit ();
	}
}
