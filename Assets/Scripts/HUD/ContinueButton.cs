using UnityEngine;
using System.Collections;
using InControl;
using UnityEngine.UI;

public class ContinueButton : MonoBehaviour {

	private RectTransform rectTransform;
	public GameObject Items;
	private GameObject[] fruits;

	void Awake(){
		fruits = HelperMethods.GetChildren (Items.transform);
	}

	void OnEnable(){
		for (int i = 0; i < fruits.Length; ++i)
			fruits [i].GetComponent<PropPickItem> ().ShowItem();

		Items.SetActive (false);
	}

	public void SetActiveItems(){

		Items.SetActive (true);
		gameObject.SetActive (false);

	}
}
