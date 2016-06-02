using UnityEngine;
using System.Collections;
using Enums;

public class PropPickItem : MonoBehaviour {

	public itemsListMC itemType;
	private MeshRenderer meshRenderer;
	private CapsuleCollider capsuleCollider;

	public PickItems player;
	private Color _color;
	private Material _material;
	private Animator anim;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PickItems> ();
		anim = GameObject.FindWithTag ("Player").GetComponent<Animator>();

		_material = meshRenderer.sharedMaterial;

		//can be set in the inspector
		_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

		_color = meshRenderer.material.GetColor ("_EmissionColor");
		GetComponent<BouncingItems> ().enabled = true;
	}
		
	void Update (){
		transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90f);

	}

	void OnTriggerStay (Collider other){

		if (other.CompareTag ("Player") ) {
			if (Input.GetButtonDown ("Pick") && !player.haveItem()){
				anim.SetBool("Pick_Object",true);
				player.changeItem(itemType);
				StartCoroutine (Respawn ());
			}
		}
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")) {
			meshRenderer.material.SetColor ("_EmissionColor", Color.gray);
			GetComponent<BouncingItems> ().enabled = false;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.CompareTag ("Player")) {
			GetComponent<BouncingItems> ().enabled = true;
			meshRenderer.material.SetColor ("_EmissionColor", _color);
		}
	}
	
	private IEnumerator Respawn() {
		meshRenderer.material.SetColor ("_EmissionColor",_color);
		meshRenderer.enabled = false;
		capsuleCollider.enabled = false;
		yield return new WaitForSeconds (5.0f);

		//Respawn animation

		meshRenderer.enabled = true;
		capsuleCollider.enabled = true;
	}
}