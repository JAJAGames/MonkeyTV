/* gamestate.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 *Is a singleton Clase witch knows the state of the game. 
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * gamestate ()
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 04/05/2016 	This code generates a gameObject called Game State and then attach itself to it;
 * 11/05/2016	WinLose Script aded to Game State GameObject
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using Enums;

#if UNITY_5_3_OR_NEWER	
using UnityEngine.SceneManagement;
#endif

public class gamestate : MonoBehaviour {
	
	// Declare properties
	private static gamestate instance;
	[SerializeField] private state stateOfScene;
	[SerializeField] private sceneLevel levelOfGame;

	// Creates an instance of gamestate as a gameobject if an instance does not exist
	public static gamestate Instance
	{
		get
		{
			if(instance == null)
			{
				GameObject go = new GameObject ("Game State");
				instance = go.AddComponent<gamestate> ();
				DontDestroyOnLoad(go);
			}	
			return instance;
		}
	}	

	// initialize singleton 
	private gamestate ()
	{
		
#if UNITY_5_3_OR_NEWER	

		levelOfGame = (sceneLevel) SceneManager.GetActiveScene ().buildIndex;

#endif
		if (levelOfGame > 0)
			Cursor.visible = false;
		else
			Cursor.visible = true;
		stateOfScene = state.STATE_INIT;  
	}

	// Sets the instance to null when the application quits
	public void OnApplicationQuit()
	{
		instance = null;
	}

	// Get and Set state of the game
	public void SetState(state newState)
	{
		stateOfScene = newState;
	}

	public state GetState()
	{
		return stateOfScene;
	}
	

	// Get and Set levels
	public sceneLevel GetLevel()
	{
		
#if UNITY_5_3_OR_NEWER	

		levelOfGame = (sceneLevel) SceneManager.GetActiveScene ().buildIndex;

#endif

		return levelOfGame;
	}

	public void SetLevel(sceneLevel newLevel)
	{
		// Set activeLevel to newLevel
		levelOfGame = newLevel;

#if UNITY_5_3_OR_NEWER	

		if (levelOfGame > 0)
			Cursor.visible = false;
		else
			Cursor.visible = true;

		SceneManager.LoadScene((int)levelOfGame);


#else

		Application.LoadLevel ((int)levelOfGame); //Deprecated

#endif

		SetState (state.STATE_INIT);
	}


}
