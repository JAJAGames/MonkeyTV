using UnityEngine;
using System.Collections;

public enum fxClip {BUTTON_HOVER, BUTTON_PRESSED, NO_FX}
public enum musicClip {MENU_SCENE, NO_MUSIC}

public class AudioManager : MonoBehaviour {

	public AudioClip[] arrayFx;
	public AudioClip[] arrayMusic;

	private AudioSource _fxSource;
	private AudioSource _musicSource;

	private fxClip _currentFX;
	private musicClip _currentMusic;


	void Awake(){
		_currentMusic = musicClip.MENU_SCENE;
		_currentFX = fxClip.NO_FX;
		_fxSource = GetComponent<AudioSource> ();
		_musicSource = Camera.main.GetComponent<AudioSource> ();
		_musicSource.loop = true;
	}

	void Start(){
		switch (gamestate.Instance.GetLevel ()) {
		case Enums.sceneLevel.MENU:
			_currentMusic = musicClip.MENU_SCENE;
			break;
		case Enums.sceneLevel.LEVEL_1:
			_currentMusic = musicClip.MENU_SCENE;
			break;
		}

		PlayMusic ();
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

	public void SetMusic(musicClip music){
		_currentMusic = music;
	}

	public void PlayMusic ()
	{
		_musicSource.PlayOneShot(arrayMusic[(int) _currentMusic]);
	}

	public void PlayMusic (musicClip Music)
	{
		_currentMusic = musicClip.NO_MUSIC;
		_musicSource.PlayOneShot(arrayMusic[(int) _currentMusic]);
	}

	public void RemoveMusic(){
		_currentMusic = musicClip.NO_MUSIC;
	}
}
