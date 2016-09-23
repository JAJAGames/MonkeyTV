using UnityEngine;
using System.Collections;

public class BossFoco : MonoBehaviour {

	public BossCamera _BossCamera;
	private Rigidbody _rigidBody;
	private Vector3 _initPos;
	private MeshRenderer _renderer;
	private Circularmovement _focoMoves;
	private Transform _player;
	private Transform jail;
	private GameObject _PlayerFocus;
	[HideInInspector]	public SceneFadeInOut sceneFadeInOut;

	void Awake() {
		_BossCamera = GameObject.FindWithTag ("BossCamera").GetComponent<BossCamera> ();
		_player = GameObject.FindWithTag ("Player").transform;
		sceneFadeInOut = GameObject.Find ("Cameras").GetComponent<SceneFadeInOut> ();
		jail = GameObject.FindWithTag ("Jail").transform;
		_rigidBody = GetComponent<Rigidbody> ();
		_renderer = GetComponent<MeshRenderer> ();
		_focoMoves = transform.parent.GetComponent<Circularmovement> ();
		_PlayerFocus = GameObject.FindWithTag ("Player Focus");
		_initPos = transform.localPosition;
		_renderer.enabled = false;
	}
		
	void Start(){
		_PlayerFocus.SetActive (false);
	}

	// Update is called once per frame
	void Update () {
		if (_BossCamera.IsShaking() && _rigidBody.isKinematic) {
			_renderer.enabled = true;
			_rigidBody.isKinematic = false;
			StartCoroutine (ResetPos());
		}
	}

	IEnumerator ResetPos(){
		yield return new WaitForSeconds (5f);
		_rigidBody.isKinematic = true;
		transform.localPosition = _initPos;
	}

	void OnTriggerEnter(Collider other){
		if (other.gameObject.CompareTag ("Enemy") ) {
			other.GetComponent<EnemySimpleFocus> ().Dead();
			_renderer.enabled = false;
			_focoMoves.target = Enums.FocusTarget.NONE;
		}
		if (other.gameObject.CompareTag ("Player")) {
			StartCoroutine (AttackPlayer());
		}

	}

	IEnumerator AttackPlayer(){
		gamestate.Instance.SetState (Enums.state.STATE_PLAYER_PAUSED);
		_PlayerFocus.SetActive (true);
		_player.GetComponent<Animator> ().SetBool ("Walk", false);

		yield return new WaitForSeconds (1.0f);

		sceneFadeInOut.FadeToBlack ();

		yield return new WaitForSeconds (1.0f);
		_PlayerFocus.SetActive (false);
		_player.position = jail.position;

		gamestate.Instance.SetState (Enums.state.STATE_CAMERA_FOLLOW_PLAYER);

		yield return new WaitForSeconds (0.5f);
		sceneFadeInOut.FadeToClear ();
	}
}
