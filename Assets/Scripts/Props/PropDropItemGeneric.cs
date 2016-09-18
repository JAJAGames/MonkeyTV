using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class PropDropItemGeneric : MonoBehaviour {

	public PickItems player;
	private Animator playerAnim;
	public PlayerMovement playerMov;



	private DishClockController dishSelection;
	public int[] courses;

	public DishList.FoodMenu[] actualCraft;

	private IGUIngredients ingredientsBar;
	public IGUItemBar itemBar;


	public IGUfromWorld[] IGUchek = new IGUfromWorld[3];

	//public MessageController message;



	void Awake (){
		player = GameObject.Find ("Player").GetComponent<PickItems> ();
		playerAnim = GameObject.FindWithTag ("Player").GetComponent<Animator>();
		playerMov = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement>();

		dishSelection = GameObject.Find ("Clock").GetComponent<DishClockController> ();
		ingredientsBar = GameObject.Find ("IGUIngredients").GetComponent<IGUIngredients> ();
		itemBar = GameObject.Find ("ItemBarMask").GetComponent<IGUItemBar> ();
	}

	void Start () {

		courses = dishSelection.GetCourses ();

		actualCraft = new DishList.FoodMenu[3];
		actualCraft [0] = new DishList.FoodMenu(DishList.menu[courses[0]]);
		actualCraft [1] = new DishList.FoodMenu(DishList.menu[courses[1]]);
		actualCraft [2] = new DishList.FoodMenu(DishList.menu[courses[2]]);
	}

		
	private void OnTriggerStay (Collider other) {
		if (other.CompareTag ("Player") ) {
			if (player.haveItem()){
				checkItem ();
			}
		}
	}
		
	private void checkItem () {
		bool foundIngredient = false;
		int currentCourse = dishSelection.currentCourse;

		for (int i = 0; i < DishList.ITEMSCOUNT; ++i) {
			if (foundIngredient) {
				actualCraft[currentCourse].ingredients [i - 1] = actualCraft[currentCourse].ingredients [i];
			}

			if (!foundIngredient && actualCraft[currentCourse].ingredients[i] == player.actualItem) {
				actualCraft[currentCourse].ingredients [i] = itemsList.NO_ITEM;
				IGUchek [i].gameObject.SetActive (true);
				IGUchek [i].StartAnimation ();
				player.throwItem ();
				foundIngredient = true;
				--actualCraft[currentCourse].itemsLeft;
			}
		}

		if (!foundIngredient) {	
			StartCoroutine (WrongItem());			//fer algun feedback de que no es l'ingredient correcte
		} else { 											
			playerAnim.SetBool("Pick_Object",false);
			//meshRenderer.material.SetColor ("_EmissionColor", _color);

			if (actualCraft [currentCourse].itemsLeft == 0) {
				dishSelection.clock = Mathf.Infinity;
				playerMov.StopPlayer();

				switch (gamestate.Instance.GetLevel ()) {
				case Enums.sceneLevel.LEVEL_2:
					actionsLVL2 ();
					break;
				}

				//if (message.gameObject.activeSelf)
				//	message.gameObject.SetActive (false);
			}
			AudioManager.Instance.PlayFX(Enums.fxClip.FX_GUI_PICK_BONUS);

			StartCoroutine(ActualizeIngredientsBar ());
		}

	}

	public void actionsLVL2() {
		switch (dishSelection.currentCourse) {
		case 0:
			playerMov.enabled = true;
			Debug.Log ("ACTIVATE TELEPORT");
			//ActivateTeleport ();
			//StartCoroutine (ViewBoss1 (10f));
			break;
		case 1:
			//StartCoroutine (ViewBoss2 (10f));
			gamestate.Instance.SetState (Enums.state.STATE_WIN);
			break;
		}
	}

	private IEnumerator ActualizeIngredientsBar(){
		yield return new WaitForSeconds(0.5f);
		ingredientsBar.ActualizeIcons ();
		itemBar.ChangeItemBarSize(actualCraft[dishSelection.currentCourse].itemsLeft);
	}

	public void ActualizeIngredientsBarNow() {
		ingredientsBar.ActualizeIcons ();
		itemBar.ChangeItemBarSize(actualCraft[dishSelection.currentCourse].itemsLeft);
	}

	public int GetIngredient(int ingredientNumber) {
		return (int)actualCraft [dishSelection.currentCourse].ingredients [ingredientNumber];
	}

	private IEnumerator WrongItem(){
		Debug.Log ("WRONG ITEM");
		/*AudioManager.Instance.PlayFX (fxClip.FX_WRONG_DELIVER);
		_particlesPot.startColor = _particleColor;
		_particles.startColor = _particleColor;
		player.throwItem ();
		yield return new WaitForSeconds(2.5f);
		_particles.startColor = _initParticleColor;
		_particlesPot.startColor = _initParticleColor;*/

		yield return new WaitForSeconds(2.5f);
	}
}