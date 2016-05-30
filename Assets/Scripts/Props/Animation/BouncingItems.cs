using UnityEngine;
using System.Collections;

public class BouncingItems : MonoBehaviour {

	public float amplitude;          //Set in Inspector 
	public float speed;              //Set in Inspector 
	private float tempVal;
	private Vector3 tempPos;
	MeshRenderer mesh;
	public bool lightningColor;
	public Color color;

	void Awake () 
	{
		mesh = GetComponent<MeshRenderer> ();
		tempVal = transform.position.y;
		speed = speed * Random.Range (0.8f, 1.2f);

	}

	void Update () 
	{        
		if (!mesh.enabled)
			return;
		
		tempPos = transform.position;
		tempPos.y = tempVal + amplitude * Mathf.Cos(speed  * Time.time);
		transform.position = tempPos;

		if (lightningColor)
			mesh.material.SetColor ("_EmissionColor", (color * Mathf.Abs( Mathf.Cos (Time.time /5 * speed))));
	}

}
