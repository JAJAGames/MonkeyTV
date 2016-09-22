using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonBouncing : MonoBehaviour {

	private RectTransform rectTransform;
	// Use this for initialization
	void Awake(){
		rectTransform = GetComponent<RectTransform> ();
	}

	void Update(){
		
		float scale = 0.5f + Mathf.Abs (Mathf.Sin (Time.timeSinceLevelLoad * 3)) / 2;

		Vector3 a = new Vector3 (scale, scale, 1);
		rectTransform.localScale = a;
	}
}
