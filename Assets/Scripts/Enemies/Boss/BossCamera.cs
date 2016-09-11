using UnityEngine;
using System.Collections;

public class BossCamera : MonoBehaviour {

	public float  shakeAmount = 0.7f;
	private float shakeTime;
	private Vector3 position;
	private bool shake;

	void Awake () {
		position = transform.position;
		shake = false;
		shakeTime = 0f;
	}
	
	public void ShakeCamera () {
		transform.position += Random.insideUnitSphere * shakeAmount;
		shakeTime -= Time.deltaTime;

		if (shakeTime <= 0)
			ResetCamera ();
	}

	private void ResetCamera(){
		shakeTime = 0.0f;
		transform.position = position;
	}

	public void SetShakeTime (float time)
	{
		shakeTime = time;
		shake = true;
	}

	public bool IsShaking()
	{
		return shake;
	}

	public void SetShake(bool value){
		shake = value;
	}
}
