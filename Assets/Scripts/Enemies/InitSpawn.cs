using UnityEngine;
using System.Collections;

public class InitSpawn : MonoBehaviour {

	public GameObject[] enemies;
	public Vector3[] startPositions;
	public GameObject offMeshLinks;
	private float stopDistance;
	void Start(){
		
		GameObject[] go = new GameObject[transform.childCount];
		startPositions = new Vector3[transform.childCount];
		go = HelperMethods.GetChildren (transform);
		for (int i = 0; i < enemies.Length; i++) {
			startPositions [i] = go [i].transform.position;
			enemies [i].SetActive (false);
		}
		
	}

	public void Spawning(){
		
		stopDistance = enemies [0].GetComponent<NavMeshAgent> ().stoppingDistance;
		for (int i = 0; i < enemies.Length; i++) {
			enemies [i].SetActive (true);
			enemies [i].GetComponent<StatePatternEnemySimple> ().startPosition = startPositions[i];
			enemies [i].GetComponent<NavMeshAgent> ().stoppingDistance = 0;
			enemies [i].GetComponent<StatePatternEnemySimple> ().actualState = Enums.enemyStateSimple.SIMPLE_STATE_IDLE;
		}

		Invoke ("DisableWaypoints", 7f);
	}

	void DisableWaypoints(){
		offMeshLinks.SetActive (false);
		for (int i = 0; i < enemies.Length; i++) {
			enemies [i].GetComponent<NavMeshAgent> ().stoppingDistance = stopDistance;
		}
	}
}
