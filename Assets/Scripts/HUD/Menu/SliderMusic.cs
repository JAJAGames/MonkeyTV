using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderMusic : MonoBehaviour {

	Slider slider;
	// Use this for initialization
	void Awake () {
		slider = GetComponent<Slider> ();
		slider.value = AudioManager.Instance.GetMusicVolume ();
	}
	
	public void SetVolume (){
		AudioManager.Instance.SetMusicVolume (slider.value);
	}
}
