/* CAMERAFREE.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * MOVEMENT OF THE FREE CAMERA USING MOUSE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Update			()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 29/03/2016	CODE BASE MATCHED TO Free Camera GAMEOBJECT IN Level1MasterChef SCENE
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class CameraFree : MonoBehaviour {

	// Use this for initialization
	const float ANGULARSPEED = 50.0f;
	const float SCROLLSPEED = 10.0f;

	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButton (0) && Input.GetMouseButton (1))	//Input both mouse buttons tranaslate position
		{
			float h = Input.GetAxis ("Mouse Y");
			float v = Input.GetAxis ("Mouse X");
			transform.Translate (v, h, 0);
		} 

		else if (Input.GetMouseButton (1))							//Input right mouse button rotate position
		{
			float v = ANGULARSPEED * Input.GetAxis ("Mouse X");
			float h = ANGULARSPEED * Input.GetAxis ("Mouse Y");
			transform.Rotate(h * Time.deltaTime,  v * Time.deltaTime, 0);
		}
		if (Input.GetAxis ("Mouse ScrollWheel") != 0.0f ) 			//Input from scrooll wheel zoom in/out
		{
			float z =  Input.GetAxis ("Mouse ScrollWheel");
			transform.Translate (0, 0, z * SCROLLSPEED);
		}

	}
}
