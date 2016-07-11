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


		void Update() {
			// Use last device which provided input.
			var inputDevice = InputManager.ActiveDevice;

			if (gamestate.Instance.GetState () == Enums.state.STATE_MENU) {
				if (inputDevice.Direction.Up.WasPressed || Input.GetKeyDown (KeyCode.UpArrow))
					MoveFocusTo (focusedButton.up);

				if (inputDevice.Direction.Down.WasPressed || Input.GetKeyDown (KeyCode.DownArrow))
					MoveFocusTo (focusedButton.down);

				if (inputDevice.Direction.Left.WasPressed || Input.GetKeyDown (KeyCode.LeftArrow))
					MoveFocusTo (focusedButton.left);

				if (inputDevice.Direction.Right.WasPressed || Input.GetKeyDown (KeyCode.RightArrow))
					MoveFocusTo (focusedButton.right);

				if ((inputDevice.Action1 && inputDevice.Action1.HasChanged) || Input.GetButtonDown ("Submit")) {
					focusedButton.OnClick ();
					MoveFocusTo (focusedButton.pressed);
				}
			}
		}
		
		
		void MoveFocusTo( ButtonMenu newFocusedButton )
		{
			if (newFocusedButton != null)
			{
				AudioManager.Instance.PlayFX (Enums.fxClip.BUTTON_HOVER);
				focusedButton.LeaveFocus ();
				focusedButton = newFocusedButton;
				focusedButton.GetFocus ();
			}
		}


	}
}