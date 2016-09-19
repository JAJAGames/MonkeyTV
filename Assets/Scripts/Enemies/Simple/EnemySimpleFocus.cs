using UnityEngine;
using System.Collections;

public class EnemySimpleFocus : MonoBehaviour {

	public MeshRenderer foco;
	private StatePatternEnemySimple _enemy;
	private bool traped;
	private StatePatternBoss _Boss;
	// Use this for initialization
	void Awake() {
		foco.enabled = false;
		traped = false;
		_enemy = GetComponent<StatePatternEnemySimple>();
		_Boss = GameObject.FindWithTag ("Boss").GetComponent<StatePatternBoss>();
	}
	
	void OnTriggerStay (Collider other){
		if (other.CompareTag("Trap")){
			_enemy.currentState = _enemy.idleState;
			_enemy.navMeshAgent.stoppingDistance = 0;
			_enemy.startPosition = other.transform.position;
			traped = true;
		}
	}

	public void Dead(){
		foco.enabled = true;
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
