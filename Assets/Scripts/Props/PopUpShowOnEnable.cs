using UnityEngine;
using System.Collections;

public class PopUpShowOnEnable : MonoBehaviour {

	public PopUp popup; 

	void OnEnable() {
		if (gamestate.Instance.GetState() == Enums.state.STATE_CAMERA_FOLLOW_PLAYER)
			popup.ShowPopUpOnce(18);
	}
}
