﻿using UnityEngine;
using System.Collections;

namespace Enums {
	public enum itemsListMC {ITEM_MUSHROOM, ITEM_CABBAGE, ITEM_LETTUCE, ITEM_BANANA, ITEM_APPLE, ITEM_ORANGE, ITEM_PINEAPPLE, ITEM_CARROT, NO_ITEM};

	//Game State
	public enum state {STATE_INIT, STATE_PLAY_CINEMATICS, STATE_INTRO, STATE_STATIC_CAMERA, STATE_CAMERA_FOLLOW_PLAYER, STATE_WIN, STATE_LOSE};

	//Game Level Scene
	public enum sceneLevel {MENU, LEVEL_1, LEVEL_2, LEVEL_BOSS};
}
