using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour {
	public Image FadeImg;
	public float fadeSpeed = 0.5f;
	private float t = 0;
	private bool clearScene;

	void Start(){
		FadeImg.rectTransform.localScale = new Vector2(Screen.width, Screen.height);
		clearScene = true;
	}

	void Update() {
		if (clearScene)
			FadeImg.color = Color.Lerp(FadeImg.color, Color.clear, t);
		else
			FadeImg.color = Color.Lerp(FadeImg.color, Color.black, t);

		if (t < 1)
			t += Time.deltaTime / fadeSpeed;
	}

	public void FadeToClear() {
		// Lerp the colour of the image between itself and transparent.
		clearScene = true;
		t = 0;
	}


	public void FadeToBlack() {
		// Lerp the colour of the image between itself and black.
		clearScene = false;
		t = 0;
	}
}