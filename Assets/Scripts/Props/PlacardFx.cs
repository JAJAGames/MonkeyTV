using UnityEngine;
using System.Collections;

public class PlacardFx : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		if (other.CompareTag("Player")||other.CompareTag("Enemy"))
			AudioManager.Instance.PlayFX (Enums.fxClip.PLACARD_COLLISION);
	}
}
