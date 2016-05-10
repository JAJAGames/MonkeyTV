using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dish : MonoBehaviour {

	public Canvas canvas;
	public Image image;
	public Image IGU_Dish;
	public Texture2D texture;

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
		
		if (Input.GetKeyDown (KeyCode.O)) {
			ShowNewDish ();
		}

	}


	void ShowNewDish(){
		
		canvas.gameObject.SetActive(true);
		gamestate.Instance.SetState (Enums.state.STATE_STATIC_CAMERA);
		player.enabled = false;
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
		
		showSelection = true;
		StartCoroutine(NeWDish());
		yield return new WaitForSeconds(waitTime);
		showSelection = false;
		clockDish.SetClock (65);
		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);
		Invoke ("ToSearch", 5.0f);

	}

	void ToSearch(){
		canvas.gameObject.SetActive(false);
		IGU_Dish.gameObject.SetActive(true);
		IGU_Dish.sprite = sprites [dishCode];
		player.enabled = true;
	} 
}