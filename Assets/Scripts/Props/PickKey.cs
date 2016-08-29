using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class PickKey : MonoBehaviour {

	private bool 			HasKey;
	private GameObject 		key = null;
	public Image			IGU_Key_Background;
	//public 	Image[] 		sprites = new Image[3]; //Falta integrar IGU Micos alliberats
	public GameObject 		particleSystemKey;
	public GameObject[] 	particleMonekys = new GameObject[3];

#if UNITY_EDITOR
	public int 				counter = 0;
#else
	private int				counter = 0;
#endif

	private GameObject 		otherToDestroy;

	void Awake() {
		//for (int i = 0; i < sprites.Length; i++)
		//	sprites [i].color = Color.white;
	}

	void OnTriggerEnter(Collider other) {


		if (other.name == "PRMC_Llave") {
			AudioManager.Instance.PlayFX (Enums.fxClip.FX_PICK_CLOK_KEY);
			//IGU_Key_Background.color = Color.yellow;
			particleSystemKey.SetActive (true);
			HasKey = true;
			key = other.gameObject;
			key.SetActive (false);
		}

		if (other.name == "GRP_PRMC_Jaula" && HasKey) {
			AudioManager.Instance.PlayFX(Enums.fxClip.FX_OPEN_JAIL);
			otherToDestroy = other.gameObject;
			otherToDestroy.transform.GetChild (0).gameObject.SetActive (false);
			otherToDestroy.transform.GetChild (1).gameObject.SetActive (false);
			otherToDestroy.transform.GetChild (2).gameObject.SetActive (false);
			otherToDestroy.transform.GetChild (3).gameObject.SetActive (false);
			otherToDestroy.transform.GetChild (4).gameObject.SetActive (false);
			otherToDestroy.transform.GetChild (5).gameObject.SetActive (true);
			otherToDestroy.transform.GetChild (6).gameObject.SetActive (true);
//			IGU_Key_Background.color = Color.white;
			Invoke ("DestroyOther", 1f);
		
			key.SetActive (true);
			key = null;
			HasKey = false;
			Color newCol;
			if (ColorUtility.TryParseHtmlString ("#FFDB96FF", out newCol))
				particleMonekys [counter].SetActive (true);
				//sprites [counter].color = newCol;
			counter++;
		}
	}

	private void DestroyOther(){
		otherToDestroy.gameObject.SetActive (false);
	}

	public int GetMonkeysSaved(){
		return counter;
	}
}
