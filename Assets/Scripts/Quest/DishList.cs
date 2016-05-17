using UnityEngine;
using System.Collections;
using Enums;

public class DishList : MonoBehaviour {

	public const int ITEMSCOUNT = 4;

	public class FoodMenu {
		
		public itemsListMC[] ingredients = new itemsListMC[ITEMSCOUNT];
		public int itemsLeft;

		public FoodMenu (itemsListMC ing0, itemsListMC ing1) {
			ingredients [0] = ing0;
			ingredients [1] = ing1;
			ingredients [2] = itemsListMC.NO_ITEM;
			ingredients [3] = itemsListMC.NO_ITEM;
			itemsLeft = 2;
		}

		public FoodMenu (itemsListMC ing0, itemsListMC ing1, itemsListMC ing2) {
			ingredients [0] = ing0;
			ingredients [1] = ing1;
			ingredients [2] = ing2;
			ingredients [3] = itemsListMC.NO_ITEM;
			itemsLeft = 3;
		}

		public FoodMenu (itemsListMC ing0, itemsListMC ing1, itemsListMC ing2, itemsListMC ing3) {
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
	}

	public static FoodMenu[] menu = new FoodMenu[10];

	void Awake() {
		menu[0] = new FoodMenu(itemsListMC.ITEM_MUSHROOM, itemsListMC.ITEM_LETTUCE, itemsListMC.ITEM_CABBAGE);
		menu[1] = new FoodMenu(itemsListMC.ITEM_LETTUCE, itemsListMC.ITEM_APPLE, itemsListMC.ITEM_ORANGE);
		menu[2] = new FoodMenu(itemsListMC.ITEM_LETTUCE, itemsListMC.ITEM_CARROT, itemsListMC.ITEM_MUSHROOM, itemsListMC.ITEM_ORANGE);
		menu[3] = new FoodMenu(itemsListMC.ITEM_ORANGE, itemsListMC.ITEM_APPLE, itemsListMC.ITEM_BANANA, itemsListMC.ITEM_MUSHROOM);
		menu[4] = new FoodMenu(itemsListMC.ITEM_CABBAGE, itemsListMC.ITEM_MUSHROOM, itemsListMC.ITEM_CARROT);
		menu[5] = new FoodMenu(itemsListMC.ITEM_PINEAPPLE, itemsListMC.ITEM_BANANA, itemsListMC.ITEM_APPLE, itemsListMC.ITEM_APPLE);
		menu[6] = new FoodMenu(itemsListMC.ITEM_CARROT, itemsListMC.ITEM_PINEAPPLE, itemsListMC.ITEM_BANANA);
		menu[7] = new FoodMenu(itemsListMC.ITEM_ORANGE, itemsListMC.ITEM_APPLE, itemsListMC.ITEM_BANANA);
		menu[8] = new FoodMenu(itemsListMC.ITEM_ORANGE, itemsListMC.ITEM_APPLE);
		menu[9] = new FoodMenu(itemsListMC.ITEM_BANANA, itemsListMC.ITEM_APPLE);
	}
}