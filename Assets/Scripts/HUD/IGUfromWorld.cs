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
	}

	void Update(){

		if (animate) {

			if (alpha > 0)
				alpha -= Time.deltaTime * speedAnimation;

			else {
				alpha = 0;
				animate = false;
			}
			Vector3 final =  HUDPoint * (1 - alpha) +  target.position  * alpha;
			float dist =  Vector3.Distance (final, HUDPoint);
			img.transform.localScale = new Vector3 (dist, dist, 0);
			img.transform.position = final;
		}

	}

	public void StartAnimation(){
		
		HUDPoint = img.rectTransform.position;					//gameobject must be child of canvas and the anchorpoint only can be set on the center.
		alpha = 1f;
		animate = true;
	}
		
}
