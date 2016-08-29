using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class Dish : MonoBehaviour {

	private const int TIMEDELAY = 5;
	public Canvas canvas;
	public Image image;
	public Image IGU_Dish;
	public Texture2D texture;
	public MessageController message;
	public ContinueButton buttonPress;
	public float timeQuest;
	private IGUIngredients ingredientsBar;


	public int dishCode;
	private Sprite[] sprites ;
	private bool showSelection = false;
	private PlayerMovement player;
	private DishClockController clockDish;

	void Awake(){
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		clockDish = GameObject.Find ("Clock").GetComponent<DishClockController> ();
		ingredientsBar = GameObject.Find ("IGUIngredients").GetComponent<IGUIngredients> ();
	}

	void Start(){
		ShowNewDish ();
	}


	public void ShowNewDish(){
		
		canvas.gameObject.SetActive(true);
		//gamestate.Instance.SetState (Enums.state.STATE_SWAP_CAMERA);
		player.StopPlayer ();
		StartCoroutine (StartNewDish (5));	
	}


	IEnumerator NeWDish() {

		if (showSelection) {
			yield return new WaitForSeconds (0.25f);
			dishCode = Random.Range (0, sprites.Length);
			StartCoroutine (NeWDish ());
		}
		image.sprite = sprites [dishCode];
	}

	IEnumerator StartNewDish (float waitTime){
		IGU_Dish.gameObject.SetActive(false);
		buttonPress.gameObject.SetActive (true);
		showSelection = true;
		StartCoroutine(NeWDish());
		clockDish.countDown = true;

		// that's to have yield return new [ WaitForsomething (bool) ];
		while (gamestate.Instance.GetState () == Enums.state.STATE_PLAY_CINEMATICS) {
			yield return null;
		}

		yield return new WaitForSeconds (3.0f);


		var inputDevice = InputManager.ActiveDevice;
		while(!(Input.GetButton("Pick") || inputDevice.Action3)) {
			yield return null;
		}

		showSelection = false;
		buttonPress.SetActiveItems ();
		clockDish.SetClock (TIMEDELAY);
		message.gameObject.SetActive (true);
		message.SetTime(TIMEDELAY);
		AudioManager.Instance.PlayFX(Enums.fxClip.FX_COUNTDOWN);
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		player.enabled = true;

		dishCode = clockDish.GetCurrent ();
		image.sprite = sprites [dishCode];
		canvas.gameObject.SetActive(false);
		IGU_Dish.gameObject.SetActive(true);
		IGU_Dish.gameObject.GetComponent<IGUfromWorld> ().StartAnimation ();
		IGU_Dish.sprite = sprites [dishCode];

		ingredientsBar.ActualizeIcons();
		Invoke ("ToSearch", 5.0f);

	}


	void ToSearch(){
		clockDish.countDown = false;
		clockDish.text.rectTransform.localScale = Vector3.one;
		clockDish.SetClock (timeQuest);
		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);


		player.enabled = true;
		message.gameObject.SetActive (false);
	}
}
