using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DishClockController : MonoBehaviour {

	private int [] course = new int[3];

	public int currentCourse;
	public float clock = Mathf.Infinity;
	public Text text;
	private Image Disk;
	private float fullFilled;
	public bool countDown;
	public JailManager jail;
	public MessageController message;

	[HideInInspector] public PlayerStats playerStats;

	//Set all coruses before countdown
	void Awake(){
		
		currentCourse = 0;							

		course[0] = (int) Random.Range (0, 2); 		//first course sprites sliced from Fast_Food_Icons 0,1,2
		course[1] = (int) Random.Range (3, 6);		//first course sprites sliced from Fast_Food_Icons 3,4,5,6 
		course[2] = (int) Random.Range (7, 9);		//the rest...

		text.gameObject.SetActive (false);
		fullFilled = 0;
		Disk = GetComponent<Image> ();
		countDown = false;

		playerStats = GameObject.FindWithTag ("Player").GetComponent<PlayerStats> ();
	}

	void Update(){

		if (gamestate.Instance.GetState () == Enums.state.STATE_LOSE)
			return;
		
		if (clock > 0) {
			text.text = string.Format("{00:00}:{1:00}",
									Mathf.Floor(clock / 60),//minutes
									Mathf.Floor(clock) % 60);//seconds
		}

		if (!playerStats.godModeActive() && !playerStats.uniformBonusActive())
			clock -= Time.deltaTime;

		if (fullFilled > 0 && !countDown) {
			Disk.fillAmount = (clock / fullFilled);
			Color color = Color.white - Color.cyan * (1 - Disk.fillAmount);
			color.a = 1;
			Disk.color = color;
		} else {
			Disk.color = Color.white;
			Disk.fillAmount = 1;
		}

		if (countDown && clock < Mathf.Infinity){
			Vector3 a = new Vector3 (-1, 0.9f + (0.1f * Mathf.Sin(clock * 6)),0);
			text.rectTransform.localScale = a;
		} else {
			if (clock == Mathf.Infinity) {
				text.text = "";
			} else {
				Vector3 a = new Vector3 (-1, 1,0);
				text.rectTransform.localScale = a;
			}
		}

		if (clock < 20 && !countDown && !message.gameObject.activeSelf){
			message.gameObject.SetActive (true);
			message.SetTime (clock, true);
		}
		if (clock < 0 && !countDown)
			gamestate.Instance.SetState (Enums.state.STATE_LOSE);
	}

	//index plus 1 but never more than array lenght	
	public void AddCourse (){
		if (currentCourse + 1 == course.Length)
			return;
		currentCourse += 1;
		if (currentCourse == 2)
			jail.SetSecond ();
	}

	public void SetClock(float time){
		clock = time;

		if (time == Mathf.Infinity) {
			text.gameObject.SetActive (false);
			fullFilled = 0;
		} else {
			text.gameObject.SetActive (true);
			fullFilled = time;
		}
	}

	public void addTimeToClock(float time){
		clock += time;
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
