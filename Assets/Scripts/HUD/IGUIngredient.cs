using UnityEngine;
using System;
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
	private Image checkOk;
	private bool animate;
	private int course;
	private DishSelection dishSelection;
	private DishList.FoodMenu ingredients;
	private Sprite[] sprites ;



	void Awake () {
		img = GetComponent<Image> ();
		checkOk = transform.GetChild (0).GetComponent<Image> ();
		SetVisible (true);
		SetCheckVisible (false);
		animate = false;

		dishSelection = GameObject.Find ("Clock").GetComponent<DishSelection> ();
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
	}
		
	public void Update(){

		if (Input.GetKeyDown (KeyCode.L))
			GetHUDPoint ();
		if (animate) {
			
			if (alpha > 0)
				alpha -= Time.deltaTime * speedAnimation;
			
			else {
				alpha = 0;
				animate = false;
			}
			Vector2 final = HUDPoint * (1 - alpha) + RectTransformUtility.WorldToScreenPoint (Camera.main, target.position) * alpha;

			img.transform.position = final;
		}

	}

	private void GetHUDPoint(){
		HUDPoint = img.rectTransform.position;					//gameobject must be child of canvas and the anchorpoint only can be set on the center.
		alpha = 1f;
		animate = true;
	}

	public void SetVisible (bool visible){
		Color color = Color.white;
		color.a = Convert.ToUInt16 (visible);
		img.color = color;
	}

	public void SetCheckVisible (bool visible){
		Color color = Color.white;
		color.a = Convert.ToUInt16 (visible);
		checkOk.color = color;
	}

	public void GetIcon(){
		course = dishSelection.GetCurrent ();
		ingredients = new DishList.FoodMenu(DishList.menu[course]);
		img.sprite = sprites [(int)ingredients.ingredients [ingredientNumber]];
		
	}

}
