using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class PickItems : MonoBehaviour {

	public itemsList actualItem;

	[Tooltip("Items")]
	public Transform Items;
	public GameObject[] allItems;
	private BoxCollider boxCollider;

	private Animator anim;

	// Use this for initialization
	void Start () {
		actualItem = itemsList.NO_ITEM;

		allItems = HelperMethods.GetChildren (Items);
		for (int i = 0; i < allItems.Length; i++) 
			allItems[i].SetActive(false);

		boxCollider = Items.GetComponent<BoxCollider> ();

		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		var inputDevice = InputManager.ActiveDevice;
		if ((Input.GetButton("Throw") || inputDevice.Action2 ) && haveItem())  //inputDevice.Action2 or ThrowButton
			throwItem ();
	}

	public void throwItem() {
		//Throw item animation
		if (actualItem == itemsList.NO_ITEM)
			return;
		anim.SetBool("Pick_Object",false);
		allItems[(int)actualItem].SetActive (false);
		boxCollider.enabled = false;
		actualItem = itemsList.NO_ITEM;
	}

	public bool haveItem() {
		return !(actualItem == itemsList.NO_ITEM);
	}

	public void changeItem(itemsList newItem) {
		actualItem = newItem;
		AudioManager.Instance.PlayFX (fxClip.FX_PICK_FOOD);
		allItems[(int)newItem].SetActive(true);
		boxCollider.enabled = true;
	}
}