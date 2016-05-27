using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PickKey : MonoBehaviour {

	private bool 			HasKey;
	private GameObject 		key = null;
	public Image			IGU_Key_Background;
	public 	Image[] 		sprites = new Image[3];
	public GameObject 		particleSystemKey;
	public GameObject[] 	particleMonekys = new GameObject[3];
	private int counter = 0;

	void Awake(){
		for (int i = 0; i < sprites.Length; i++)
			sprites [i].color = Color.white;
	}

	void OnTriggerEnter(Collider other) {

		if (other.name == "PRMC_Llave") {
			IGU_Key_Background.color = Color.yellow;
			particleSystemKey.SetActive (true);
			HasKey = true;
			key = other.gameObject;
			key.SetActive (false);
		}

		if (other.name == "GRP_PRMC_Jaula" && HasKey) {
			IGU_Key_Background.color = Color.white;
			other.gameObject.SetActive (false);
			key.SetActive (true);
			key = null;
			HasKey = false;
			Color newCol;
			if (ColorUtility.TryParseHtmlString ("#FFDB96FF", out newCol))
				particleMonekys [counter].SetActive (true);
				sprites [counter].color = newCol;
			counter++;
		}
	}
}
