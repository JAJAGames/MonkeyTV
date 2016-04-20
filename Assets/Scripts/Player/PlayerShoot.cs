using UnityEngine;
using System.Collections;

/* PLAYERSHOOT.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * - TakeDamage
 * - Death
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * XX/XX/XXXX	XXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXXX
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

public class PlayerShoot : MonoBehaviour {
	[Header ("Bullet")]
	public GameObject prefab;
	private Transform render;

	void Awake(){
		render = transform.GetChild (0).transform;
	}

	void Update()
	{
		if (Input.GetButtonDown("Fire1") )
		PoolManager.instance.ReuseObject (prefab, render.position ,render.rotation);	
	}
}