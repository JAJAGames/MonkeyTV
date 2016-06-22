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
		buttonPress.gameObject.SetActive (true);
		showSelection = true;
		StartCoroutine(NeWDish());
		clockDish.countDown = true;
		var inputDevice = InputManager.ActiveDevice;
		//yield return new WaitForSeconds(1f);

		while( !(Input.GetButton("Pick") || inputDevice.Action3) )   // that's to have yield return new [ WaitForsomething (bool) ];
		{
			yield return null;
		}
		
		showSelection = false;
		buttonPress.SetActiveItems ();
		clockDish.SetClock (TIMEDELAY);
		message.gameObject.SetActive (true);
		message.SetTime(TIMEDELAY);
		AudioManager.Instance.PlayFX(Enums.fxClip.COUNTDOWN);
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		player.enabled = true;
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
