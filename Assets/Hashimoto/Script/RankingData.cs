using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingData : MonoBehaviour {
	
	static List<getRequestAndroid.data_android> containerList;
	static List<int>							rankingNum;
	static int rank_num;	// 開始は1からなのでリストの添え字より1大きい(ランキングの総数)

	// プロパティ
	public int IsRankingNum{	// ランキング者数
		get{return rank_num;}
		set{rank_num = value;}
	}
	// ランキング者データ取得
	public getRequestAndroid.data_android getRankData(int num){
		return containerList [num];
	}
	// 順位の取得
	public int getRankingNum(int num){
		return rankingNum [num];
	}

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad (this);

		rank_num = 0;
		containerList = new List<getRequestAndroid.data_android>();	
		rankingNum = new List<int> ();
	}
	// Update is called once per frame
	void Update () {}

	// データの追加
	public void AddRanking(getRequestAndroid.data_android new_list){
		containerList.Add (new_list);
	}
	// 順位の追加
	public void AddRankingNum(int num){
		rankingNum.Add (num);
	}

	// データの削除
	public void DeleteRanking(int num){
		containerList.RemoveAt (num);
	}
	// 順位の削除
	public void DeleteRankingNum(int num){
		rankingNum.RemoveAt (num);
	}
}
