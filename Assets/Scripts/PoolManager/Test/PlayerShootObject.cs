using UnityEngine;
using System.Collections;

public class PlayerShootObject : PoolObject {
	[Range (1,20)]
	public float speed = 1.0f;

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(Vector3.forward * speed/5);
	}


	void OnTriggerStay (Collider other) {

		if (Vector3.Distance (gameObject.transform.position, other.gameObject.transform.position) < 1) {
			if (other.CompareTag ("Enemy"))
				other.gameObject.GetComponent<EnemyStats> ().TakeDamage (1);
			if (!other.CompareTag ("Player"))
				gameObject.SetActive (false);
		}
	}
}