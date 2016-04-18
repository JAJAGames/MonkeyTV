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