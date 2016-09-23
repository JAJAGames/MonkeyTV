using UnityEngine;
using System.Collections;

public class EnemySimpleFocus : MonoBehaviour {

	public MeshRenderer foco;
	private StatePatternEnemySimple _enemy;
	private StatePatternBoss _Boss;
	private bool _isDead;
	public bool trapped;
	// Use this for initialization
	void Awake() {
		foco.enabled = false;
		_isDead = false;
		_enemy = GetComponent<StatePatternEnemySimple>();
		_Boss = GameObject.FindWithTag ("Boss").GetComponent<StatePatternBoss>();
		trapped = false;
	}
	
	void OnTriggerStay (Collider other){
		if (other.CompareTag("Trap") && trapped){
			_enemy.currentState = _enemy.idleState;
			_enemy.navMeshAgent.stoppingDistance = 0;
			_enemy.startPosition = other.transform.position;
		}
	}

	public void Dead(){
		if (_isDead)
			return;
		foco.enabled = true;
		_isDead = true;
		_Boss.remainingMonkeys--;
		StartCoroutine (ShowDead());
	}

	IEnumerator ShowDead(){
		yield return new WaitForSeconds (1f);
		_enemy.animator.SetTrigger ("Dead");
		_enemy.enabled = false;
		yield return new WaitForSeconds (5f);
		this.gameObject.SetActive (false);
	}
}
