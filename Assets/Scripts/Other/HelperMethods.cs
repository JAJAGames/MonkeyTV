using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class HelperMethods
{
	public static GameObject[] GetChildren(Transform go)
	{
		List<GameObject> childrenList = new List<GameObject> ();

		foreach (Transform goChild in go) {
			childrenList.Add (goChild.gameObject);
		}
		return childrenList.ToArray();
	}
}