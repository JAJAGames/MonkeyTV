using UnityEngine;
using System.Collections;

public class AddToPool : MonoBehaviour {

	[Header ("Prefab to Add to Pool")]
	public GameObject prefab;
	[Header ("Place to instanciate")]
	public GameObject startPoint;

	[Header ("Number of instances")]
	public int amount;

	void Start(){
		PoolManager.instance.CreatePool (prefab, amount);
	}
}
