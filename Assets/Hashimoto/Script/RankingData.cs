using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingData : MonoBehaviour {
	
	static List<getRequestAndroid.data_android> containerList;
	static int rank_num;

	// プロパティ
	public int IsRankingNum{	// ランキング者数
		get{return rank_num;}
		set{rank_num = value;}
	}
	// ランキング者データ
	public getRequestAndroid.data_android getRankData(int num){
		return containerList [num];
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);

		rank_num = 0;
		containerList = new List<getRequestAndroid.data_android>();														
	}
	// Update is called once per frame
	void Update () {}

	// データの追加
	public void AddRanking(getRequestAndroid.data_android new_list){
		containerList.Add (new_list);
	}
}
