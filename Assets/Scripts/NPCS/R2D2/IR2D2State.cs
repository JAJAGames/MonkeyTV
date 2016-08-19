using UnityEngine;
using System.Collections;

public interface IR2D2State {

	void UpdateState();

	void OnTriggerEnter (Collider other);

	void ToNextPoint();
	void ToIdleState();
	void ToRepairLeveler();
}