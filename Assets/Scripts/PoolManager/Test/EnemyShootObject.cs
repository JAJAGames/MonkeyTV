using UnityEngine;
using System.Collections;

public class EnemyShootObject : PoolObject {

	public float speed = 20.0f;
	public GameObject player;
	private PlayerStats playerStats;

	void Awake() {
		playerStats = player.GetComponent<PlayerStats> ();
	}

	// Update is called once per frame
	void Update () {
		gameObject.transform.Translate(Vector3.forward * speed/5);
	}

	public override void OnObjectReuse (){
		transform.localScale = Vector3.one;
	}

	void OnTriggerEnter (Collider other) {
		if (other.CompareTag ("Player")) {
			playerStats.TakeDamage (1);
			gameObject.SetActive (false);
		}
		//else 
			//gameObject.SetActive (false);
	}
}