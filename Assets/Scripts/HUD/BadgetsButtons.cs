using UnityEngine;
using System.Collections;

public class BadgetsButtons : MonoBehaviour {


	public void ToMenu(){
		gamestate.Instance.SetLevel (Enums.sceneLevel.MENU);
	}

	public void ToNextLevel(){
		gamestate.Instance.SetLevel (Enums.sceneLevel.MENU); //only one level
	}

	public void ReloadLevel(){
		gamestate.Instance.SetLevel (gamestate.Instance.GetLevel()); //only one level
	}
}
