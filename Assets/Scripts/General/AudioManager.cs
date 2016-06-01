using UnityEngine;
using System.Collections;
using Enums;


public class AudioManager : MonoBehaviour {

	public Object[] arrayFx;
	public Object[] arrayMusic;
	private static AudioSource _fxSource;
	private AudioSource _musicSource;

	private fxClip _currentFX;

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
				DontDestroyOnLoad(go);
			}	
			return instance;
		}
	}	

	private AudioManager(){
		
		_currentFX = fxClip.NO_FX;
		//_fxSource = gameObject.GetComponent<AudioSource> ();

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
		_musicSource.loop = true;
	}
}
