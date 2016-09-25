using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class PropDropItemGeneric : MonoBehaviour {

	//PLAYER
	public PickItems player;
	private Animator playerAnim;
	public PlayerMovement playerMov;

	//DISH
	private DishClockController dishSelection;
	public int[] courses;
	public DishList.FoodMenu[] actualCraft;

	//IGU
	private IGUIngredients ingredientsBar;
	public IGUItemBar itemBar;
	public IGUfromWorld[] IGUchek = new IGUfromWorld[3];

	//TELEPORT
	private Teleport teleport;
	private GameObject teleportParticles;

	//CAMERA
	public CameraFollow camera;
	public Transform cameraPosition;

	//R2D2
	public GameObject R2D2;
	public StatePatternR2D2 statePattern;
	public Transform teleportUp;
	public Transform teleportDown;

	//OTHER
	public GameObject nextLocation;


	void Awake (){
		player = GameObject.Find ("Player").GetComponent<PickItems> ();
		playerAnim = GameObject.FindWithTag ("Player").GetComponent<Animator>();
		playerMov = GameObject.FindWithTag ("Player").GetComponent<PlayerMovement>();

		dishSelection = GameObject.Find ("Clock").GetComponent<DishClockController> ();
		ingredientsBar = GameObject.Find ("IGUIngredients").GetComponent<IGUIngredients> ();
		itemBar = GameObject.Find ("ItemBarMask").GetComponent<IGUItemBar> ();

		camera = Camera.main.GetComponent<CameraFollow>();
		//cameraPosition = GameObject.Find ("BossCameraPosition").transform;

		teleport = GameObject.Find ("Teleport1").GetComponent<Teleport> ();
		teleportParticles = GameObject.Find ("Teleport1").transform.FindChild("Particles").gameObject;

		R2D2 = GameObject.Find ("R2D2");
		statePattern = R2D2.GetComponent<StatePatternR2D2> ();
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

			if (actualCraft [currentCourse].itemsLeft == 0) {
				dishSelection.clock = Mathf.Infinity;
				playerMov.StopPlayer();

				switch (gamestate.Instance.GetLevel ()) {
				case Enums.sceneLevel.LEVEL_2:
					actionsLVL2 ();
					break;
				}
					
			}
			AudioManager.Instance.PlayFX(Enums.fxClip.FX_GUI_PICK_BONUS);

			StartCoroutine(ActualizeIngredientsBar ());
		}

	}

	public void actionsLVL2() {
		switch (dishSelection.currentCourse) {
		case 0:
			teleportParticles.SetActive (false);
			transform.position = nextLocation.transform.position;
			statePattern.navMeshAgent.enabled = false;
			StartCoroutine (ViewBoss ());
			StartCoroutine (TeleportR2D2 ());
			statePattern.navMeshAgent.enabled = true;
			teleport.enabled = true;
			playerMov.enabled = true;
			break;
		case 1:
			StartCoroutine (BombExplode ());
			StartCoroutine (ViewBoss2 ());
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

	//ACTIONS WHEN A CRAFT IS DONE
	private IEnumerator TeleportR2D2() {
		yield return new WaitForSeconds(2.0f);
		R2D2.transform.position = teleportUp.transform.position;
		yield return new WaitForSeconds(1.0f);
	}

	private IEnumerator ViewBoss() {
		gamestate.Instance.SetState(Enums.state.STATE_SWAP_CAMERA);
		camera.target = cameraPosition;
		R2D2.transform.position = teleportUp.transform.position;
		yield return new WaitForSeconds(3.0f);
		gamestate.Instance.SetState(Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		yield return new WaitForSeconds(0.5f);
	}

	private IEnumerator BombExplode() {
		yield return new WaitForSeconds(2.5f);
	}

	private IEnumerator ViewBoss2() {
		yield return new WaitForSeconds(2.5f);
	}
}