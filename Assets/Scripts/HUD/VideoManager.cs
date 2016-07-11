using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using InControl;

public class VideoManager : MonoBehaviour {

	private RawImage screen;

	public MovieTexture video;
	private AudioSource audio;

	public Enums.state actualState;
	public Enums.state nextState;

	void Awake() {
		gamestate.Instance.SetState(actualState);

		screen = GetComponent<RawImage> ();
		screen.enabled = true;
		screen.texture = video as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = video.audioClip;

		video.Play ();
		audio.Play ();
	}

	void Update() {
		var inputDevice = InputManager.ActiveDevice;

		if ((inputDevice.AnyButton.WasPressed || Input.anyKey) && video.isPlaying) { 
			StopVideo ();
		}	

		if (!video.isPlaying) {
			gamestate.Instance.SetState (nextState);
			PlaySceneMusic ();
			gameObject.SetActive (false);
		}
	}

	public void StopVideo() {
		video.Stop();
		gamestate.Instance.SetState (nextState);
		PlaySceneMusic ();
	}

	public void PlaySceneMusic() {
		AudioManager.Instance.SetFxVolume (0.5f);
		AudioManager.Instance.SetMusicVolume (0.5f);
		AudioManager.Instance.PlayMusic (gamestate.Instance.GetLevel ());
	}
}