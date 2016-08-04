using UnityEngine;
using System.Collections;

public class JailManagerGeneric : MonoBehaviour {

	public GameObject[] jail;
	private int actualJail;

	// Use this for initialization
	void Awake() {
		actualJail = 0;
		transform.position = jail[actualJail].transform.position;
	}

	public void SetSecond() {
		++actualJail;
		transform.position = jail[actualJail].transform.position;
	}
}