using UnityEngine;
using System.Collections;
using Enums;

public class PropBonus : MonoBehaviour {
	[Header ("Bonus Type")]
	public enumBonus type;
	public float time;
	private PlayerStats player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerStats> ();	
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			switch (type) {
			case enumBonus.BONUS_TIME:
				bonusTime ();
				break;
			case enumBonus.BONUS_UNIFORM:
				bonusUniform ();
				break;
			case enumBonus.BONUS_KEY:
				bonusKey ();
				break;
			}
			gameObject.SetActive (false);
		}
	}


	private void bonusTime() {
		// Add time to total time
	}

	private void bonusUniform() {
		//Activate bonus for time seconds
		if (!player.godModeActive())
			player.activeBonus(time);
	}

	private void bonusKey(){
		// Add a key used to open a cage
	}
}