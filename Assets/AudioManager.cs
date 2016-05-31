using UnityEngine;
using System.Collections;

public enum fxClip {BUTTON_HOVER, BUTTON_PRESSED, NO_FX}

public class AudioManager : MonoBehaviour {

	public AudioClip[] arrayFx;
	public AudioClip[] arrayMusic;

	private AudioSource _fxSource;
	private AudioSource _musicSource;

	private fxClip _currentFX;


	void Awake(){
		_currentFX = fxClip.NO_FX;
		_fxSource = GetComponent<AudioSource> ();
		_musicSource = Camera.main.GetComponent<AudioSource> ();
		_musicSource.loop = true;
		PlayMusic (gamestate.Instance.GetLevel ());
	}

	public void SetFX(fxClip fx){
		_currentFX = fx;
	}

	public void PlayFX ()
	{
		_fxSource.PlayOneShot(arrayFx[(int) _currentFX]);
	}

	public void PlayFX (fxClip fx)
	{
		_currentFX = fx;
		_fxSource.PlayOneShot(arrayFx[(int) _currentFX]);
	}

	public void RemoveFX(){
		_currentFX = fxClip.NO_FX;
	}

	public void PlayMusic (Enums.sceneLevel music)
	{
		_musicSource.clip = arrayMusic [(int)music];
		_musicSource.Play();
	}

	public void StopMusic(){
		_musicSource.Stop();
	}
}
