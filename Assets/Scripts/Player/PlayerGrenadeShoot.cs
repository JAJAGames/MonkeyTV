using UnityEngine;
using System.Collections;

public class PlayerGrenadeShoot : MonoBehaviour {


	[Header ("Bullet")]
	public GameObject prefab;

	[Header ("Grenade in HUD")]
	public IGUGrenade iguGrenade;

	private bool active; 
	private Transform render;

	// Use this for initialization
	void Awake () {
		active = false;
		render = transform.GetChild (0).transform;
	}

	void Update()
	{
		if (Input.GetKey (KeyCode.E) && active) {
			PoolManager.instance.ReuseObject (prefab, render.position + (Vector3.up + render.forward ) * 2, render.rotation);
			SetActive (false);
		}
	}
	public void SetActive(bool bolean){
		active = bolean;
		iguGrenade.SetActive (active);
	}
}
