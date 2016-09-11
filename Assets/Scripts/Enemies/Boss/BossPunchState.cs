using UnityEngine;
using System.Collections;
using Enums;

public class BossPunchState : IBossState {

	private readonly StatePatternBoss Boss;
	private Vector3 cameraPosition;
	private BossCamera camera;
	private float clock;
	public BossPunchState(StatePatternBoss statePatternBoss) {
		Boss = statePatternBoss;
		cameraPosition = Boss.BossCameraPosition.position;
		camera = Boss.BossCameraPosition.GetComponent<BossCamera> ();
	}

	public void UpdateState() {
	

		if (camera.IsShaking ()) {
			
			camera.ShakeCamera ();

			if (clock <= Constants.BOSS_RECOVER_TIME - 1f)
				gamestate.Instance.SetState (state.STATE_CAMERA_FOLLOW_PLAYER);

			clock -= Time.deltaTime;

		} else {
			clock = Constants.BOSS_RECOVER_TIME;
			if (Boss.anim.GetCurrentAnimatorStateInfo (0).IsName ("Boss Recover"))
				camera.SetShakeTime (1);

		}

		if (clock < 0)
			ToIdleState ();
	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToIdleState () {
		camera.SetShake (false);
		Boss.anim.SetBool ("Walk", false);
		Boss.currentState = Boss.idleState;
		Boss.actualState = BossState.IDLE_SATE;
	}

	public void ToMoveState(){
		camera.SetShake (false);
		Boss.anim.SetBool ("Walk", true);
		Boss.actualState = BossState.MOVE_STATE;
		Boss.currentState = Boss.moveState;
	}

	public void ToPunchState() {}

	public void ToDamagedState() {
		camera.SetShake (false);
		Boss.anim.SetTrigger ("Damaged");
		Boss.actualState = BossState.DAMAGED_STATE;
		Boss.currentState = Boss.damagedState;
	}

}