/* CAMERAMANAGER.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * MOVEMENT OF THE FREE CAMERA USING MOUSE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * ButtonCameraPressed ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 29/03/2016	CODE BASE MATCHED TO Canvas GAMEOBJECT IN Level1MasterChef SCENE
 * ------------------------------------------------------------------------------------------------------------------------------------
 */
using UnityEngine;
using System.Collections;

public enum camType {MAIN,TOP,FREE}; 	//same order as childs of gameobject Cameras in Hieriarchy : first child -> main camera, second -> top camera, etc...

//allow to show struct in ispector
[System.Serializable] 			

public struct cameras{
	public GameObject camera;	
	public camType type;
}

public class CameraManager : MonoBehaviour {

	public Transform camerasTransforms;
	public camType currentCamera;

	[SerializeField] private cameras[] cams; 					// must be readonly property in inspector

	void Awake ()
	{
		GameObject[] c = new GameObject[camerasTransforms.childCount];	//get childs of cameras gameobject
		c = HelperMethods.GetChildren (camerasTransforms);
		cams = new cameras[camerasTransforms.childCount];				//set the number of cameras in scene
		for (int i = 0; i<c.Length; ++i)
		{
			cams[i].camera = c [i];								//camType ordered as in inspector. we can get easily the type of camera
			cams [i].type = (camType) i;

			if (cams [i].type != camType.MAIN)					// set the main camera
				cams [i].camera.SetActive (false);
			else
				cams [i].camera.SetActive (true);
		}
		currentCamera = camType.MAIN;							//control the current camera
		Enums.sceneLevel level = gamestate.Instance.GetLevel();
		AudioManager.Instance.LoadAudioFiles ();
	}
	
	public void ButtonCameraPressed () {
		
		currentCamera = (camType) (((int) currentCamera + 1) % cams.Length);
		for (int i = 0; i<cams.Length; ++i)
		{
			if (cams [i].type != currentCamera)
				cams [i].camera.SetActive (false);
			else
				cams [i].camera.SetActive (true);
		}
		GetComponent<Canvas> ().worldCamera = Camera.main;

		AudioManager.Instance.PlayMusic (gamestate.Instance.GetLevel());
	}
}
