using UnityEngine;
using System.Collections;

public class BadgetsButtons : MonoBehaviour {


	public void ToMenu(){
		gamestate.Instance.SetLevel (Enums.sceneLevel.MENU);
	}

	public void ToNextLevel(){
		Debug.Log (gamestate.Instance.GetLevel () + 1);
		gamestate.Instance.SetLevel (gamestate.Instance.GetLevel() + 1); //only one level
	}

	public void ReloadLevel(){
		gamestate.Instance.SetLevel (gamestate.Instance.GetLevel()); //only one level
	}
}
