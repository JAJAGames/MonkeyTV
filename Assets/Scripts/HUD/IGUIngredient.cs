using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

public class IGUIngredient : MonoBehaviour {


	public uint counter;
	public float speedAnimation;
	public Transform target;
	private Image img;
	private Image checkOk;
	public float alpha;
	public Vector2 screenPoint;
	public Vector2 HUDPoint;
	private bool animate;


	void Awake () {
		img = GetComponent<Image> ();
		checkOk = transform.GetChild (0).GetComponent<Image> ();
		SetVisible (true);
		SetCheckVisible (false);
		animate = false;
		HUDPoint = img.rectTransform.localPosition;					//gameobject must be child of canvas and the anchorpoint only can be set on the center.
	}
		

	public void Update(){

		if (Input.GetKeyDown (KeyCode.L))
			GetHUDPoint ();
		if (animate) {
			
			if (alpha > 0)
				alpha -= Time.deltaTime * speedAnimation;
			
			else {
				alpha = 0;
				animate = false;
			}
			Vector2 final = HUDPoint * (1 - alpha) + RectTransformUtility.WorldToScreenPoint (Camera.main, target.position) * alpha;
			//Vector2 sinfinal = HUDPoint * Mathf.Sin(1 - alpha) + RectTransformUtility.WorldToScreenPoint (Camera.main, target.position) * Mathf.Sin(alpha);

			img.transform.position = final;
		}

	}

	private void GetHUDPoint(){
		alpha = 1f;
		animate = true;
	}

	public void SetVisible (bool visible){
		Color color = Color.white;
		color.a = Convert.ToUInt16 (visible);
		img.color = color;
	}

	public void SetCheckVisible (bool visible){
		Color color = Color.white;
		color.a = Convert.ToUInt16 (visible);
		checkOk.color = color;
	}
}
