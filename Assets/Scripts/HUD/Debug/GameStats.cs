/* PANELDEBUG.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * MOVEMENT OF THE FREE CAMERA USING MOUSE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * Update ()
 * FramesPerSecond ()
 * PlayerStats ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 29/03/2016	CODE BASE MATCHED TO Stats Text GAMEOBJECT IN Level1MasterChef SCENE. 
 * 				This code Updates the text where are showed the game stats
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameStats : MonoBehaviour {

	public GameObject player;
	public int FramesPerSec { get; protected set; }
	private float frequency = 0.5f;
	public bool active;
	private string textToShow;

	private void Awake() {
		active = false;
	}

	void Update()
	{
		if (!active)
			return;
		StartCoroutine (FramesPerSecond ());
		gameObject.GetComponent<Text> ().text = textToShow;
	}

	IEnumerator FramesPerSecond() {

		int lastFrameCount = Time.frameCount;
		float lastTime = Time.realtimeSinceStartup;
		yield return new WaitForSeconds(frequency);

		float timeSpan = Time.realtimeSinceStartup - lastTime;
		int frameCount = Time.frameCount - lastFrameCount;
		FramesPerSec = Mathf.RoundToInt(frameCount / timeSpan);

		// Display FPS
		textToShow = string.Format ("FPS:\t\t{0:0.0} ms ({1:0.} fps)\n", timeSpan * 1000, FramesPerSec.ToString());

		//Add player Stats to text
		PlayerStats ();
	}

	void PlayerStats()
	{
		// Display Player coordinates
		textToShow += "Player Coordinates:\n\t\t\t";
		textToShow = textToShow + string.Format ("x = " + player.transform.position.x.ToString("F2") + " " +
			"y = " + player.transform.position.y.ToString("F2") + " " +
			"z = " + player.transform.position.z.ToString("F2") + "\n");
		// Player Rotation
		float angle = Quaternion.Angle(Quaternion.Euler(new Vector3(0,0,0)),player.transform.GetChild(0).transform.rotation);
		textToShow += "Player Rotation:\n\t\t\t";
		textToShow += string.Format ("Angle: {0:0.0} ", angle);
	}
}
