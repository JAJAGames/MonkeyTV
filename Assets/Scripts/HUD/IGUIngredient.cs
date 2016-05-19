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



	void Awake () {
		img = GetComponent<Image> ();
		dishSelection = GameObject.Find ("Clock").GetComponent<DishSelection> ();
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
	}
		

	public void GetIcon(){
		course = dishSelection.GetCurrent ();
		ingredients = new DishList.FoodMenu(DishList.menu[course]);
		img.sprite = sprites [(int)ingredients.ingredients [ingredientNumber]];
		
	}

}
