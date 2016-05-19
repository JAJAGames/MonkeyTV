using UnityEngine;
using System.Collections;

public class BouncingItems : MonoBehaviour {

	public float amplitude;          //Set in Inspector 
	public float speed;              //Set in Inspector 
	private float tempVal;
	private Vector3 tempPos;
	MeshRenderer mesh;
	void Awake () 
	{
		tempVal = transform.position.y;
		speed = speed * Random.Range (0.8f, 1.2f);
		mesh = GetComponent<MeshRenderer> ();
	}

	void Update () 
	{        
		if (!mesh.enabled)
			return;
		
		tempPos = transform.position;
		tempPos.y = 2 * tempVal + amplitude * Mathf.Sin(speed * Time.time);
		transform.position = tempPos;
	}

}
