using UnityEngine;
using System.Collections;
using Enums;


public class AudioManager : MonoBehaviour {

	public Object[] arrayFx;
	public Object[] arrayMusic;
	private static AudioSource _fxSource;
	private AudioSource _musicSource;

	private fxClip _currentFX;
	private float _musicVolume;
	private static AudioManager instance;

	// Creates an instance of gamestate as a gameobject if an instance does not exist
	public static AudioManager Instance
	{
		get
		{
			if(instance == null)
			{
				GameObject go = new GameObject ("Audio Manager");
				instance = go.AddComponent<AudioManager> ();
				_fxSource = go.AddComponent<AudioSource> ();
				_fxSource.playOnAwake = false;
				_fxSource.loop = false;
				_fxSource.volume = 1;
				DontDestroyOnLoad(go);
			}	
			return instance;
		}
	}	

	private AudioManager(){
		
		_currentFX = fxClip.NO_FX;
		//_fxSource = gameObject.GetComponent<AudioSource> ();

		_musicVolume = 1;
		arrayFx =  Resources.LoadAll("Audio/FX", typeof(AudioClip));
		arrayMusic =  Resources.LoadAll("Audio/Music", typeof(AudioClip));
		PlayMusic (gamestate.Instance.GetLevel ());
	}

	public void SetFX(fxClip fx){
		_currentFX = fx;
	}

	public void PlayFX ()
	{
		_fxSource.PlayOneShot(arrayFx[(int) _currentFX] as AudioClip);
	}

	public void PlayFX (fxClip fx)
	{
		_currentFX = fx;
		_fxSource.PlayOneShot(arrayFx[(int) _currentFX] as AudioClip);
	}

	public void RemoveFX(){
		_currentFX = fxClip.NO_FX;
	}

	public void SetMusic (Enums.sceneLevel music){
		_musicSource.clip = arrayMusic [(int)music] as AudioClip;
	}

	public void PlayMusic ()
	{
		_musicSource.Play();
	}

	public void PlayMusic (Enums.sceneLevel music)
	{
		GetCameraSource ();
		_musicSource.clip = arrayMusic [(int)music] as AudioClip;
		_musicSource.Play();
	}

	public void StopMusic(){
		_musicSource.Stop();
	}

	public void GetCameraSource(){
		_musicSource = Camera.main.GetComponent<AudioSource> ();
		_musicSource.volume = _musicVolume;
		_musicSource.loop = true;
	}

	public void SetFxVolume(float volume){
		_fxSource.volume = volume;
	}

	public float GetFxVolume(){
		return _fxSource.volume;
	}

	public void SetMusicVolume(float volume){
		_musicVolume = volume;
		_musicSource.volume = _musicVolume;
	}

	public float GetMusicVolume(){
		return 	_musicVolume;
	}

	public bool MusicIsPlaying(){
		return _musicSource.isPlaying;
	}

	public bool FxIsPlaying(){
		return _fxSource.isPlaying;
	}
}
