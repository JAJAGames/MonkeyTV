using UnityEngine;
using System.Collections;

public class Enums : MonoBehaviour {

	public enum itemsListMasterChef {ITEM_MUSHROOM, ITEM_CABBAGE, ITEM_LETTUCE, ITEM_APPLE, ITEM_ORANGE, ITEM_PINEAPPLE, ITEM_BANANA, ITEM_CARROT, NO_ITEM};

	//Game State
	public enum state {INIT_SCENE, RUN_ANIMATION, RUN_INTRO, NEW_SEARCH, SEARCH_OBJECTS, DELIVER_OBJECTS, COMPLETED, WIN, LOOSE};

	//Game Level Scene
	public enum sceneLevel {MENU, LEVEL_1, LEVEL_2, LEVEL_BOSS};
}
