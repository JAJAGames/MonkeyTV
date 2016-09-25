using UnityEngine;
using System.Collections;

public interface IBossLiteState {

	void UpdateState();

	void ToIdleState();
	void ToMoveState();
}
