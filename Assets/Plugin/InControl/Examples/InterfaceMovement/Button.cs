using System;
using UnityEngine;
using InControl;
using UnityEngine.UI;

namespace InterfaceMovement
{
	public class Button : MonoBehaviour
	{
		public Button up = null;
		public Button down = null;
		public bool hasFocus;
		public Color defaultColor;
		public Color onSelectedColor;
		public Color onPressedColor;
		private Image image;

		void Awake()
		{
			image = GetComponent<Image>();
		}


		void Update()
		{
			if (!hasFocus)
				return;
			
			var inputDevice = InputManager.ActiveDevice;
			if (inputDevice.Action1.IsPressed)
				image.color = onPressedColor;
			else {
				image.color = onSelectedColor;
				Debug.Log (gameObject.name);
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
}

