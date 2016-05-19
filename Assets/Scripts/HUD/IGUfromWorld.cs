using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGUfromWorld : MonoBehaviour {

	[Range (1,3)]
	public float speedAnimation = 1;
	public Transform target;

	private Vector2 HUDPoint;
	private float alpha;
	private Image img;
	private bool animate;


	void Awake () {
		img = GetComponent<Image> ();
		animate = false;
	}

	void Update(){

		if (animate) {

			if (alpha > 0)
				alpha -= Time.deltaTime * speedAnimation;

			else {
				alpha = 0;
				animate = false;
			}
			Vector2 final = HUDPoint * (1 - alpha) + RectTransformUtility.WorldToScreenPoint (Camera.main, target.position) * alpha;

			img.transform.position = final;
		}

	}

	public void StartAnimation(){
		
		HUDPoint = img.rectTransform.position;					//gameobject must be child of canvas and the anchorpoint only can be set on the center.
		alpha = 1f;
		animate = true;
	}
		
}
