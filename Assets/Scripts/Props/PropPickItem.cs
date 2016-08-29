using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class PropPickItem : MonoBehaviour {

	public itemsList itemType;
	private MeshRenderer meshRenderer;
	private CapsuleCollider capsuleCollider;
	private SphereCollider sphereCollider;

	public PickItems player;
	private Color _color;
	private Material _material;
	private Animator anim;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
		capsuleCollider = GetComponent<CapsuleCollider> ();
		sphereCollider = GetComponent<SphereCollider> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PickItems> ();
		anim = GameObject.FindWithTag ("Player").GetComponent<Animator>();

		_material = meshRenderer.sharedMaterial;

		//can be set in the inspector
		_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

		_color = meshRenderer.material.GetColor ("_OutlineColor");
		GetComponent<BouncingItems> ().enabled = true;
	}
		
	void Update (){
		transform.RotateAround(transform.position, Vector3.up, Time.deltaTime * 90f);

	}

	void OnTriggerStay (Collider other){
		var inputDevice = InputManager.ActiveDevice;
		if (other.CompareTag ("Player") ) {
			if ( (Input.GetButton("Pick") || inputDevice.Action3 ) && !player.haveItem())	{ //inputDevice.Action3 or pickNutton
				anim.SetBool("Pick_Object",true);
				player.changeItem(itemType);
				StartCoroutine (Respawn ());
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player") && !player.haveItem()) {
			meshRenderer.material.SetColor ("_OutlineColor", Color.magenta);
			GetComponent<BouncingItems> ().enabled = false;
		}
	}

	void OnTriggerExit (Collider other){
		if (other.CompareTag ("Player")) {
			GetComponent<BouncingItems> ().enabled = true;
			meshRenderer.material.SetColor ("_OutlineColor", _color);
		}
	}
	
	private IEnumerator Respawn() {
		
		meshRenderer.material.SetColor ("_OutlineColor",_color);
		meshRenderer.enabled = false;
		capsuleCollider.enabled = false;
		sphereCollider.enabled = false;

		if (GetComponent<OnEnterEnable> () != null)
			GetComponent<OnEnterEnable> ().DisableOther ();
		yield return new WaitForSeconds (5.0f);

		//Respawn animation
		ShowItem();
	}

	public void ShowItem(){
		GetComponent<BouncingItems> ().enabled = true;
		GetComponent<SphereCollider>().enabled = true;
		GetComponent<MeshRenderer>().enabled = true;
		GetComponent<CapsuleCollider>().enabled = true;
	}
}