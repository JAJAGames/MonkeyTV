/* MENUDROPDOWN.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * UPDATE THE DROPDOWN MENU TO LANUAGE IN USE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * Start()
 * Destroy()
 * myDropdownValueChangedHandler (Dropdown)
 * SetDropdownIndex (int)
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 19/04/2016	CODE BASE MATCHED TO Main Camera OF THE MENU SCENE
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using UnityEngine.UI;

public class MenuDropDown : MonoBehaviour {

	public Dropdown myDropdown;
	private Loading loading;

	void Awake (){
		loading = Camera.main.GetComponent<Loading> ();
	}

	void Start() {
		myDropdown.onValueChanged.AddListener( delegate 
		{ 
			myDropdownValueChangedHandler(myDropdown);	
		});
		myDropdown.value = (int) loading.output - 1;
	}

	void Destroy() {
		myDropdown.onValueChanged.RemoveAllListeners();
	}

	private void myDropdownValueChangedHandler(Dropdown target) {
		loading.output = (TypeOfData) target.value + 1;
	}

	public void SetDropdownIndex(int index) {
		myDropdown.value = index;
	}
}