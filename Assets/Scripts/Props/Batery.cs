﻿/* BATERY.CS
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
	private Transform scalable;
	public OpenDoor door;

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


		if (!other.CompareTag("PlayerShoot"))
			return;

		other.gameObject.SetActive (false);					//feedback of impact
		big = true;

		emSmoke.enabled = true;								//enable smoke particles

		
		if (anim.isPlaying) {								//if animation is playing the battery have inmunity
			return;
		}
		anim.Play ();										//play once time, so Invoke StopAnim when it's over.
		Invoke ("StopAnim",anim.clip.length);  		


		if (smoke.startSize > 1.5f) {						//If particles size is upper than 1.5 --> kill the battery ingame
			gameObject.SetActive (false);
			door.Open ();
		}

		if (emSmoke.rate.constantMax < 5) {					//If the number of particles per second is 5 change the size
			var rate = emSmoke.rate;
			rate.constantMax += 1;
			emSmoke.rate = rate;
		} else
			smoke.startSize += 0.1f;
		
	}

	private void StopAnim(){
		anim.Stop();
	}

}
