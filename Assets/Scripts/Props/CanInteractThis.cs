using UnityEngine;
using System.Collections;

public class CanInteractThis : MonoBehaviour {

	public MeshRenderer mesh1, mesh2;
	public SkinnedMeshRenderer skinned;
	private Color color1,color2, colorS;

	public bool showFeedback = true;

	void Awake(){
		if (mesh1)
			color1 = mesh1.material.GetColor("_Color");
		if (mesh2)
			color2 = mesh2.material.GetColor("_Color");
		if (skinned)
			colorS = skinned.material.GetColor("_Color");
	}

	void OnTriggerEnter (Collider other){
		if (other.CompareTag ("Player")) {
			if (mesh1)
				mesh1.material.SetColor ("_Color", Color.white);
			if (mesh2)
				mesh2.material.SetColor ("_Color", Color.white);
			if (skinned)
				skinned.material.SetColor("_Color", Color.white);
		}
	}

	void OnTriggerExit (Collider other){
		if (other.CompareTag ("Player")) {
			if (mesh1)
				mesh1.material.SetColor ("_Color", color1);
			if (mesh2)
				mesh2.material.SetColor ("_Color", color2);
			if (skinned)
				skinned.material.SetColor("_Color", colorS);
		}
	}
}
