using UnityEngine;
using System.Collections;

public interface IBossState {

	void UpdateState();

	void OnTriggerEnter (Collider other);

	void ToIdleState();
	void ToMoveState();
	void ToPunchState();
	void ToDamagedState();
}