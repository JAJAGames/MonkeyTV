using UnityEngine;
using System.Collections;

public class JailManager : MonoBehaviour {

	public GameObject jail1;
	public GameObject jail2;

	// Use this for initialization
	void Awake() {
		transform.position = jail1.transform.position;
	}
	
	// Update is called once per frame
	public void SetSecond() {
		transform.position = jail2.transform.position;
	}
}
