using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Dish : MonoBehaviour {

	public Image image;
	public Image IGU_Dish;
	public Texture2D texture;
	public int dishCode;
	private Sprite[] sprites ;
	private bool selection = false;
	private bool stopSelection = false;

	void Awake(){
		sprites = Resources.LoadAll <Sprite>(@"Images/IGU/"+texture.name) ;
	}

	void Update(){
		
		if (Input.GetKeyDown (KeyCode.O)) {
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
		IGU_Dish.sprite = sprites [dishCode];
	}

}
