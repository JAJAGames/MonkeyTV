using UnityEngine;
using System.Collections;
using Enums;
using InControl;

public class PropDropItem : MonoBehaviour {

	public MeshRenderer meshRenderer;
	public PickItems player;
	private Animator anim;

	private Color _color, _initParticleColor;
	public Color _particleColor;

	private Material _material;
	private DishSelection dishSelection;

	public Transform cameraStaticPosition;
	public Transform cameraStaticPosition2;
	public Transform cameraGorilla;
	private CameraFollow cam;
	private Transform cameraStaticDoor;

	public OpenDoor keyDoor,secondKey;
	public int[] courses;
	public DishList.FoodMenu[] menu;
	public Dish dish;
	public IGUfromWorld[] IGUchek = new IGUfromWorld[3];

	int currentDish;
	bool firsTime = true;
	private InitSpawn spawn;

	public IGUItemBar itemBar;
	private IGUIngredients ingredientsBar;
	public ParticleSystem _particles, _particlesPot;
	// Use this for initialization
	void Awake (){
		cam = Camera.main.GetComponent<CameraFollow> ();
		meshRenderer = GetComponent<MeshRenderer> ();
		player = GameObject.FindWithTag ("Player").GetComponent<PickItems> ();
		anim = GameObject.FindWithTag ("Player").GetComponent<Animator>();
		dishSelection = GameObject.Find ("Clock").GetComponent<DishSelection> ();
		spawn = GameObject.Find ("Spawn Point").GetComponent<InitSpawn>();
		itemBar = GameObject.Find ("ItemBarMask").GetComponent<IGUItemBar> ();
		ingredientsBar = GameObject.Find ("IGUIngredients").GetComponent<IGUIngredients> ();
		_initParticleColor = _particles.startColor;
	}

	void Start () {
		
		_material = meshRenderer.sharedMaterial;

		//can be set in the inspector
		_material.globalIlluminationFlags = MaterialGlobalIlluminationFlags.RealtimeEmissive;

		_color = meshRenderer.material.GetColor ("_EmissionColor");


		courses = dishSelection.GetCourses ();

		menu = new DishList.FoodMenu[3];
		menu [0] = new DishList.FoodMenu(DishList.menu[courses[0]]);
		menu [1] = new DishList.FoodMenu(DishList.menu[courses[1]]);
		menu [2] = new DishList.FoodMenu(DishList.menu[courses[2]]);

		dish = (Dish)GameObject.FindObjectOfType(typeof(Dish));

		itemBar.ChangeItemBarSize(menu[dishSelection.currentCourse].itemsLeft);
	}

	// Update is called once per frame
	void Update () {
		//Cooking animation
	}


	private void OnTriggerStay (Collider other){
		var inputDevice = InputManager.ActiveDevice;
		if (other.CompareTag ("Player") ) {
			if (player.haveItem()){
				checkItem ();
			}
		}
	}

	private void OnTriggerEnter (Collider other){	//Si el player no porta ingredient no s'ha d'il.luminar
		if (other.CompareTag ("Player") && player.haveItem()) {
			meshRenderer.material.SetColor ("_EmissionColor", Color.grey);
		}
	}

	private void OnTriggerExit (Collider other){
		if (other.CompareTag ("Player")) {
			meshRenderer.material.SetColor ("_EmissionColor", _color);
		}
	}

	private void checkItem () {
		bool foundIngredient = false;

		if (firsTime) {
			currentDish = dishSelection.currentCourse;
			firsTime = false;
		}


		for (int i = 0; i < DishList.ITEMSCOUNT; ++i) {
			if (foundIngredient) {
				menu[dishSelection.currentCourse].ingredients [i - 1] = menu[dishSelection.currentCourse].ingredients [i];
			}

			if (!foundIngredient && menu[currentDish].ingredients[i] == player.actualItem) {
				menu[dishSelection.currentCourse].ingredients [i] = itemsListMC.NO_ITEM_MC;
				IGUchek [i].gameObject.SetActive (true);
				IGUchek [i].StartAnimation ();
				player.throwItem ();
				foundIngredient = true;
				--menu[dishSelection.currentCourse].itemsLeft;
			}
		}
			
		if (!foundIngredient) {	
			StartCoroutine (WrongIngredient ());//fer algun feedback de que no es l'ingredient correcte
		} else { 																//ja no te ingredient deixa d'il.luminar
			anim.SetBool("Pick_Object",false);
			meshRenderer.material.SetColor ("_EmissionColor", _color);
		
			if (menu [dishSelection.currentCourse].itemsLeft == 0) {
				dishSelection.clock = Mathf.Infinity;
				player.GetComponent<PlayerMovement> ().StopPlayer();

				if (dishSelection.currentCourse == 0) {
					StartCoroutine (OpenDoor (1f));
					spawn.Spawning ();
				}

				if (dishSelection.currentCourse == 1) {
					StartCoroutine (Open2Door (2f));
				}
					
				if (dishSelection.currentCourse == 2) {
					gamestate.Instance.SetState (Enums.state.STATE_WIN);
				}					
			}
			AudioManager.Instance.PlayFX(Enums.fxClip.GUI_PICK_BONUS);

			StartCoroutine(ActualizeIngredientsBar ());
		}
	}

	IEnumerator OpenDoor(float waitTime){
		gamestate.Instance.SetState (state.STATE_STATIC_CAMERA);
		cameraStaticDoor = cam.target;
		cam.target = cameraStaticPosition;
		yield return new WaitForSeconds(waitTime);
		keyDoor.Open ();
		yield return new WaitForSeconds(waitTime + 1f);
		CameraToFollow ();
		//yield return new WaitForSeconds(waitTime + 2f);
		NewCourse ();
	}

	IEnumerator Open2Door(float waitTime){
		gamestate.Instance.SetState (state.STATE_STATIC_CAMERA);
		cameraStaticDoor = cam.target;
		cam.target = cameraStaticPosition2;
		yield return new WaitForSeconds(waitTime);
		secondKey.Open ();
		yield return new WaitForSeconds(waitTime + 1f);
		CameraToFollow ();
		//yield return new WaitForSeconds(waitTime + 2f);
		NewCourse ();
	}

	void CameraToFollow(){
		//gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		gamestate.Instance.SetState (state.STATE_SWAP_CAMERA);
		cam.target = cameraGorilla;
	}

	void NewCourse(){
		++currentDish;
		dishSelection.AddCourse ();
		itemBar.ChangeItemBarSize(menu[dishSelection.currentCourse].itemsLeft);
		dish.ShowNewDish ();
	}

	private IEnumerator ActualizeIngredientsBar(){
		yield return new WaitForSeconds(0.5f);
		ingredientsBar.ActualizeIcons ();
		itemBar.ChangeItemBarSize(menu[dishSelection.currentCourse].itemsLeft);
	}

	private IEnumerator WrongIngredient(){
		AudioManager.Instance.PlayFX (fxClip.WRONG_DELIVER);
		_particlesPot.startColor = _particleColor;
		_particles.startColor = _particleColor;
		player.throwItem ();
		yield return new WaitForSeconds(2.5f);
		_particles.startColor = _initParticleColor;
		_particlesPot.startColor = _initParticleColor;
	}

	public int GetIngredient(int ingredientNumber) {
		return (int)menu [dishSelection.currentCourse].ingredients [ingredientNumber];
	}
}