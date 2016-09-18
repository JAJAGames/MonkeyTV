using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class PropDropItemGeneric : MonoBehaviour {

	public PickItems player;

	private DishClockController dishSelection;
	public int[] courses;

	void Awake (){
		player = GameObject.Find ("Player").GetComponent<PickItems> ();
		dishSelection = GameObject.Find ("Clock").GetComponent<DishClockController> ();
	}

	void Start () {

		courses = dishSelection.GetCourses ();
		/*
		menu = new DishList.FoodMenu[3];
		menu [0] = new DishList.FoodMenu(DishList.menu[courses[0]]);
		menu [1] = new DishList.FoodMenu(DishList.menu[courses[1]]);
		menu [2] = new DishList.FoodMenu(DishList.menu[courses[2]]);

		dish = (Dish)GameObject.FindObjectOfType(typeof(Dish));*/

	}

		
	private void OnTriggerStay (Collider other) {
		if (other.CompareTag ("Player") ) {
			Debug.Log("PLAUER TE ITEM?" + player.haveItem());
			if (player.haveItem()){
				checkItem ();
			}
		}
	}
		
	private void checkItem () {
		Debug.Log ("CHECK ITEM");
		/*
		bool foundIngredient = false;

		for (int i = 0; i < DishList.ITEMSCOUNT; ++i) {
			
		}
			
		if (!foundIngredient) {	
			//StartCoroutine (WrongIngredient ());			//fer algun feedback de que no es l'ingredient correcte
		} else { 											

			//StartCoroutine(ActualizeIngredientsBar ());
		}
		*/
	}
		
	/*void NewCourse(){
		++currentDish;
		dishSelection.AddCourse ();
		itemBar.ChangeItemBarSize(menu[dishSelection.currentCourse].itemsLeft);
		dish.ShowNewDish ();
	}

	private IEnumerator ActualizeIngredientsBar(){
		yield return new WaitForSeconds(0.5f);
		ingredientsBar.ActualizeIcons ();
		itemBar.ChangeItemBarSize(menu[dishSelection.currentCourse].itemsLeft);
	}
		
	public int GetIngredient(int ingredientNumber) {
		return (int)menu [dishSelection.currentCourse].ingredients [ingredientNumber];
	}*/
}