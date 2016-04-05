using UnityEngine;
using System.Collections;

/* PLAYERSHOOT.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
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

	public int damagePerShot = 1;
	public float timeBetweenBullets = 0.15f;
	public float range = 200f;
	float timer;

	LineRenderer gunLine;
	Ray shootRay;
	RaycastHit shootHit;
	float effectsDisplayTime = 1f; 
	//float effectsDisplayTime = 0.2f; 
	int enemyMask;
	int explosiveMask;

	public GameObject render;


	void Awake () {
		enemyMask = LayerMask.GetMask("Enemy");
		explosiveMask = LayerMask.GetMask("Explosive");
		gunLine = GetComponent<LineRenderer> ();

	}

	void Update () {
		timer += Time.deltaTime;

		if (Input.GetButtonDown("Fire1") && timer >= timeBetweenBullets) {
			Shoot ();
		}

		if(timer >= timeBetweenBullets * effectsDisplayTime) {
			DisableEffects ();
		}
	}

	void Shoot() {
		timer = 0f;
		gunLine.enabled = true;

		gunLine.SetPosition (0, transform.position);


		shootRay.origin = transform.position;
		shootRay.direction = render.transform.forward;

		if(Physics.Raycast (shootRay, out shootHit, range, enemyMask)) {
			EnemyStats enemyHealth = shootHit.collider.GetComponent <EnemyStats> ();

			if(enemyHealth != null) {
				enemyHealth.TakeDamage(damagePerShot);
			}

			gunLine.SetPosition (1, shootHit.point);
		} else {
			gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
		}
	}

	public void DisableEffects () {
		gunLine.enabled = false;
	}
}