using UnityEngine;
using System.Collections;

public class triggerTamarin : MonoBehaviour {

	public bool moveTo;
	public Transform renderTamarin;
	public Transform destination;
	public float speed = 0.25f;
	public Transform player;
	void FixedUpdate()
	{
		Vector3 looking = player.position;
		looking.y = renderTamarin.position.y;
		renderTamarin.LookAt (looking);	
		if (moveTo) 
		{
			renderTamarin.LookAt (destination);	
			Vector3 direction = destination.position - renderTamarin.position;

			renderTamarin.position += direction.normalized * speed;
			float dist = Vector3.Distance (renderTamarin.position, destination.position);
			if (dist <= 0.2f)
				moveTo = false;
		}
		Debug.DrawLine (renderTamarin.position, renderTamarin.position + renderTamarin.forward.normalized * 2, Color.red);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("Player"))
			moveTo = true;
	}
}
