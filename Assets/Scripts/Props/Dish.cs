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
	private bool selection = false;
	private bool stopSelection = false;
	private PlayerMovement player;
	void Awake(){
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
		player = GameObject.Find ("Player").GetComponent<PlayerMovement> ();
	}

	void Update(){
		
		if (Input.GetKeyDown (KeyCode.O)) {
			canvas.gameObject.SetActive(true);
			gamestate.Instance.SetState (Enums.state.STATE_STATIC_CAMERA);
			player.enabled = false;
			selection = true;
			StartCoroutine (StartNewDish (5));
		}
		
		if (selection && !stopSelection) {
			StartCoroutine(NeWDish());
		}

	}

	IEnumerator NeWDish() {

		selection = false;
		yield return new WaitForSeconds(0.25f);
		selection = true;

		if (!stopSelection) {
			dishCode = Random.Range (0, sprites.Length);
			image.sprite = sprites [dishCode];
		}
	}

	IEnumerator StartNewDish (float waitTime){
		stopSelection = false;
		yield return new WaitForSeconds(waitTime);
		stopSelection = true;
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
