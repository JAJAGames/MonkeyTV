using System;
using UnityEngine;
using InControl;
using UnityEngine.UI;
using UnityEngine.Events;

public class ButtonMenu : MonoBehaviour
{
	public ButtonMenu up = null;
	public ButtonMenu down = null;
	public ButtonMenu left = null;
	public ButtonMenu right = null;
	public ButtonMenu	pressed = null;

	private bool hasFocus;
	public Color defaultColor;
	public Color onSelectedColor;
	public Color onPressedColor;
	private Image image;
	public UnityEvent onClick;
	//private RectTransform trans;
	public bool PlayOnEnable;
	private Image childBackGround = null;

	void Awake()
	{
		image = GetComponent<Image>();
//		trans = GetComponent<RectTransform> ();

		if (transform.FindChild("Image")!=null)
			childBackGround = transform.FindChild("Image").GetComponent<Image>();

		SetColor (defaultColor);
	}
		

	void Update()
	{
		if (!hasFocus)
			return;
			
		var inputDevice = InputManager.ActiveDevice;
	}

	public void GetFocus(){
		hasFocus = true;
		SetColor(onSelectedColor);
		if (GetComponent<Animator>())
			GetComponent<Animator> ().SetTrigger("OnEnter");
	}

	public void LeaveFocus(){
		hasFocus = false;
		SetColor(defaultColor);
		if (GetComponent<Animator>())
			GetComponent<Animator> ().SetTrigger("OnExit");
	}

	private void SetColor(Color c){
		
		if (childBackGround != null)
			childBackGround.GetComponent<Image> ().color = c;
		else
			image.color = c;
	}

	public void OnClick(){
		SetColor (onPressedColor);
		onClick.Invoke();
	}
		
}
	

