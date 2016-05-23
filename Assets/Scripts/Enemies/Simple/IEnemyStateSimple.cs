using UnityEngine;
using System.Collections;

public interface IEnemyStateSimple {
	void UpdateState();

	void OnTriggerEnter (Collider other);

	void ToEscapeState();
	void ToChaseState();
	void ToIdleState();
}