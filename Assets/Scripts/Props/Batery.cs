/* BATERY.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * This script allows the behaviour of Batery prop
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 20/04/2016	GENERIC METHODS TO USE IN SCRIPTS
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class Batery : MonoBehaviour {

	private Animation anim;

	private ParticleSystem smoke;
	ParticleSystem.EmissionModule emSmoke;
	private bool big;
	public Transform scalable;

	void Awake () {
		anim = gameObject.GetComponent<Animation>();
		smoke = gameObject.GetComponent<ParticleSystem> ();
		emSmoke = smoke.emission;
		emSmoke.enabled = false;
		big = false;
		scalable = transform.GetChild (0).transform;
	}

	void Update (){
		if (big) {
			scalable.localScale +=  Vector3.one * Time.deltaTime * 10;
		}
		if (scalable.localScale.x > 1.5) {
			scalable.localScale = Vector3.one;
			big = false;
		}
	
	}

	void OnTriggerEnter(Collider other) {

		other.gameObject.SetActive (false);
		big = true;
		if (anim.isPlaying)
			return;
		
		if (smoke.startSize > 1.5f)
			gameObject.SetActive (false);

		if (other.CompareTag ("PlayerShoot")) {

			anim.Play ();
			Invoke ("StopAnim",anim.clip.length);  		//play once time
			emSmoke.enabled = true;

			if (emSmoke.rate.constantMax < 5) {
				var rate = emSmoke.rate;
				rate.constantMax += 1;
				emSmoke.rate = rate;
			} else
				smoke.startSize += 0.1f;
		}
	}

	private void StopAnim(){
		anim.Stop();
	}

}
