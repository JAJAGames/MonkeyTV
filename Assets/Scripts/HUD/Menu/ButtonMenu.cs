using System;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonMenu : MonoBehaviour
{
	public ButtonMenu up = null;
	public ButtonMenu down = null;
	public bool hasFocus;
	public Color defaultColor;
	public Color onSelectedColor;
	public Color onPressedColor;
	private Image image;
	public UnityEvent onClick;

	void Awake()
	{
		image = GetComponent<Image>();
	}


	void Update()
	{
		if (!hasFocus)
			return;
			
		var inputDevice = InputManager.ActiveDevice;
		if (inputDevice.Action1.IsPressed && inputDevice.Action1.HasChanged) {
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
	}

	public void LeaveFocus(){
		hasFocus = false;
		image.color = defaultColor;
	}
		
}
	

