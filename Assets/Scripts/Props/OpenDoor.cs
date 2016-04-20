/* MENUCLICK.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * WHEN PLAYER ENTER IN THE KEY'S AREA OF THE DOOR NMC03_Door, LEFT AND RIGHT DOORS ROTATE ARROUND THE STICK AXIS.  
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * OnTriggerEnter 	(Collider)
 * Update			()
 * void Open(){
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 23/03/2016	CODE BASE MATCHED TO NMC03_Door GAMEOBJECT IN Level1MasterChef SCENE
 * 20/04/2016	Code changend from key object to Root object of prefab.  erased mR variable and added Open Method
 * ------------------------------------------------------------------------------------------------------------------------------------
 */

using UnityEngine;
using System.Collections;

public class OpenDoor : MonoBehaviour {

	//public transforms of the both doors
	public Transform leftDoor;
	public Transform rightDoor;

	//angle rotated, incremet of rotation and door initial state closed
	public float stopAngle = 90;
	private float rotation = 0;
	private bool closed = true;


	public void Open(){
		closed = false;
	}

	//update the angle position and increment till the door is opened
	void Update ()
	{
		if (closed || stopAngle < 0)
			return;

		rotation += Time.deltaTime * 20.0f ;
		if (leftDoor)
			leftDoor.Rotate (rotation * Vector3.up, Space.World);
		if (rightDoor)
			rightDoor.Rotate (rotation * Vector3.down, Space.World);

		stopAngle -= rotation;
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player"))
			closed = false;
	}
}