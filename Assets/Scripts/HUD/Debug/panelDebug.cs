/* PANELDEBUG.CS
 * (C) COPYRIGHT "BOTIFARRA GAMES", 2.016
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
 * ButtonModeGodPressed ()
 * ------------------------------------------------------------------------------------------------------------------------------------
 * MODIFICATIONS:
 * DATA			DESCRIPCTION	
 * ----------	-----------------------------------------------------------------------------------------------------------------------
 * 28/03/2016	CODE BASE MATCHED TO Canvas GAMEOBJECT IN Level1MasterChef SCENE
 * 19/04/2016	God Mode Added
 * ------------------------------------------------------------------------------------------------------------------------------------
 */
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class panelDebug : MonoBehaviour {

	// Use this for initialization
	[Header("Scenery Renders")]
	public Transform NMC_ALL01;
	private GameObject [] rendersMCS01;

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

	[Header("God Mode")]
	public GameObject textGod;
	public GameObject ShieldSphere;
	private PlayerStats stats;

	[Header("Shader")]
	public Shader wireframe;
	Shader transparent;

	public GameStats statsText;

	void Awake () {
		
		//wallsRenders = new List<GameObject>();
		if (NMC_ALL01)
			rendersMCS01		= HelperMethods.GetChildren(NMC_ALL01);
		
		colliderRenders 	= HelperMethods.GetChildren (colliders);

		//show renders
		if (NMC_ALL01)
			for (int i = 0; i < rendersMCS01.Length; i++)
				rendersMCS01 [i].SetActive (true);
		
		//set wireframe mode and hide colliders
		for (int i = 0; i < colliderRenders.Length; i++) 
			colliderRenders[i].GetComponent<MeshRenderer> ().enabled = false;

		stats = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerStats>();
	}
		
	void Start(){
		
		togglePanel = false;
		maxPosition = _panelDebug.rect.position.x;
		minPosition = currentPosition - _panelDebug.rect.width;
		_panelDebug.position = new Vector3 (minPosition,0,0);
	}

	void Update()
	{
		if (Input.GetKeyUp (KeyCode.G))
				ButtonModeGodPressed();

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
		
	public void ButtonRendersPressed ()
	{
		if (rendersMCS01 [0].activeSelf) {								//show  colliders if renders will disapear
			GameObject.FindGameObjectWithTag ("Player").transform.GetChild (1).gameObject.SetActive (true);
			for (int i = 0; i < colliderRenders.Length; i++)
				colliderRenders [i].GetComponent<MeshRenderer> ().enabled = true;
		} else {
			GameObject.FindGameObjectWithTag ("Player").transform.GetChild (1).gameObject.SetActive (false);
			for (int i = 0; i < colliderRenders.Length; i++)
				colliderRenders [i].GetComponent<MeshRenderer> ().enabled = false;
		}
		//show & hide renders
		for (int i = 0; i < rendersMCS01.Length; i++)
			rendersMCS01 [i].SetActive (!rendersMCS01 [i].activeSelf);
		
	}

	public void ButtonCollidersPressed () 
	{
		if (!rendersMCS01 [0].activeSelf) {								//show  renders if colliders will disapear
			//Player collider
			GameObject.FindGameObjectWithTag ("Player").transform.GetChild (1).gameObject.SetActive (false);
			for (int i = 0; i < rendersMCS01.Length; i++)
				rendersMCS01 [i].SetActive (!rendersMCS01 [i].activeSelf);
		}else
			GameObject.FindGameObjectWithTag ("Player").transform.GetChild (1).gameObject.SetActive (true);
		//show & hide colliders
		for (int i = 0; i < colliderRenders.Length; i++) 
			colliderRenders[i].GetComponent<MeshRenderer> ().enabled = !colliderRenders[i].GetComponent<MeshRenderer> ().enabled;
	}


	public void ButtonModeGodPressed () {
		stats.GOD = !stats.GOD;
		textGod.SetActive (!textGod.activeSelf);
		ShieldSphere.SetActive (!ShieldSphere.activeSelf);
	}

	public void ButtonBackPressed () 
	{
		statsText.active = !statsText.active;
		togglePanel = true;
		initPosition = _panelDebug.position.x;
		currentPosition = initPosition;
	}
}
