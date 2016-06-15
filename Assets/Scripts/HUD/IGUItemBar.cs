using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class IGUItemBar : MonoBehaviour {

	[SerializeField]
	public Image itemBar;

	private float[] startPosition;

	// Use this for initialization
	void Awake() {
		startPosition = new float[4];
		startPosition [0] = itemBar.rectTransform.localPosition.y;
		startPosition [1] = startPosition [0] + 71;
		startPosition [2] = startPosition [1] + 71;
		startPosition [3] = startPosition [2] + 100;
	}
	
	public void ChangeItemBarSize(int itemsLeft) {
		Vector3 temp = itemBar.rectTransform.localPosition;
		temp.y = startPosition [3 - itemsLeft];
		itemBar.rectTransform.localPosition = temp;
	}
}
