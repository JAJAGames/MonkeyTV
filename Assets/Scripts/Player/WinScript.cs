using UnityEngine;
using System.Collections;
#if UNITY_5_3
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
			StartCoroutine (WinTheGame());
		}
	}
	
	private IEnumerator WinTheGame() {
		youWin = true;
		yield return new WaitForSeconds(3f);
#if UNITY_5_3
		SceneManager.LoadScene(MENUID);
#endif
#if UNITY_5_2
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
