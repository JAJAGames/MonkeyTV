using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/* SPAWNMANAGER.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION:
 * Manages enemy spawners and add control to the level zones
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * DEVELOPING
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * XX/XX/XXXX	XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class SpawnManager : MonoBehaviour {

	public List<GameObject> activeEnemies;
	public List<GameObject> enemySidelines;

	[Header("Spawn Areas")]
	public Transform NivelMCS01;
	private int[] enemyTypeA;
	private int[] enemyTypeB;

	// Use this for initialization
	void Start () {
		activeEnemies = new List<GameObject> ();
		enemySidelines = new List<GameObject> ();



		AddAllEnemiesToSideLines ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void AddAllEnemiesToSideLines(){
		/*GameObject[]go = GameObject.FindGameObjectsWithTag("Enemy");    
		foreach(GameObject enemy in go)
			AddTarget(enemy.transform);
	*/
//		enemySidelines = GameObject.FindGameObjectsWithTag ("Enemy");
	}
/*
	public void AddTarget(Transform enemy){
     targets.Add(enemy);    
         
     }
*/
}
