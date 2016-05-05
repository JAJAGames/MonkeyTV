using UnityEngine;
using System.Collections;
using Enums;

public class PropPickItem : MonoBehaviour {

	public itemsListMasterChef itemType;
	public MeshRenderer meshRenderer;
	public SphereCollider sphereCollider;

	public PickItems player;
	private Color _color;
	private Material _material;
	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
		sphereCollider = GetComponent<SphereCollider> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PickItems> ();

		_material = meshRenderer.sharedMaterial;

		//can be set in the inspector
		_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

		_color = meshRenderer.material.GetColor ("_EmissionColor");
	}

	// Update is called once per frame
	void Update () {
		if (Vector3.Distance (player.transform.position, transform.position) < 2) {
			meshRenderer.material.SetColor ("_EmissionColor", Color.gray);
		}
		else
			meshRenderer.material.SetColor ("_EmissionColor",_color);
	}

	void OnTriggerStay (Collider other){

		if (other.CompareTag ("Player") && Input.GetButtonDown ("Pick")) {
			if (player.haveItem()) {
				player.changeItem(itemType);
				StartCoroutine (Respawn ());
			}
		}

	}

	private IEnumerator Respawn() {
		meshRenderer.enabled = false;
		sphereCollider.enabled = false;
		yield return new WaitForSeconds (5.0f);

		//Respawn animation

		meshRenderer.enabled = true;
		sphereCollider.enabled = true;
	}
}
