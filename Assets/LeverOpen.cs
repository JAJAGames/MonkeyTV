﻿using UnityEngine;
using System.Collections;


public class LeverOpen : MonoBehaviour {
	
	public Transform lever; 
	public Transform cameraStaticPosition;
	private CameraFollow cam;
	private Transform cameraStaticGorilla;
	private bool opened = false;
	void Awake(){
		cam = Camera.main.GetComponent<CameraFollow> ();
	}

	void OnTriggerStay (Collider other){
		if (opened)
			return;
		if (Input.GetKeyDown (KeyCode.E) && other.CompareTag ("Player")) {
			lever.Rotate (100, 0, 0);
			Invoke ("ShowOpening", 1f);
			gamestate.Instance.SetState(Enums.state.STATE_STATIC_CAMERA);
			cameraStaticGorilla = cam.target;
			cam.target = cameraStaticPosition;
			opened = true;
		}
	}

	void ShowOpening()
	{
		gameObject.GetComponent<OpenDoor> ().enabled = true;
		gameObject.GetComponent<OpenDoor> ().SetCloset (false);
		Invoke ("DisableSelf", 1f);

	}
	void DisableSelf(){
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		cam.target = cameraStaticGorilla;
		gameObject.SetActive (false);
	}
}