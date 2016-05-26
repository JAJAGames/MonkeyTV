using UnityEngine;
using System.Collections;
using Enums;

public class PickItems : MonoBehaviour {

	public itemsListMC actualItem;

	[Tooltip("Items")]
	public Transform Items;
	public GameObject[] allItems;
	private BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
		actualItem = itemsListMC.NO_ITEM_MC;

		allItems = HelperMethods.GetChildren (Items);
		for (int i = 0; i < allItems.Length; i++) 
			allItems[i].SetActive(false);

		boxCollider = Items.GetComponent<BoxCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Throw") && actualItem != itemsListMC.NO_ITEM_MC) {
			throwItem ();
		}
	}

	public void throwItem() {
		//Throw item animation
		allItems[(int)actualItem].SetActive (false);
		boxCollider.enabled = false;
		actualItem = itemsListMC.NO_ITEM_MC;
	}

	public bool haveItem() {
		return !(actualItem == itemsListMC.NO_ITEM_MC);
	}

	public void changeItem(itemsListMC newItem) {
		actualItem = newItem;
		allItems[(int)newItem].SetActive(true);
		boxCollider.enabled = true;
	}
}