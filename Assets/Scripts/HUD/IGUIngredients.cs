using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGUIngredients : MonoBehaviour {
	public Image[] iguImages;
	public Texture2D ingredientsTextures;
	private Sprite[] ingredientsSprites;

	private PropDropItem menu;

	void Awake() {
		iguImages = gameObject.GetComponentsInChildren<Image> ();
		ingredientsSprites = Resources.LoadAll <Sprite>(@"Images/IGU/" + ingredientsTextures.name);
		menu = GameObject.Find ("PRMC_Olla").GetComponent<PropDropItem>();
	}

	public void ActualizeIcons() {
		iguImages [0].sprite = ingredientsSprites[menu.GetIngredient(0)];
		iguImages [1].sprite = ingredientsSprites[menu.GetIngredient(1)];
		iguImages [2].sprite = ingredientsSprites[menu.GetIngredient(2)];

		iguImages[0].color = Color.white;
		iguImages[1].color = Color.white;
		iguImages[2].color = Color.white;
	}
}
