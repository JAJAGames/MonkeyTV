using UnityEngine;
using System.Collections;
#if UNITY_5_3
using UnityEngine.SceneManagement;
#endif

public class EscapeKey : MonoBehaviour {
	
	private const int MENUID = 0;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
#if UNITY_5_3
			SceneManager.LoadScene(MENUID);
#endif
#if UNITY_5_2
			Application.LoadLevel(MENUID);
#endif
		}
	}
}
