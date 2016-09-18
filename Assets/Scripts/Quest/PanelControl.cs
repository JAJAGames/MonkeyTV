using UnityEngine;
using System.Collections;
using Enums;
using InControl;
public class PanelControl : MonoBehaviour {

	public StatePatternBoss boss;
	private Transform button;
	// Use this for initialization
	void Awake () {
		boss = GameObject.FindWithTag("Boss").GetComponent<StatePatternBoss> ();
		button = transform.FindChild ("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		if (boss.actualState == BossState.PUNCH_STATE) 
			button.gameObject.SetActive (true);
		else
			button.gameObject.SetActive (false);
	
	}

	public void OnTriggerStay (Collider other) {
		if (boss.actualState != BossState.PUNCH_STATE)
			return;
		var inputDevice = InputManager.ActiveDevice;
		if (other.gameObject.CompareTag ("Player")) {
			if ( Input.GetButton("Pick") || inputDevice.Action3 ){ //inputDevice.Action3 or pickNutton
				boss.batteries--;
				gameObject.SetActive (false);
			}
		}
	}
}
