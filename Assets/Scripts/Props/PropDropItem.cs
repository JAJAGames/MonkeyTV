﻿using UnityEngine;
using System.Collections;
using Enums;

public class PropDropItem : MonoBehaviour {


	public MeshRenderer meshRenderer;
	public PickItems player;

	private Color _color;
	private Material _material;
	private DishSelection dishSelection;

	public OpenDoor keyDoor;
	public int[] courses;
	public DishList.FoodMenu[] menu;
	public Dish dish;

	//PROVES
	int currentDish;
	bool firsTime = true;

	// Use this for initialization
	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PickItems> ();

		_material = meshRenderer.sharedMaterial;

		//can be set in the inspector
		_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

		_color = meshRenderer.material.GetColor ("_EmissionColor");

		dishSelection = GameObject.Find ("Clock").GetComponent<DishSelection> ();
		courses = dishSelection.GetCourses ();

		menu = new DishList.FoodMenu[3];
		menu [0] = new DishList.FoodMenu(DishList.menu[courses[0]]);
		menu [1] = new DishList.FoodMenu(DishList.menu[courses[1]]);
		menu [2] = new DishList.FoodMenu(DishList.menu[courses[2]]);

		dish = (Dish)GameObject.FindObjectOfType(typeof(Dish));
	}

	// Update is called once per frame
	void Update () {
		//Cooking animation
	}


	private void OnTriggerStay (Collider other){
		if (other.CompareTag ("Player") ) {
			if (Input.GetButtonDown ("Pick") && player.haveItem()){
				checkItem ();
			}
		}
	}

	private void OnTriggerEnter (Collider other){	//Si el player no porta ingredient no s'ha d'il.luminar
		if (other.CompareTag ("Player") && player.haveItem()) {
			meshRenderer.material.SetColor ("_EmissionColor", Color.red);
		}
	}

	private void OnTriggerExit (Collider other){
		if (other.CompareTag ("Player")) {
			meshRenderer.material.SetColor ("_EmissionColor", _color);
		}
	}

	private void checkItem () {
		bool foundIngredient = false;

		if (firsTime) {
			currentDish = dishSelection.currentCourse;
			firsTime = false;
		}

		for (int i = 0; i < DishList.ITEMSCOUNT; ++i) {
			if (!foundIngredient && menu[currentDish].ingredients[i] == player.actualItem) {
				menu[dishSelection.currentCourse].ingredients [i] = itemsListMC.NO_ITEM;
				player.throwItem ();
				foundIngredient = true;
				--menu[dishSelection.currentCourse].itemsLeft;
			} 
		} 

		if (!foundIngredient) {																//fer algun feedback de que no es l'ingredient correcte
			Debug.Log ("NO MATCH");
		} else { 																//ja no te ingredient deixa d'il.luminar
			meshRenderer.material.SetColor ("_EmissionColor", _color);
			Debug.Log("GOT IT!");

			if (currentDish == 0 && keyDoor.isClosed ())
				keyDoor.Open ();

			if (menu [dishSelection.currentCourse].itemsLeft == 0) {
				if (dishSelection.currentCourse < 2) {
					Debug.Log("DISH COMPLETE");
					++currentDish;
					dish.ShowNewDish ();
				} else {
					gamestate.Instance.SetState (Enums.state.STATE_WIN);
				}
			}
		}
	}
}

