using UnityEngine;
using System.Collections;

public class JailManager : MonoBehaviour {

	public Vector3 firstPosition, secondPosition;
	// Use this for initialization
	void Awake() {
		transform.position = firstPosition;
	}
	
	// Update is called once per frame
	public void SetSecond() {
		transform.position = secondPosition;
	}
}
