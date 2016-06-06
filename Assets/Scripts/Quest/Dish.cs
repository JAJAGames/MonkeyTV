using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dish : MonoBehaviour {

	public Canvas canvas;
	public Image image;
	public Image IGU_Dish;
	public Texture2D texture;

	public IGUIngredient iconFirst;
	public IGUIngredient iconSecond;
	public IGUIngredient iconThird;


	public int dishCode;
	private Sprite[] sprites ;
	private bool showSelection = false;
	private PlayerMovement player;
	private DishSelection clockDish;

	void Awake(){
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
		clockDish = GameObject.Find ("Clock").GetComponent<DishSelection> ();
	}

	void Update(){
		
		if (gamestate.Instance.GetState() == Enums.state.STATE_INIT) {
			ShowNewDish ();
		}

	}


	public void ShowNewDish(){
		
		canvas.gameObject.SetActive(true);
		gamestate.Instance.SetState (Enums.state.STATE_STATIC_CAMERA);
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
		showSelection = true;
		StartCoroutine(NeWDish());
		clockDish.countDown = true;
		yield return new WaitForSeconds(waitTime);
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
		iconFirst.GetIcon();
		iconSecond.GetIcon();
		iconThird.GetIcon();
		player.enabled = true;
	} 
}
