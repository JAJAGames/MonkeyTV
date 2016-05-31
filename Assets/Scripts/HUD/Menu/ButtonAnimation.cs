using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool isOver = false;
	private Animator anim;
	private AudioManager audio;

	void Awake(){
		anim = GetComponent<Animator> ();
		anim.SetBool ("OnOver",false);
		audio = GameObject.Find ("Audio Manager").GetComponent<AudioManager> ();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		audio.PlayFX (fxClip.BUTTON_HOVER);
		anim.SetBool ("OnOver",true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		anim.SetBool ("OnOver",false);
	}
}