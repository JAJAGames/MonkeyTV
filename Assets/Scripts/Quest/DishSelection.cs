using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DishSelection : MonoBehaviour {

	private int [] course = new int[3];

	public int currentCourse;
	public float clock = Mathf.Infinity;
	public Text text;
	private Image Disk;
	private float fullFilled;
	//Set all coruses before countdown
	void Awake(){
		
		currentCourse = 0;							

		course[0] = (int) Random.Range (0, 2); 		//first course sprites sliced from Fast_Food_Icons 0,1,2
		course[1] = (int) Random.Range (3, 6);		//first course sprites sliced from Fast_Food_Icons 3,4,5,6 
		course[2] = (int) Random.Range (7, 9);		//the rest...

		text.gameObject.SetActive (false);
		fullFilled = 0;
		Disk = GetComponent<Image> ();
	}

	void Update(){

		if (gamestate.Instance.GetState () == Enums.state.STATE_LOOSE)
			return;
		
		if (clock > 0) {
			text.text = string.Format("{0:#0}:{1:00}",
									Mathf.Floor(clock / 60),//minutes
									Mathf.Floor(clock) % 60);//seconds
		}

		clock -= Time.deltaTime;

		if (fullFilled > 0)
			Disk.fillAmount = (clock / fullFilled);
		else
			Disk.fillAmount = 1;
		
		if (clock < 0)
			gamestate.Instance.SetState (Enums.state.STATE_LOOSE);
	}

	//index plus 1 but never more than array lenght	
	public void AddCourse (){
		if (currentCourse + 1 == course.Length)
			return;
		currentCourse += 1;
	}

	public void SetClock(float time){
		clock = time;

		if (time == Mathf.Infinity) {
			text.gameObject.SetActive (false);
			fullFilled = 0;
		} else {
			text.gameObject.SetActive (true);
			fullFilled = time - 5;
		}
	}

	//compare indexes.	
	public bool CompareCourse (int i){
		if (i > course.Length)						//course.Length == 3
			return false;
		else
			return i == currentCourse;				
	}

	//read the current course
	public int GetCurrent(){
		return course[currentCourse];
	}

	public int[] GetCourses(){
		return course;
	}
}
