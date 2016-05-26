using UnityEngine;
using System.Collections;

public class DisableOnFollow : MonoBehaviour {

	// Update is called once per frame
	void Update () {
		if (gamestate.Instance.GetState () == Enums.state.STATE_CAMERA_FOLLOW_PLAYER)
			gameObject.SetActive (false);
	}
}
