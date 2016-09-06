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

	public static FoodMenu[] menu = new FoodMenu[15];

	void Awake() {
		//LEVEL 1
		menu[0] = new FoodMenu(
			itemsList.ITEM_LVL1_LETTUCE, 
			itemsList.ITEM_LVL1_CABBAGE
		);
		menu[1] = new FoodMenu(
			itemsList.ITEM_LVL1_LETTUCE,
			itemsList.ITEM_LVL1_PINEAPPLE
		);
		menu[2] = new FoodMenu(
			itemsList.ITEM_LVL1_PINEAPPLE, 
			itemsList.ITEM_LVL1_CABBAGE
		);
		menu[3] = new FoodMenu(
			itemsList.ITEM_LVL1_CABBAGE,
			itemsList.ITEM_LVL1_CARROT,
			itemsList.ITEM_LVL1_MUSHROOM
		);
		menu[4] = new FoodMenu(
			itemsList.ITEM_LVL1_CARROT,
			itemsList.ITEM_LVL1_LETTUCE,
			itemsList.ITEM_LVL1_MUSHROOM
		);
		menu[5] = new FoodMenu(
			itemsList.ITEM_LVL1_CABBAGE,
			itemsList.ITEM_LVL1_LETTUCE,
			itemsList.ITEM_LVL1_MUSHROOM
		);
		menu[6] = new FoodMenu(
			itemsList.ITEM_LVL1_MUSHROOM,
			itemsList.ITEM_LVL1_APPLE,	
			itemsList.ITEM_LVL1_BANANA
		);
		menu[7] = new FoodMenu(
			itemsList.ITEM_LVL1_CARROT,
			itemsList.ITEM_LVL1_APPLE,
			itemsList.ITEM_LVL1_ORANGE
		);
		menu[8] = new FoodMenu(
			itemsList.ITEM_LVL1_MUSHROOM,
			itemsList.ITEM_LVL1_BANANA,
			itemsList.ITEM_LVL1_ORANGE
		);
		//LEVEL 2
		menu[9] = new FoodMenu(
			itemsList.ITEM_LVL2_ITEM1,
			itemsList.ITEM_LVL2_ITEM2
		);
		menu[10] = new FoodMenu(
			itemsList.ITEM_LVL2_ITEM2,
			itemsList.ITEM_LVL2_ITEM1
		);
		menu[11] = new FoodMenu(
			itemsList.ITEM_LVL2_ITEM3,
			itemsList.ITEM_LVL2_ITEM4
		);
		menu[12] = new FoodMenu(
			itemsList.ITEM_LVL2_ITEM4,
			itemsList.ITEM_LVL2_ITEM3
		);
		menu[13] = new FoodMenu(
			itemsList.ITEM_LVL2_ITEM5,
			itemsList.ITEM_LVL2_ITEM6
		);
		menu[14] = new FoodMenu(
			itemsList.ITEM_LVL2_ITEM6,
			itemsList.ITEM_LVL2_ITEM5
		);
	}
}