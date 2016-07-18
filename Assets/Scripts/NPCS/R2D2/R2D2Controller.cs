using UnityEngine;
using System.Collections;

public class R2D2Controller : MonoBehaviour {

	private bool _quest = false;
	// Update is called once per frame

	void Update () {
		if (_quest)
			return;
		
		if (gamestate.Instance.GetState () == Enums.state.STATE_INIT)
			gamestate.Instance.SetState(Enums.state.STATE_STATIC_CAMERA);

		Invoke("QuesToFalse", 3);
	}

	void QuesToFalse(){
		_quest = false;
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
	}

}
