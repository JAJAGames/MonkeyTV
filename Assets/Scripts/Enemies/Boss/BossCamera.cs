using UnityEngine;
using System.Collections;

public class BossCamera : MonoBehaviour {

	public float  shakeAmount = 0.7f;
	private float shakeTime;
	private Vector3 position;
	private bool shake;
	private Transform boss;
	void Awake () {
		boss = GameObject.Find ("Boss").transform;
		shake = false;
		shakeTime = 0f;
	}

	void Update(){
		transform.LookAt (boss.position);
	}

	public void ShakeCamera () {
		transform.position += Random.insideUnitSphere * shakeAmount;
		shakeTime -= Time.deltaTime;

		if (shakeTime <= 0)
			ResetCamera ();
	}

	private void ResetCamera(){
		shakeTime = 0.0f;
		transform.position = transform.parent.position - position;
	}

	public void SetShakeTime (float time)
	{
		position = transform.parent.position - transform.position;
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
