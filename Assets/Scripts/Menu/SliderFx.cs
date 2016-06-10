using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderFx : MonoBehaviour {

	Slider slider;
	// Use this for initialization
	void Awake () {
		slider = GetComponent<Slider> ();
		slider.value = AudioManager.Instance.GetFxVolume ();
	}

	public void SetVolume (){
		AudioManager.Instance.SetFxVolume (slider.value);
		if (!AudioManager.Instance.FxIsPlaying())
			AudioManager.Instance.PlayFX (Enums.fxClip.BUTTON_HOVER);
	}
}
