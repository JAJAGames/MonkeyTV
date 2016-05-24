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

	private RectTransform _panelDebug; 
	private bool togglePanel;
	private Vector3 showPos, hidePos;
	[Header("God Mode")]
	public GameObject textGod;
	public GameObject torus;
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
		_panelDebug = GameObject.Find ("Dynamic").GetComponent<RectTransform>();
		stats = GameObject.Find("Player").GetComponent<PlayerStats>();
	}
		
	void Start(){
		togglePanel = false;
		hidePos = _panelDebug.localPosition;
		showPos = _panelDebug.localPosition + new Vector3 (-400, 0, 0);
	}

	void Update()
	{
		if (Input.GetKeyUp (KeyCode.G))
				ButtonModeGodPressed();

		if (Input.GetKeyUp (KeyCode.P) && !togglePanel) {
			togglePanel = true;
			Cursor.visible = !Cursor.visible;
			statsText.active = !statsText.active; 

		}

		if (Input.GetKeyUp (KeyCode.C))
			Cursor.visible = !Cursor.visible;

		if (togglePanel) {
			if (statsText.active) {
				_panelDebug.localPosition = showPos;
			}else
				_panelDebug.localPosition = hidePos;
			togglePanel = false;
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
		textGod.SetActive (!textGod.activeSelf);
		torus.SetActive (!torus.activeSelf);
	}

	public void ButtonBackPressed () 
	{
		statsText.active = !statsText.active;
		togglePanel = true;
		Cursor.visible = !Cursor.visible;
	}
}
