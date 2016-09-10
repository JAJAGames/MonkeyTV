using UnityEngine;
using System.Collections;
using Enums;
public class PanelControl : MonoBehaviour {

	public StatePatternBoss boss;
	private Transform button;
	// Use this for initialization
	void Awake () {
		boss = GameObject.Find ("Boss").GetComponent<StatePatternBoss> ();
		button = transform.FindChild ("Canvas");
	}
	
	// Update is called once per frame
	void Update () {
		if (boss.actualState == BossState.PUNCH_STATE) 
			button.gameObject.SetActive (true);
		else
			button.gameObject.SetActive (false);
	
	}
	
}
