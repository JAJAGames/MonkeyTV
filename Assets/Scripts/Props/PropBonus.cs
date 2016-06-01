using UnityEngine;
using System.Collections;
using Enums;

public class PropBonus : MonoBehaviour {
	[Header ("Bonus Type")]
	public enumBonus type;
	public float time;
	private PlayerStats player;
	private DishSelection clockDish;

	// Use this for initialization
	void Start () {
		player = GameObject.FindWithTag ("Player").GetComponent<PlayerStats> ();
		clockDish = GameObject.Find ("Clock").GetComponent<DishSelection> ();
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player")) {
			other.transform.position = transform.position;
			switch (type) {
			case enumBonus.BONUS_TIME:
				AudioManager.Instance.PlayFX (fxClip.PICK_CLOK_KEY);
				bonusTime ();
				break;
			case enumBonus.BONUS_UNIFORM:
				AudioManager.Instance.PlayFX (fxClip.PICK_SUIT);
				bonusUniform ();
				break;
			case enumBonus.BONUS_KEY:
				bonusKey ();
				break;
			}
			gameObject.transform.GetChild (0).gameObject.SetActive (true);
			gameObject.GetComponent<MeshRenderer> ().enabled = false;
			Invoke ("DisableSelf", 1f);
	
		}
	}

	private void DisableSelf(){
		gameObject.SetActive (false);
	}
	private void bonusTime() {
		//Add seconds to total time
		clockDish.addTimeToClock(time);
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