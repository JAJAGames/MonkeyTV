/* PANELDEBUG.CS
 * (C) COPYRIGHT "JAJA GAMES", 2.016
 * ------------------------------------------------------------------------------------------------------------------------------------
 * EXPLANATION: 
 * MOVEMENT OF THE FREE CAMERA USING MOUSE
 * ------------------------------------------------------------------------------------------------------------------------------------
 * FUNCTIONS LIST:
 * 
 * Awake ()
 * Start ()
 * Update ()
 * ButtonCollidersPressed ()
 * ButtonWireframePressed ()
 * ButtonBackPressed ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 28/03/2016	CODE BASE MATCHED TO Canvas GAMEOBJECT IN Level1MasterChef SCENE
 * ------------------------------------------------------------------------------------------------------------------------------------
 */
using UnityEngine;
using System.Collections;

public class panelDebug : MonoBehaviour {

	// Use this for initialization
	[Header("Scenery Renders")]
	public Transform NivelMCS01;
	private GameObject [] rendersMCS01;

	public Transform NivelMCS02;
	private  GameObject[] rendersMCS02;

	public Transform NivelMCS03;
	private  GameObject[] rendersMCS03;

	[Tooltip("Colliders")]
	public Transform colliders;
	private  GameObject[] colliderRenders;

	[Header("Panel Debug")]
	public RectTransform _panelDebug; 
	public float smoothMovement = 20.0f; 
	private float currentPosition;
	private float maxPosition;
	private float minPosition;
	private float initPosition;
	private bool togglePanel;

	[Header("Shader")]
	public Shader wireframe;
	Shader transparent;

	public GameStats statsText;

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
		
	void Start(){
		
		togglePanel = false;
		maxPosition = _panelDebug.rect.position.x;
		minPosition = currentPosition - _panelDebug.rect.width;
		_panelDebug.position = new Vector3 (minPosition,0,0);
	}

	void Update()
	{

		if (Input.GetKeyUp (KeyCode.P) && !togglePanel) {
			togglePanel = true;
			statsText.active = !statsText.active; 
			initPosition = _panelDebug.position.x;
			currentPosition = initPosition;
		}

		if (togglePanel) 
		{
			if (initPosition == minPosition) {

				currentPosition += smoothMovement;
				_panelDebug.position = new Vector3 (currentPosition,0,0);
				if (currentPosition - maxPosition > - smoothMovement ) 
					togglePanel = false;
			} 
			else 
			{
				currentPosition -= smoothMovement;
				_panelDebug.position = new Vector3 (currentPosition,0,0);
				if (currentPosition - minPosition < smoothMovement) 
					togglePanel = false;
			}

		}
	}

	public void ButtonCollidersPressed () 
	{
		//show & hide renders
		for (int i = 0; i < rendersMCS01.Length; i++)
			rendersMCS01 [i].SetActive (!rendersMCS01 [i].activeSelf);
		for (int i = 0; i < rendersMCS02.Length; i++)
			rendersMCS02 [i].SetActive (!rendersMCS02 [i].activeSelf);
		for (int i = 0; i < rendersMCS03.Length; i++)
			rendersMCS03 [i].SetActive (!rendersMCS03 [i].activeSelf);
		for (int i = 0; i < colliderRenders.Length; i++) 
			colliderRenders[i].GetComponent<MeshRenderer> ().enabled = !colliderRenders[i].GetComponent<MeshRenderer> ().enabled;

	}

	public void ButtonWireframePressed () 
	{
		//toggle shader
		if (colliderRenders [0].GetComponent<MeshRenderer> ().material.shader == wireframe) 
		{
			for (int i = 0; i < colliderRenders.Length; i++) 
				colliderRenders [i].GetComponent<MeshRenderer> ().material.shader = Shader.Find ("Transparent/Diffuse");
		}else
		{
			for (int i = 0; i < colliderRenders.Length; i++) 
				colliderRenders [i].GetComponent<MeshRenderer> ().material.shader = wireframe;
		}

	}

	public void ButtonBackPressed () 
	{
		statsText.active = !statsText.active;
		togglePanel = true;
		initPosition = _panelDebug.position.x;
		currentPosition = initPosition;
	}
}
