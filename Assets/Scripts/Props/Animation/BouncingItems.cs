using UnityEngine;
using System.Collections;

public class BouncingItems : MonoBehaviour {

	public float amplitude;          //Set in Inspector 
	public float speed;              //Set in Inspector 
	private float tempVal;
	private Vector3 tempPos;
	MeshRenderer mesh;
	private Material _material;
	public Color color;

	void Awake () 
	{
		mesh = GetComponent<MeshRenderer> ();
		tempVal = transform.position.y;
		speed = speed * Random.Range (0.8f, 1.2f);

		_material = mesh.sharedMaterial;
	}

	void Update () 
	{        
		if (!mesh.enabled)
			return;
		
		tempPos = transform.position;
		tempPos.y = 2 * tempVal + amplitude * Mathf.Sin(speed  * Time.time);
		transform.position = tempPos;


		mesh.material.SetColor ("_EmissionColor", (color * Mathf.Abs( Mathf.Cos (Time.time /5 * speed))));
	}

}
