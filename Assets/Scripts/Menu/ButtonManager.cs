using UnityEngine;
using InControl;


namespace InterfaceMovement
{
	public class ButtonManager : MonoBehaviour
	{
		public ButtonMenu focusedButton;


		void Start()
		{
			TwoAxisInputControl.StateThreshold = 0.7f;
			focusedButton.GetFocus ();
		}


		void Update()
		{
			// Use last device which provided input.
			var inputDevice = InputManager.ActiveDevice;

			// Move focus with directional inputs.
			if (inputDevice.Direction.Up.WasPressed)
				MoveFocusTo( focusedButton.up );
			
			if (inputDevice.Direction.Down.WasPressed) 
				MoveFocusTo( focusedButton.down );
		}
		
		
		void MoveFocusTo( ButtonMenu newFocusedButton )
		{
			if (newFocusedButton != null)
			{
				focusedButton.LeaveFocus ();
				focusedButton = newFocusedButton;
				focusedButton.GetFocus ();
			}
		}
	}
}