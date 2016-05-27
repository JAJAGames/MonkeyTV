using UnityEngine;
using System.Collections;

public class ParticleCanvasPLayOnce : MonoBehaviour {

	void Update () {
		Invoke ("StopPlay", 2f);
	}
	
	void StopPlay(){
		gameObject.SetActive (false);
	}
}
