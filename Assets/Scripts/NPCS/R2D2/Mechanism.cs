using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class Mechanism : MonoBehaviour {

	private const int TIMEDELAY = 5;
	public Canvas canvas;
	public Image image;
	public Image IGU_Mechanism;
	public Texture2D texture;
	public MessageController message;
	public ContinueButton buttonPress;
	public float timeQuest;
	private IGUIngredients ingredientsBar;


	public int mechanismCode;
	private Sprite[] sprites ;
	private bool showSelection = false;
	private PlayerMovement player;
	private DishClockController clockMechanism;

	void Awake(){
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		clockMechanism = GameObject.Find ("Clock").GetComponent<DishClockController> ();
		ingredientsBar = GameObject.Find ("IGUIngredients").GetComponent<IGUIngredients> ();
	}

	void Start(){
		ShowNewMechanism ();
	}


	public void ShowNewMechanism(){
		canvas.gameObject.SetActive(true);
		player.StopPlayer ();
		StartCoroutine (StartNewDish (5));	
	}


	IEnumerator NeWDish() {

		if (showSelection) {
			yield return new WaitForSeconds (0.25f);
			mechanismCode = Random.Range (0, sprites.Length);
			StartCoroutine (NeWDish ());
		}
		image.sprite = sprites [mechanismCode];
	}

	IEnumerator StartNewDish (float waitTime){
		IGU_Mechanism.gameObject.SetActive(false);
		buttonPress.gameObject.SetActive (true);
		showSelection = true;
		StartCoroutine(NeWDish());
		clockMechanism.countDown = true;

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
		clockMechanism.SetClock (TIMEDELAY);
		message.gameObject.SetActive (true);
		message.SetTime(TIMEDELAY);
		AudioManager.Instance.PlayFX(Enums.fxClip.COUNTDOWN);
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		player.enabled = true;

		mechanismCode = clockMechanism.GetCurrent ();
		image.sprite = sprites [mechanismCode];
		canvas.gameObject.SetActive(false);
		IGU_Mechanism.gameObject.SetActive(true);
		IGU_Mechanism.gameObject.GetComponent<IGUfromWorld> ().StartAnimation ();
		IGU_Mechanism.sprite = sprites [mechanismCode];

		ingredientsBar.ActualizeIcons();
		Invoke ("ToSearch", 5.0f);

	}


	void ToSearch(){
		clockMechanism.countDown = false;
		clockMechanism.text.rectTransform.localScale = Vector3.one;
		clockMechanism.SetClock (timeQuest);
		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);


		player.enabled = true;
		message.gameObject.SetActive (false);
	}
}
