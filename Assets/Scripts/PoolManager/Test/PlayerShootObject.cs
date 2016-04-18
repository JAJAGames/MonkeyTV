using UnityEngine;
using System.Collections;

public class PlayerShootObject : PoolObject {
	[Range (1,3)]
	public float speed = 1.0f;

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(Vector3.forward * speed/5);
	}


	void OnTriggerStay (Collider other) {

		if (other.CompareTag ("Enemy")) {
			if (Vector3.Distance (gameObject.transform.position, other.gameObject.transform.position) < 1) {
				other.gameObject.GetComponent<EnemyStats> ().TakeDamage (1);
				gameObject.SetActive (false);
			}
		}
	}

}