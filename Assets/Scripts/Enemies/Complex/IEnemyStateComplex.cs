using UnityEngine;
using System.Collections;

public interface IEnemyStateComplex{
	void UpdateState();

	void OnTriggerEnter (Collider other);

	void ToAttackState();
	void ToChaseState();
	void ToEscapeState();
	void ToIdleState();
	void ToPatrolState();
}