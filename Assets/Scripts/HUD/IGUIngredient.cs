using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGUIngredient : MonoBehaviour {

	public Texture2D texture;
	public uint ingredientNumber;
	public float speedAnimation;
	public Transform target;

	private Vector2 HUDPoint;
	private float alpha;
	private Image img;
	private bool animate;
	private int course;
	private DishSelection dishSelection;
	private DishList.FoodMenu ingredients;
	private Sprite[] sprites ;

	private DishList.FoodMenu[] menu;

	void Awake () {
		img = GetComponent<Image> ();
		dishSelection = GameObject.Find ("Clock").GetComponent<DishSelection> ();
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name);
		menu = GameObject.Find ("PRMC_Olla").GetComponent<PropDropItem> ().menu;
	}
		

	public void GetIcon(){
		course = dishSelection.GetCurrent ();
		ingredients = new DishList.FoodMenu(DishList.menu[course]);
		//ingredients = new DishList.FoodMenu(menu[course]);

		img.sprite = sprites [(int)ingredients.ingredients [ingredientNumber]];
		img.color = Color.white;
	}

	/*
	public void ActualizeIcon() {
		course = dishSelection.GetCurrent ();
		ingredients = new DishList.FoodMenu(menu[dishSelection.currentCourse]);
		img.sprite = sprites [(int)ingredients.ingredients [ingredientNumber]];
		img.color = Color.white;
	}
	*/
}
