using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGUfromWorld : MonoBehaviour {

	[Range (0,3)]
	public float speedAnimation = 1f;
	public Transform target;

	private Vector3 HUDPoint;
	private float alpha;
	private Image img;
	private bool animate;

	void Awake () {
		img = GetComponent<Image> ();
		animate = false;
		Vector3 tmpPos = img.rectTransform.localPosition;
		img.rectTransform.anchorMin = Vector2.zero;
		img.rectTransform.anchorMax = Vector2.zero;
		tmpPos.y = transform.parent.GetComponent<Canvas> ().pixelRect.height + tmpPos.y;
		img.rectTransform.localPosition = tmpPos;
	}

	void Update(){

		if (animate) {

			if (alpha > 0)
				alpha -= Time.deltaTime * speedAnimation;
			else {
				alpha = 0;
				animate = false;
			}
			Vector3 final = HUDPoint * (1 - alpha) + new Vector3 (Camera.main.WorldToScreenPoint (target.position).x, Camera.main.WorldToScreenPoint (target.position).y, 0) * alpha;
			img.rectTransform.anchoredPosition = final;
		} 
	}

	public void StartAnimation(){

		HUDPoint = img.rectTransform.anchoredPosition;					//gameobject must be child of canvas and the anchorpoint only can be set on the center.
		alpha = 1f;
		animate = true;
	}
		
}
