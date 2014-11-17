using UnityEngine;
using System.Collections;

public class TownCountDown : MonoBehaviour {

	UILabel timeLabel;

	float countdown_number;
	// Use this for initialization
	void Start () {
		countdown_number = 10.0f;
		timeLabel = GameObject.Find("/UI Root (2D)/Camera/Anchor/Panel/Label").GetComponent<UILabel>();
	}
	
	// Update is called once per frame
	void Update () {
		countdown_number -= Time.deltaTime;
		timeLabel.text = ((int)countdown_number).ToString();
		if(countdown_number <= 0.0f){
			timeLabel.text = "END";
			Application.LoadLevel("RankingAndroid");
		}
	}
}
