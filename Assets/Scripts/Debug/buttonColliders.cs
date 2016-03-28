using UnityEngine;
using System.Collections;


public class buttonColliders : MonoBehaviour {

	// Use this for initialization
	[Header("walls")]
	[Tooltip("Parent of walls renders")]

	public Transform walls;
	private GameObject [] wallsRenders;
	[Tooltip("Collider of walls")]
	public GameObject wallsCollider;

	[Header("furniture")]
	public Transform furniture;
	private  GameObject[] furnitureRenders;
	public GameObject furnitureCollider;

	[Header("ground")]
	public GameObject groundRender;
	public GameObject groundCollider;
	[Header ("background")]
	public GameObject background;
	[Header("shader")]
	public Shader wireframe;
	Shader transparent;

	void Awake () {
		//wallsRenders = new List<GameObject>();
		wallsRenders		= HelperMethods.GetChildren(walls);
		furnitureRenders 	= HelperMethods.GetChildren(furniture);

		//show renders
		groundRender.SetActive (true);
		background.SetActive (true);
		for (int i = 0; i < wallsRenders.Length; i++)
			wallsRenders [i].SetActive (true);
		for (int i = 0; i < furnitureRenders.Length; i++)
			furnitureRenders [i].SetActive (true);

		//hide colliders
		groundCollider.GetComponent<MeshRenderer> ().enabled = false;
		wallsCollider.GetComponent<MeshRenderer>().enabled = false;
		furnitureCollider.GetComponent<MeshRenderer>().enabled = false;

		//set wireframe mode
		furnitureCollider.GetComponent<MeshRenderer> ().material.shader = Shader.Find("Transparent/Diffuse");
		wallsCollider.GetComponent<MeshRenderer> ().material.shader = Shader.Find("Transparent/Diffuse");
	}
		

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp(KeyCode.F11))
		{
			//show&hide render
			groundRender.SetActive (!groundRender.activeSelf);
			for (int i = 0; i < wallsRenders.Length; i++)
				wallsRenders [i].SetActive (!wallsRenders [i].activeSelf);
			for (int i = 0; i < furnitureRenders.Length; i++)
				furnitureRenders [i].SetActive (!furnitureRenders [i].activeSelf);

			//show&hide colliders
			groundCollider.GetComponent<MeshRenderer> ().enabled = !groundCollider.GetComponent<MeshRenderer> ().enabled;
			wallsCollider.GetComponent<MeshRenderer>().enabled = !wallsCollider.GetComponent<MeshRenderer>().enabled;
			furnitureCollider.GetComponent<MeshRenderer>().enabled = !furnitureCollider.GetComponent<MeshRenderer>().enabled;
			background.SetActive (!background.activeSelf);
		}

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

	}
}