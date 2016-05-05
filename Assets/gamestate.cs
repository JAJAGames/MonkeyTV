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
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

#if UNITY_5_3_OR_NEWER	
using UnityEngine.SceneManagement;
#endif

public enum state {INIT_SCENE, RUN_ANIMATION, RUN_INTRO, NEW_SEARCH, SEARCH_OBJECTS, DELIVER_OBJECTS, COMPLETED, WIN, LOOSE};
public enum sceneLevel {MENU, LEVEL_1, LEVEL_2, LEVEL_BOSS};

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
				instance = new GameObject("Game State").AddComponent<gamestate>();
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

		stateOfScene = state.INIT_SCENE;  
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

		SceneManager.LoadScene((int)levelOfGame);

#else

		Application.LoadLevel ((int)levelOfGame); //Deprecated

#endif

		SetState (state.INIT_SCENE);
	}


}
