using UnityEngine;
using System.Collections;


public class Dish : MonoBehaviour {

	public Texture2D texture;
	public int dishCode;
	private Sprite[] sprites ;
	void Awake(){
		sprites = Resources.LoadAll<Sprite> (texture.name);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.O)) {
			dishCode = NewDish ();
		}
	}

	int NewDish (){
		Debug.Log ("New Dish" +  sprites.Length.ToString());
		return Random.Range(0,sprites.Length);
	}

}
