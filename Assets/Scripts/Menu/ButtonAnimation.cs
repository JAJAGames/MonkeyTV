﻿using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimation: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public bool isOver = false;
	private Animator anim;

	void Awake(){
		anim = GetComponent<Animator> ();
		anim.SetBool ("OnOver",false);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		AudioManager.Instance.PlayFX  (Enums.fxClip.FX_BUTTON_HOVER);

		anim.SetBool ("OnOver",true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		anim.SetBool ("OnOver",false);
	}
}