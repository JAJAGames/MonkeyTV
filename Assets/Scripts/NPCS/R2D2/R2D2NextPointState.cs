using UnityEngine;
using System.Collections;
using Enums;

public class R2D2NextPointState : IR2D2State {

	private readonly StatePatternR2D2 R2D2;


	public R2D2NextPointState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
	}

	public void UpdateState() {
		
	}

	public void OnTriggerEnter (Collider other) {
		
	}

	public void ToNextPoint(){
	}

	public void ToIdleState () {
		R2D2.actualState = R2D2State.IDLE_STATE;
	}

	public void ToRepairLeveler() {
		R2D2.actualState = R2D2State.REPAIR_LEVELER_STATE;
	}
		
}