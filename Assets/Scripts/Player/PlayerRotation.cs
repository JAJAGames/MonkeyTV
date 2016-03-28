using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {

	private int floorMask;
	private float camRayLength = 100f;

	private void Awake ()  {
		floorMask = LayerMask.GetMask ("Floor");
	}

	void Update () {
		// get the mouse point in background plane
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit floorHit;
		Physics.Raycast (camRay, out floorHit, camRayLength, floorMask);

		// look to point
		Vector3 lookTo = floorHit.point;
		lookTo.y = transform.position.y;
		transform.LookAt(lookTo);
	}
}