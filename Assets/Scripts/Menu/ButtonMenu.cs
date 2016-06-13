using System;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonMenu : MonoBehaviour
{
	public ButtonMenu up = null;
	public ButtonMenu down = null;

	private bool hasFocus;
	public Color defaultColor;
	public Color onSelectedColor;
	public Color onPressedColor;
	private Image image;
	public UnityEvent onClick;
	private RectTransform trans;
	public bool PlayOnEnable;

	void Awake()
	{
		image = GetComponent<Image>();
		trans = GetComponent<RectTransform> ();
	}

	void OnEnable(){
		if (PlayOnEnable)
			GetFocus();
		else if(hasFocus)
			GetFocus();
	}

	void Update()
	{
		if (!hasFocus)
			return;
			
		var inputDevice = InputManager.ActiveDevice;
		if ((inputDevice.Action1.IsPressed && inputDevice.Action1.HasChanged)||Input.GetButtonDown("Submit")) {
			image.color = onPressedColor;
			onClick.Invoke();
		}
		else {
			image.color = onSelectedColor;
		}
	}

	public void GetFocus(){
		hasFocus = true;
		image.color = onSelectedColor;
		GetComponent<Animator> ().SetTrigger("OnHover");
	}

	public void LeaveFocus(){
		hasFocus = false;
		image.color = defaultColor;
		trans.localScale = Vector3.one;
	}
		
}
	

