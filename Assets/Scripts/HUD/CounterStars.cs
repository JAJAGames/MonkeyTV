using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CounterStars : MonoBehaviour {

	public int goals = 0;
	public Color initColor, reachedColor;
	private GameObject[] childs;
	private GameObject player;

	void Awake(){
		childs = HelperMethods.GetChildren (transform);
		EnableDisableChilds (false);
		player = GameObject.Find ("Player");
	}

	void Update(){
		
		if (gamestate.Instance.GetState () != Enums.state.STATE_WIN)
			return;
		
		if (player.GetComponent<PickKey> ().GetMonkeysSaved () == 3 && !childs [1].activeSelf)
			goals++;

		if (!player.GetComponent<PlayerStats> ().jailed && 				!childs [2].activeSelf)
			goals++;
		
		EnableDisableChilds (true);

		
		childs [0].GetComponent<Image> ().color = reachedColor;
		childs [1].GetComponent<Image> ().color = (goals > 0 )? reachedColor : initColor;
		childs [2].GetComponent<Image> ().color = (goals > 1 )? reachedColor : initColor;

	}
	public void AddGoals (){
		goals++;
	}

	public int GetGoals (){
		return goals;
	}

	private void EnableDisableChilds(bool state){
		childs [0].SetActive (state);
		childs [1].SetActive (state);
		childs [2].SetActive (state);
	}
}
