using UnityEngine;
using System.Collections;

public class PropPickItem : MonoBehaviour {

	public PickItems.itemsListMasterChef itemType;
	public MeshRenderer meshRenderer;
	public SphereCollider sphereCollider;

	public PickItems player;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
		sphereCollider = GetComponent<SphereCollider> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PickItems> ();
	}
	
	// Update is called once per frame
	void Update () {
	
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
