using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MessageController : MonoBehaviour {

	private float 				countDown = 0f;	
	private  bool 				beep = false;
	private Text				textCountDown;

	void Awake(){
		textCountDown = transform.GetChild(0).GetComponent<Text>();		
	}

	void Update(){
		if (countDown <= 0)
			return;
		textCountDown.text = string.Format("{00:00}:{1:00}",
			Mathf.Floor(countDown / 60),//minutes
			Mathf.Floor(countDown) % 60);//seconds
		countDown -= Time.deltaTime;

		if (beep) {
			AudioManager.Instance.PlayFX (Enums.fxClip.FX_PICK_CLOK_KEY);
			beep = false;
			StartCoroutine (PlayClock ());
		}
	}

	public void SetTime(float t, bool sounds = false){
		countDown = t;
		beep = sounds;
	}

	private IEnumerator PlayClock(){
		yield return new WaitForSeconds (1f);
		beep = true;
	}

	public void SetText (string text){
		textCountDown.text = text;
	}
}
