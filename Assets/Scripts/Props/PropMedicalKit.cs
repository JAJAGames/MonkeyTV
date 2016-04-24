using UnityEngine;
using System.Collections;

public class PropMedicalKit : MonoBehaviour {

	public int lifeHealed;
	private PlayerStats player;

	void Awake () {
		player = GameObject.FindWithTag("Player").GetComponent<PlayerStats> ();
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			if (player.currentHealth < player.startingHealth) {
				player.Heal (lifeHealed);
				gameObject.SetActive (false);
			}
		}
	}
}
