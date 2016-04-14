using UnityEngine;
using System.Collections;

public class Targetting : MonoBehaviour {

    public Transform player;
	// Update is called once per frame
	void Update () {
        Vector3 lookAt = player.position;
        lookAt.y = transform.position.y;
        transform.LookAt(lookAt);
	}
}
