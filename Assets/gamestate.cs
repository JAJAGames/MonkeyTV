﻿/* gamestate.CS
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

public class gamestate : MonoBehaviour {
	
	// Declare properties
	private static gamestate instance;
	[SerializeField] private Enums.state stateOfScene;
	[SerializeField] private Enums.sceneLevel levelOfGame;

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

		levelOfGame = (Enums.sceneLevel) SceneManager.GetActiveScene ().buildIndex;

#endif

		stateOfScene = Enums.state.INIT_SCENE;  
	}

	// Sets the instance to null when the application quits
	public void OnApplicationQuit()
	{
		instance = null;
	}

	// Get and Set state of the game
	public void SetState(Enums.state newState)
	{
		stateOfScene = newState;
	}

	public Enums.state GetState()
	{
		return stateOfScene;
	}
	

	// Get and Set levels
	public Enums.sceneLevel GetLevel()
	{
		
#if UNITY_5_3_OR_NEWER	

		levelOfGame = (Enums.sceneLevel) SceneManager.GetActiveScene ().buildIndex;

#endif

		return levelOfGame;
	}

	public void SetLevel(Enums.sceneLevel newLevel)
	{
		// Set activeLevel to newLevel
		levelOfGame = newLevel;

#if UNITY_5_3_OR_NEWER	

		SceneManager.LoadScene((int)levelOfGame);

#else

		Application.LoadLevel ((int)levelOfGame); //Deprecated

#endif

		SetState (Enums.state.INIT_SCENE);
	}


}
