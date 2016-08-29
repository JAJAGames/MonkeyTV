using UnityEngine;
using System.Collections;
using Enums;

public class DishList : MonoBehaviour {

	public const int ITEMSCOUNT = 4;

	public class FoodMenu {
		
		public itemsList[] ingredients = new itemsList[ITEMSCOUNT];
		public int itemsLeft;

		public FoodMenu (itemsList ing0, itemsList ing1) {
			ingredients [0] = ing0;
			ingredients [1] = ing1;
			ingredients [2] = itemsList.NO_ITEM;
			ingredients [3] = itemsList.NO_ITEM;
			itemsLeft = 2;
		}

		public FoodMenu (itemsList ing0, itemsList ing1, itemsList ing2) {
			ingredients [0] = ing0;
			ingredients [1] = ing1;
			ingredients [2] = ing2;
			ingredients [3] = itemsList.NO_ITEM;
			itemsLeft = 3;
		}

		public FoodMenu (itemsList ing0, itemsList ing1, itemsList ing2, itemsList ing3) {
			ingredients [0] = ing0;
			ingredients [1] = ing1;
			ingredients [2] = ing2;
			ingredients [3] = ing3;
			itemsLeft = 4;
		}

		public FoodMenu (FoodMenu menu) {
			ingredients [0] = menu.ingredients[0];
			ingredients [1] = menu.ingredients[1];
			ingredients [2] = menu.ingredients[2];
			ingredients [3] = menu.ingredients[3];
			itemsLeft = menu.itemsLeft;
		}

		public int GetIngredient (int ingredientNumber) {
			return (int)ingredients[ingredientNumber];
		}
	}

	public static FoodMenu[] menu = new FoodMenu[10];

	void Awake() {
		menu[0] = new FoodMenu(itemsList.ITEM_LVL1_LETTUCE, 	itemsList.ITEM_LVL1_CABBAGE);
		menu[1] = new FoodMenu(itemsList.ITEM_LVL1_LETTUCE, 	itemsList.ITEM_LVL1_PINEAPPLE);
		menu[2] = new FoodMenu(itemsList.ITEM_LVL1_PINEAPPLE, 	itemsList.ITEM_LVL1_CABBAGE);
		menu[3] = new FoodMenu(itemsList.ITEM_LVL1_CABBAGE,	itemsList.ITEM_LVL1_CARROT,	itemsList.ITEM_LVL1_MUSHROOM);
		menu[4] = new FoodMenu(itemsList.ITEM_LVL1_CARROT,		itemsList.ITEM_LVL1_LETTUCE,	itemsList.ITEM_LVL1_MUSHROOM);
		menu[5] = new FoodMenu(itemsList.ITEM_LVL1_CABBAGE,	itemsList.ITEM_LVL1_LETTUCE,	itemsList.ITEM_LVL1_MUSHROOM);
		menu[6] = new FoodMenu(itemsList.ITEM_LVL1_MUSHROOM,	itemsList.ITEM_LVL1_APPLE,		itemsList.ITEM_LVL1_BANANA);
		menu[7] = new FoodMenu(itemsList.ITEM_LVL1_CARROT,		itemsList.ITEM_LVL1_APPLE,		itemsList.ITEM_LVL1_ORANGE);
		menu[8] = new FoodMenu(itemsList.ITEM_LVL1_MUSHROOM,	itemsList.ITEM_LVL1_BANANA,	itemsList.ITEM_LVL1_ORANGE);
	}
}