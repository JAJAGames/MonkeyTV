using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PickKey : MonoBehaviour {

	private bool 			HasKey;
	private IGUfromWorld 	ifw;
	private GameObject 		key = null;
	public 	Image[] 		sprites = new Image[3];
	private int counter = 0;

	void Awake(){
		ifw = GameObject.Find ("Image Key").GetComponent<IGUfromWorld>();
		ifw.gameObject.SetActive (false);
		for (int i = 0; i < sprites.Length; i++)
			sprites [i].color = Color.white;
	}

	void OnTriggerEnter(Collider other) {

		if (other.name == "PRMC_Llave") {
			ifw.gameObject.SetActive (true);
			HasKey = true;
			key = other.gameObject;
			key.SetActive (false);
			ifw.StartAnimation ();
		}

		if (other.name == "GRP_PRMC_Jaula" && HasKey) {
			other.gameObject.SetActive (false);
			ifw.friendImage.color = Color.white;
			ifw.gameObject.SetActive (false);
			key.SetActive (true);
			key = null;
			HasKey = false;
			Color newCol;
			if (ColorUtility.TryParseHtmlString("#FFDB96FF", out newCol))
				sprites [counter].color = newCol;
			counter++;
		}
	}
}
