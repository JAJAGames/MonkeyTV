using UnityEngine;
using System.Collections;

public class ShowHideArrow : MonoBehaviour {

	PickItems player;
	MeshRenderer mesh;
	Transform child;
	// Use this for initialization
	void Awake () {
		player = GameObject.Find ("Player").GetComponent<PickItems>();
		mesh = GetComponent<MeshRenderer> ();
		child = transform.GetChild (0);
	}
	
	// Update is called once per frame
	void Update () {
		mesh.enabled = (player.actualItem != Enums.itemsListMC.NO_ITEM);
		child.gameObject.SetActive (player.actualItem != Enums.itemsListMC.NO_ITEM);
	}
}
