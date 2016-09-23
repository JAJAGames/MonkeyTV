using UnityEngine;
using System.Collections;
using Enums;

public class Circularmovement : MonoBehaviour {
	private StatePatternBoss _boss;
	private Transform _player;
	public FocusTarget target;
	private Quaternion initRotation;
	void Awake(){
		_boss = GameObject.FindWithTag ("Boss").GetComponent<StatePatternBoss> ();
		_player = GameObject.FindWithTag ("Player").transform;
		target = FocusTarget.NONE;
		initRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update () {
		if (_boss.actualState == Enums.BossState.PUNCH_STATE)
			return;
		
		if (_boss.phase == BossPhase.COMBAT_PHASE_2 && target == FocusTarget.PLAYER) {
			transform.position = Vector3.MoveTowards (transform.position, _player.position ,Time.deltaTime * 5);
			transform.rotation = Quaternion.Slerp (transform.rotation, initRotation, Time.deltaTime * 5);
		}
		else{
			transform.Translate (0f,-.05f,0f);
			transform.Rotate(0f,0f,Time.deltaTime * 15); // turn a little
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Enemy") && target == FocusTarget.NONE) {
			target = FocusTarget.MONKEY;
			other.GetComponent<EnemySimpleFocus> ().trapped = true;;
		}

		if (_boss.phase == BossPhase.COMBAT_PHASE_2 && other.CompareTag ("Player"))
			target = FocusTarget.PLAYER;
	}
}
