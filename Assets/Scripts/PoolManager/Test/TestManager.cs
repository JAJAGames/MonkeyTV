using UnityEngine;
using System.Collections;

public class TestManager : MonoBehaviour {

	public GameObject prefab;
	public GameObject startPoint;

	void Start(){
		PoolManager.instance.CreatePool (prefab, 10);
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			PoolManager.instance.ReuseObject (prefab, startPoint.transform.position, Quaternion.identity);			
		}

	}
}
