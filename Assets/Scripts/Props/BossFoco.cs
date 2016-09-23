using UnityEngine;
using System.Collections;

public class BossFoco : MonoBehaviour {

	public BossCamera _BossCamera;
	private Rigidbody _rigidBody;
	private Vector3 _initPos;
	private MeshRenderer _renderer;
	private Circularmovement _focoMoves;
	// Use this for initialization

	void Awake() {
		//_BossCamera = GameObject.FindWithTag ("BossCamera").GetComponent<BossCamera>();
		_rigidBody = GetComponent<Rigidbody> ();
		_initPos = transform.localPosition;
		_renderer = GetComponent<MeshRenderer> ();
		_renderer.enabled = false;
		_focoMoves = transform.parent.GetComponent<Circularmovement> ();
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
	}
}
