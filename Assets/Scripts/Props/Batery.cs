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
	public Transform sphere;

	void Awake () {
		anim = gameObject.GetComponent<Animation>();
		smoke = gameObject.GetComponent<ParticleSystem> ();
		emSmoke = smoke.emission;
		emSmoke.enabled = false;
		sphere = transform.GetChild (4).transform;
		sphere.gameObject.SetActive (false);
	}

	void Update (){
		if (sphere.gameObject.activeSelf) {
			sphere.transform.localScale +=  Vector3.one * Time.deltaTime * 10;
			if (sphere.transform.localScale.x > 2)
				sphere.gameObject.SetActive (false);
		}
	}

	void OnTriggerEnter(Collider other) {

		other.gameObject.SetActive (false);
		sphere.transform.localScale = Vector3.one;
		sphere.gameObject.SetActive (true);

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
