﻿using UnityEngine;
using System.Collections;
using InControl;

public class LeverOpen : MonoBehaviour {
	
	public Transform lever; 
	public Transform cameraStaticPosition;
	private CameraFollow cam;
	private Transform cameraStaticGorilla;

	public Transform door;

	//angle rotated, incremet of rotation and door initial state closed
	public float stopAngle = 90;
	private float rotation = 0;
	private bool closed = true;

	void Awake(){
		cam = Camera.main.GetComponent<CameraFollow> ();
	}

	//update the angle position and increment till the door is opened
	void Update ()
	{
		if (closed || stopAngle < 0)
			return;

		rotation += Time.deltaTime * 20.0f ;
		if (door)
			door.Rotate (rotation * Vector3.down, Space.World);

		stopAngle -= rotation;
	}

	void OnTriggerStay (Collider other){
		if (!closed)
			return;
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action3 && other.CompareTag ("Player")) {
			closed = false;
			AudioManager.Instance.PlayFX (Enums.fxClip.UNLOCK_LEVER);
			lever.Rotate (100, 0, 0);
			Invoke ("ShowOpening", 1f);
			gamestate.Instance.SetState(Enums.state.STATE_STATIC_CAMERA);
			cameraStaticGorilla = cam.target;
			cam.target = cameraStaticPosition;

		}
	}

	void ShowOpening()
	{
		AudioManager.Instance.PlayFX (Enums.fxClip.OPEN_DOOR);
		Invoke ("DisableSelf", 1f);

	}

	void DisableSelf(){
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		cam.target = cameraStaticGorilla;
		gameObject.SetActive (false);
	}
}