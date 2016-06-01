﻿using UnityEngine;
using System.Collections;

namespace Enums {
	//Game State
	public enum state {STATE_INIT, STATE_PLAY_CINEMATICS, STATE_INTRO, STATE_STATIC_CAMERA, STATE_CAMERA_FOLLOW_PLAYER, STATE_PLAYER_PAUSED,STATE_WIN, STATE_LOSE};

	//Game Level Scene
	public enum sceneLevel {MENU, LEVEL_1, LEVEL_2, LEVEL_BOSS};

	//Game FX
	public enum fxClip {BUTTON_HOVER, BUTTON_PRESSED, NO_FX};

	//PLAYER
	public enum playerState {PLAYER_STATE_MORTAL, PLAYER_STATE_BONUS_UNIFORM, PLAYER_STATE_GOD};

	//LEVELS
	//LEVEL MONKEY CHEF
	public enum itemsListMC {ITEM_MUSHROOM, ITEM_CABBAGE, ITEM_LETTUCE, ITEM_BANANA, ITEM_APPLE, ITEM_ORANGE, ITEM_PINEAPPLE, ITEM_CARROT, NO_ITEM_MC};
	//LEVEL STAR WARS
	public enum itemsListMW {ITEM_DROID, NO_ITEM_MW};

	//BONUS
	public enum enumBonus {BONUS_UNIFORM, BONUS_TIME, BONUS_KEY};

	//ENEMIES
	//ENEMY SIMPLE
	public enum enemyTypeSimple {SIMPLE_TYPE_RUNNER, SIMPLE_TYPE_JUMPER}
	public enum enemyStateSimple {SIMPLE_STATE_IDLE, SIMPLE_STATE_CHASE, SIMPLE_STATE_ESCAPE}
}
