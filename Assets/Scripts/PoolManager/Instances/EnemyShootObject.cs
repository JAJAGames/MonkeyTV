using UnityEngine;
using System.Collections;

public class EnemyShootObject : PoolObject {

	public float speed = 20.0f;
	private PlayerStats playerStats;

	void Awake() {
		playerStats = GameObject.FindWithTag("Player").GetComponent<PlayerStats> ();
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(Vector3.forward * speed/5);
	}


	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Player")) {
			playerStats.TakeDamage (1);
			gameObject.SetActive (false);
		}

	}
}