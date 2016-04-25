using UnityEngine;
using System.Collections;
#if UNITY_5_3_OR_NEWER
using UnityEngine.SceneManagement;
#endif

public class WinScript : MonoBehaviour {

	private bool youWin;
	private const int MENUID = 0;
	
	// Use this for initialization
	void Start () {
		youWin = false;
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.gameObject.CompareTag ("Player")) {
			StartCoroutine (WinTheGame(other));
		}
	}

	private IEnumerator WinTheGame(Collider player) {
		youWin = true;
		player.gameObject.transform.GetChild (0).GetComponent<Animator> ().SetTrigger("Win");

		yield return new WaitForSeconds(3f);
#if UNITY_5_3_OR_NEWER
		SceneManager.LoadScene(MENUID);
#else
		Application.LoadLevel(MENUID);
#endif
	}
	
	void OnGUI() {
		if (youWin) {
			//Text properties
			int w = Screen.width, h = Screen.height;
			
			GUIStyle style = new GUIStyle ();
			style.alignment = TextAnchor.UpperCenter;
			style.fontSize = h / 10;
			style.fontStyle = FontStyle.Bold;
			style.normal.textColor = Color.green;
			
			Rect rect = new Rect (w / 2 - 50, h / 2 - 25, 100, 50);
			
			GUI.Label (rect, "YOU WIN!", style);
		}
	}
}
