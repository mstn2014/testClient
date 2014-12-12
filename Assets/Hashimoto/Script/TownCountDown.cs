using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownCountDown : MonoBehaviour {

	UILabel timeLabel;

	List<getRequestAndroid.data_android> DataList;
	int rank_num;

	float countdown_number;
		
	int xpos = 200;
	int startNum = 0;
	float nowtime;

	getRequestAndroid.data_android[] dt;
	// Use this for initialization
	void Start () {
		countdown_number = 10.0f;
		timeLabel = GameObject.Find("Timer").GetComponent<UILabel>();

		nowtime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		//countdown_number -= Time.deltaTime;
		//timeLabel.text = ((int)countdown_number).ToString();
		nowtime += Time.deltaTime;
		timeLabel.text = nowtime.ToString ("f1");

		/*
		if(countdown_number <= 0.0f){
			timeLabel.text = "END";
			Application.LoadLevel("RankingAndroid");

			GameObject obj = GameObject.Find("RankingData");
			Destroy(obj);
		}
		*/
	}


}
