using UnityEngine;
using System.Collections;

public class FloorPosition : MonoBehaviour {


	public Transform player;
	private Vector3 initDiference;
	private CharacterController cC;
	void Awake () {
		initDiference = transform.position - player.position;
		cC = player.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		Vector3 currentDifference = player.position + initDiference;
		if (cC.isGrounded)
			currentDifference.y = transform.position.y;
		
		transform.position = currentDifference;

	}
}
