using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using InControl;

public class NewCraft : MonoBehaviour {

	private const int TIMEDELAY = 5;
	public Canvas canvas;
	public Image craftImage;
	public Image IGU_Dish;
	public Texture2D texture;
	public MessageController message;
	public ContinueButton buttonPress;
	public float timeQuest;
	private IGUIngredients ingredientsBar;


	public int craftCode;
	private Sprite[] sprites ;
	//private bool showSelection = false;
	private PlayerMovement player;
	private DishClockController clockDish;


	void Awake(){
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/" + texture.name);
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		clockDish = GameObject.Find ("Clock").GetComponent<DishClockController> ();
		ingredientsBar = GameObject.Find ("IGUIngredients").GetComponent<IGUIngredients> ();
	}

	public void StartNewCraft() {
		craftCode = clockDish.GetCurrent ();
		craftImage.sprite = sprites [craftCode];
		IGU_Dish.gameObject.SetActive(false);
		buttonPress.gameObject.SetActive (true);
		//showSelection = true;
		//StartCoroutine(NeWDish());
		clockDish.countDown = true;


		//showSelection = false;
		buttonPress.SetActiveItems ();
		clockDish.SetClock (TIMEDELAY);
		message.gameObject.SetActive (true);
		message.SetTime(TIMEDELAY);
		AudioManager.Instance.PlayFX(Enums.fxClip.FX_COUNTDOWN);
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		player.enabled = true;

		//craftImage.sprite = sprites [craftCode];
		canvas.gameObject.SetActive(false);
		IGU_Dish.gameObject.SetActive(true);
		IGU_Dish.gameObject.GetComponent<IGUfromWorld> ().StartAnimation ();
		IGU_Dish.sprite = sprites [craftCode];

		//ingredientsBar.ActualizeIcons();
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
