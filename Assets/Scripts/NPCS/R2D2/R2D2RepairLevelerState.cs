using UnityEngine;
using System.Collections;
using Enums;

public class R2D2RepairLevelerState : IR2D2State {


	private readonly StatePatternR2D2 R2D2;


	public R2D2RepairLevelerState(StatePatternR2D2 statePatternR2D2) {
		R2D2 = statePatternR2D2;
	}

	public void UpdateState() {

	}

	public void OnTriggerEnter (Collider other) {

	}

	public void ToNextPoint(){
		R2D2.actualState = R2D2State.NEXT_POINT_STATE;
	}

	public void ToIdleState () {
		R2D2.actualState = R2D2State.IDLE_STATE;
	}

	public void ToRepairLeveler() {

	}

}