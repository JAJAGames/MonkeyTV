using UnityEngine;
using System.Collections;

public class TestObject : PoolObject {

	public GameObject target;
	public float speed = 50.1f;

	// Update is called once per frame
	void Update () {
		transform.LookAt (target.transform);
		transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
		//Debug.Log (transform.position.ToString ());
	}

	public override void OnObjectReuse (){
		transform.localScale = Vector3.one;
	}

	void OnTriggerEnter (Collider other) {
		Debug.Log ("Han tocat algo: " + other.GetComponent<Collider>().tag.ToString() );
		if (other.GetComponent<Collider>().tag == "Floor"){
			Debug.Log ("FLOOR");
			//Falta spawnejar a l'enemic

			gameObject.SetActive (false);
		}
	}
}
