﻿using UnityEngine;
using System.Collections;

namespace Enums {
	//Game State
	public enum state {
		STATE_INIT,
		STATE_PLAY_CINEMATICS,
		STATE_INTRO,
		STATE_MENU,
		STATE_STATIC_CAMERA,
		STATE_CAMERA_FOLLOW_PLAYER,
		STATE_PLAYER_PAUSED,
		STATE_SWAP_CAMERA,
		STATE_WIN,
		STATE_LOSE
	};

	//Game Level Scene
	public enum sceneLevel {
		MENU,
		LEVEL_1,
		LEVEL_2,
		LEVEL_BOSS,
		WIN,
		LOSE
	};

	//Game FX
	public enum fxClip {
		FX_BUTTON_HOVER,
		FX_BUTTON_PRESSED,
		FX_GUI_PICK_BONUS,
		FX_OPEN_DOOR,
		FX_OPEN_JAIL,
		FX_COUNTDOWN,
		FX_WATER_CUBE,
		FX_PLACARD_COLLISION,
		FX_PICK_FOOD,
		FX_PICK_SUIT,
		FX_UNLOCK_LEVER,
		FX_PICK_CLOK_KEY,
		FX_WRONG_DELIVER,
		FX_FALL_ON_BACKSIDE,
		FX_JUMPER_PAD,
		FX_PUNCH,
		NO_FX
	};

	//PLAYER
	public enum playerState {
		PLAYER_STATE_MORTAL,
		PLAYER_STATE_BONUS_UNIFORM,
		PLAYER_STATE_GOD
	};

	//LEVELS
	public enum itemsList {
		ITEM_LVL1_MUSHROOM,
		ITEM_LVL1_CABBAGE,
		ITEM_LVL1_LETTUCE,
		ITEM_LVL1_BANANA,
		ITEM_LVL1_APPLE,
		ITEM_LVL1_ORANGE,
		ITEM_LVL1_PINEAPPLE,
		ITEM_LVL1_CARROT,
		ITEM_LVL2_HAMMER,
		ITEM_LVL2_SAW,
		ITEM_LVL2_BATTERY,
		ITEM_LVL2_WRENCH,
		ITEM_LVL2_SCREWDRIVER,
		ITEM_LVL2_ENGINE,
		NO_ITEM
	};

	//BONUS
	public enum enumBonus {
		BONUS_UNIFORM,
		BONUS_TIME,
		BONUS_KEY
	};

	//ENEMIES
	//ENEMY SIMPLE
	public enum enemyTypeSimple {
		SIMPLE_HUNGRY,
		SIMPLE_AGGRESIVE
	};
	public enum enemyStateSimple {
		SIMPLE_STATE_IDLE,
		SIMPLE_STATE_CHASE,
		SIMPLE_STATE_ESCAPE,
		SIMPLE_STATE_ATTACK
	};
	//ENEMY COMPLEX
	public enum enemyTypeComplex {
		COMPLEX_TYPE_BASIC
	};
	public enum enemyStateComplex {
		COMPLEX_STATE_IDLE,
		COMPLEX_STATE_CHASE,
		COMPLEX_STATE_ESCAPE,
		COMPLEX_STATE_ATTACK,
		COMPLEX_STATE_PATROL
	};
	//BOSS
	public enum enemyStateBoss {
		BOSS_STATE_IDLE,
		BOSS_STATE_ATTACK
	};

	//R2D2 States
	public enum R2D2State {
		IDLE_STATE,
		MOVE_STATE,
		ASK_FOR_ITEMS_STATE,
		RECEIVE_ITEMS_STATE
	};

	public enum R2D2Poin{
		INIT,
		LEVELER,
		BOMB,
		SHIP
	}

	//Boss States defines boss action
	public enum BossState{
		IDLE_SATE,
		MOVE_STATE,
		PUNCH_STATE,
		DAMAGED_STATE,
		DEAD_STATE
	}

	//boss phase defines boss behavior 
	public enum BossPhase{
		OUT_OF_COMBAT,
		COMBAT_PHASE_1,
		COMBAT_PHASE_2
	}

<<<<<<< Updated upstream
	public enum FocusTarget{
		NONE,
		MONKEY,
		PLAYER
	}
=======
	public enum BossLiteState {
		BOSSLITE_IDLE_STATE,
		BOSSLITE_MOVE_STATE
	};
>>>>>>> Stashed changes
}
