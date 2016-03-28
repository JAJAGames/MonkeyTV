using UnityEngine;
using System.Collections;


public class buttonColliders : MonoBehaviour {

	// Use this for initialization
	[Header("NivelMCS01")]
	public Transform NivelMCS01;
	private GameObject [] rendersMCS01;

	[Header("NivelMCS02")]
	public Transform NivelMCS02;
	private  GameObject[] rendersMCS02;

	[Header("NivelMCS03")]
	public Transform NivelMCS03;
	private  GameObject[] rendersMCS03;

	[Tooltip("Colliders")]
	public Transform colliders;
	private  GameObject[] colliderRenders;

	[Header("shader")]
	public Shader wireframe;
	Shader transparent;

	void Awake () {
		//wallsRenders = new List<GameObject>();
		rendersMCS01		= HelperMethods.GetChildren(NivelMCS01);
		rendersMCS02 		= HelperMethods.GetChildren(NivelMCS02);
		rendersMCS03 		= HelperMethods.GetChildren(NivelMCS03);
		colliderRenders 	= HelperMethods.GetChildren (colliders);

		//show renders
		for (int i = 0; i < rendersMCS01.Length; i++)
			rendersMCS01 [i].SetActive (true);
		for (int i = 0; i < rendersMCS02.Length; i++)
			rendersMCS02 [i].SetActive (true);
		for (int i = 0; i < rendersMCS03.Length; i++)
			rendersMCS03 [i].SetActive (true);
		
		//set wireframe mode and hide colliders
		for (int i = 0; i < colliderRenders.Length; i++) 
		{
			colliderRenders[i].GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
			colliderRenders[i].GetComponent<MeshRenderer> ().enabled = false;
		}
	}
		

	// Update is called once per frame
	public void ButtonCollidersPressed () 
	{
		//show&hide render
		for (int i = 0; i < rendersMCS01.Length; i++)
			rendersMCS01 [i].SetActive (!rendersMCS01 [i].activeSelf);
		for (int i = 0; i < rendersMCS02.Length; i++)
			rendersMCS02 [i].SetActive (!rendersMCS02 [i].activeSelf);
		for (int i = 0; i < rendersMCS03.Length; i++)
			rendersMCS03 [i].SetActive (!rendersMCS03 [i].activeSelf);
		for (int i = 0; i < colliderRenders.Length; i++) 
			colliderRenders[i].GetComponent<MeshRenderer> ().enabled = !colliderRenders[i].GetComponent<MeshRenderer> ().enabled;

	}
}

		/*
		if (Input.GetKeyUp (KeyCode.F12)) 
		{

			if (furnitureCollider.GetComponent<MeshRenderer> ().material.shader == wireframe) {
				furnitureCollider.GetComponent<MeshRenderer> ().material.shader = Shader.Find("Transparent/Diffuse");
				wallsCollider.GetComponent<MeshRenderer> ().material.shader = Shader.Find("Transparent/Diffuse");
			} 
			else 
			{
				furnitureCollider.GetComponent<MeshRenderer> ().material.shader = wireframe;
				wallsCollider.GetComponent<MeshRenderer> ().material.shader = wireframe;
			}

		}
		*/

