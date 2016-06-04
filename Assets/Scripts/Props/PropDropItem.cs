﻿using UnityEngine;
using System.Collections;
using Enums;

public class PropDropItem : MonoBehaviour {


	public MeshRenderer meshRenderer;
	public PickItems player;
	private Animator anim;

	private Color _color;
	private Material _material;
	private DishSelection dishSelection;

	public Transform cameraStaticPosition;
	private CameraFollow cam;
	private Transform cameraStaticDoor;

	public OpenDoor keyDoor;
	public int[] courses;
	public DishList.FoodMenu[] menu;
	public Dish dish;
	public IGUfromWorld[] IGUchek = new IGUfromWorld[3];
	//PROVES
	int currentDish;
	bool firsTime = true;

	// Use this for initialization
	void Awake (){
		cam = Camera.main.GetComponent<CameraFollow> ();
		meshRenderer = GetComponent<MeshRenderer> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PickItems> ();
		anim = GameObject.FindWithTag ("Player").GetComponent<Animator>();
		dishSelection = GameObject.Find ("Clock").GetComponent<DishSelection> ();
	}

	void Start () {
		
		_material = meshRenderer.sharedMaterial;

		//can be set in the inspector
		_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

		_color = meshRenderer.material.GetColor ("_EmissionColor");


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
				menu[dishSelection.currentCourse].ingredients [i] = itemsListMC.NO_ITEM_MC;
				IGUchek [i].gameObject.SetActive (true);
				IGUchek [i].StartAnimation ();
				player.throwItem ();
				foundIngredient = true;
				--menu[dishSelection.currentCourse].itemsLeft;
			} 
		} 

		if (!foundIngredient) {													//fer algun feedback de que no es l'ingredient correcte
			Debug.Log ("NO MATCH");
		} else { 																//ja no te ingredient deixa d'il.luminar
			anim.SetBool("Pick_Object",false);
			meshRenderer.material.SetColor ("_EmissionColor", _color);

				

			if (menu [dishSelection.currentCourse].itemsLeft == 0) {
				dishSelection.clock = Mathf.Infinity;
				if (dishSelection.currentCourse == 0 ) {
					StartCoroutine (OpenDoor (1f));
				}else
					if (dishSelection.currentCourse < 2) {
						Debug.Log("DISH COMPLETE");
						NewCourse ();
					}else 
						if (dishSelection.currentCourse == 2)
							gamestate.Instance.SetState (Enums.state.STATE_WIN);
			}
			AudioManager.Instance.PlayFX(Enums.fxClip.GUI_PICK_BONUS);
		}
	}

	IEnumerator OpenDoor(float waitTime){
		gamestate.Instance.SetState (state.STATE_STATIC_CAMERA);
		cameraStaticDoor = cam.target;
		cam.target = cameraStaticPosition;
		yield return new WaitForSeconds(waitTime);
		keyDoor.Open ();
		yield return new WaitForSeconds(waitTime + 3f);
		CameraToFollow ();
		yield return new WaitForSeconds(waitTime + 7f);
		NewCourse ();
	}

	void CameraToFollow(){
		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		cam.target = cameraStaticDoor;
	}


	void NewCourse(){
		++currentDish;
		dishSelection.AddCourse ();
		dish.ShowNewDish ();
	}
}


