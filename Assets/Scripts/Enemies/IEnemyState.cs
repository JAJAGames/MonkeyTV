using UnityEngine;
using System.Collections;

public interface IEnemyState 
{
	void UpdateState();

	void OnTriggerEnter (Collider other);

	void ToWaitState ();
	void ToIdleState ();
	void ToPatrolState ();
	void ToAlertState ();
	void ToChaseState ();
}