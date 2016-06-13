using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class PickItems : MonoBehaviour {

	public itemsListMC actualItem;

	[Tooltip("Items")]
	public Transform Items;
	public GameObject[] allItems;
	private BoxCollider boxCollider;

	private Animator anim;

	// Use this for initialization
	void Start () {
		actualItem = itemsListMC.NO_ITEM_MC;

		allItems = HelperMethods.GetChildren (Items);
		for (int i = 0; i < allItems.Length; i++) 
			allItems[i].SetActive(false);

		boxCollider = Items.GetComponent<BoxCollider> ();

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if (Input.GetButton("Throw") && actualItem != itemsListMC.NO_ITEM_MC)  //inputDevice.Action2 or ThrowButton
			throwItem ();
	}

	public void throwItem() {
		//Throw item animation
		anim.SetBool("Pick_Object",false);
		allItems[(int)actualItem].SetActive (false);
		boxCollider.enabled = false;
		actualItem = itemsListMC.NO_ITEM_MC;
	}

	public bool haveItem() {
		return !(actualItem == itemsListMC.NO_ITEM_MC);
	}

	public void changeItem(itemsListMC newItem) {
		actualItem = newItem;
		AudioManager.Instance.PlayFX (fxClip.PICK_FOOD);
		allItems[(int)newItem].SetActive(true);
		boxCollider.enabled = true;
	}
}