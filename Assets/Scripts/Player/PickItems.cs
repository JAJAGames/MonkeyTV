using UnityEngine;
using System.Collections;

public class PickItems : MonoBehaviour {

	public enum itemsListMasterChef {ITEM_TOMATO=0, ITEM_BANANA=1, NO_ITEM=2};

	public itemsListMasterChef actualItem;

	[Tooltip("Items")]
	public Transform Items;
	public GameObject[] allItems;

	// Use this for initialization
	void Start () {
		actualItem = itemsListMasterChef.NO_ITEM;

		allItems = HelperMethods.GetChildren (Items);
		for (int i = 0; i < allItems.Length; i++) 
			allItems[i].GetComponent<MeshRenderer> ().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Throw") && actualItem != itemsListMasterChef.NO_ITEM) {
			throwItem ();
		}
	}

	void change() {
	
	}

	public void throwItem() {
		//Throw item animation
		allItems[(int)actualItem].GetComponent<MeshRenderer> ().enabled = false;
		actualItem = itemsListMasterChef.NO_ITEM;
	}

	public bool haveItem() {
		return actualItem == itemsListMasterChef.NO_ITEM;
	}

	public void changeItem(itemsListMasterChef newItem) {
		actualItem = newItem;
		allItems[(int)newItem].GetComponent<MeshRenderer> ().enabled = true;
	}
}
