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
		
	void Update (){
		transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90f);

	}

	void OnTriggerStay (Collider other){

		if (other.CompareTag ("Player") ) {
			if (Input.GetButtonDown ("Pick") && player.haveItem()){
				player.changeItem(itemType);
				StartCoroutine (Respawn ());
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player"))
			meshRenderer.material.SetColor ("_EmissionColor", Color.gray);
	}

	void OnTriggerExit (Collider other){
		if (other.CompareTag ("Player"))
			meshRenderer.material.SetColor ("_EmissionColor",_color);
	}
	
	private IEnumerator Respawn() {
		meshRenderer.material.SetColor ("_EmissionColor",_color);
		meshRenderer.enabled = false;
		sphereCollider.enabled = false;
		yield return new WaitForSeconds (5.0f);

		//Respawn animation

		meshRenderer.enabled = true;
		sphereCollider.enabled = true;
	}
}
