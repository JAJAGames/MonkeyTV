using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class Dish : MonoBehaviour {

	public Canvas canvas;
	public Image image;
	public Image IGU_Dish;
	public Texture2D texture;
	public GameObject buttonPress;

	private IGUIngredients ingredientsBar;


	public int dishCode;
	private Sprite[] sprites ;
	private bool showSelection = false;
	private PlayerMovement player;
	private DishSelection clockDish;
	void Awake(){
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		clockDish = GameObject.Find ("Clock").GetComponent<DishSelection> ();
		ingredientsBar = GameObject.Find ("IGUIngredients").GetComponent<IGUIngredients> ();
	}

	void Start(){
			ShowNewDish ();
	}


	public void ShowNewDish(){
		
		canvas.gameObject.SetActive(true);
		gamestate.Instance.SetState (Enums.state.STATE_SWAP_CAMERA);
		player.StopPlayer ();
		StartCoroutine (StartNewDish (5));		
	}


	IEnumerator NeWDish() {

		if (showSelection) {
			yield return new WaitForSeconds(0.25f);
			dishCode = Random.Range (0, sprites.Length);
			StartCoroutine(NeWDish());
		}else
			dishCode = clockDish.GetCurrent ();


		image.sprite = sprites [dishCode];

	}

	IEnumerator StartNewDish (float waitTime){
		IGU_Dish.gameObject.SetActive(false);
		buttonPress.SetActive (true);
		showSelection = true;
		StartCoroutine(NeWDish());
		clockDish.countDown = true;

		var inputDevice = InputManager.ActiveDevice;

		while( !(Input.GetButton("Pick") || inputDevice.Action3) )   // that's to have yield return new [ WaitForsomething (bool) ];
		{
			yield return null;
		}
		
		showSelection = false;

		clockDish.SetClock (5);
		AudioManager.Instance.PlayFX(Enums.fxClip.COUNTDOWN);

		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		player.enabled = true;

		Invoke ("ToSearch", 5.0f);

	}

	void ToSearch(){
		clockDish.countDown = false;
		clockDish.text.rectTransform.localScale = Vector3.one;
		clockDish.SetClock (120);
		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		canvas.gameObject.SetActive(false);
		IGU_Dish.gameObject.SetActive(true);
		IGU_Dish.gameObject.GetComponent<IGUfromWorld> ().StartAnimation ();
		IGU_Dish.sprite = sprites [dishCode];

		ingredientsBar.ActualizeIcons();

		player.enabled = true;
	}
}
