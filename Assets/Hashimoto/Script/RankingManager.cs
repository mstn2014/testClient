using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour {

	getRequestAndroid getreq;
	RankingData m_RankData;

	bool	m_move_flg;
	public GameObject ranker;
	public GameObject higerranker;
	Vector3 scaleNum;
	int		m_rankPosY;
	Vector3 BaseLine,MaxLine;

	// 定数呼び出し
	RankingSetting	RANKING; 

	public bool IsMove{
		get{return m_move_flg;}
		set{m_move_flg = value;}
	}
	// Use this for initialization
	void Start () {
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");

		getreq = GameObject.Find("/getRequestObject").GetComponent<getRequestAndroid>();
		GameObject obj = GameObject.Find ("RankingData");
		m_RankData = obj.GetComponent<RankingData>();

		m_rankPosY = RANKING.MoziInitPosY;
		scaleNum = new Vector3(RANKING.MoziScall,RANKING.MoziScall,RANKING.MoziScall);	
		m_move_flg = false;
	}

	// Update is called once per frame
	void Update () {}

	// ランキング追加
	public void DataSet(int num,getRequestAndroid.data_android toShow){
		m_RankData.AddRanking (toShow);

		int rankNum = m_RankData.IsRankingNum;
		RankingObjectGeneration(toShow,rankNum);
		m_RankData.IsRankingNum = ++num;
	}

	// ランキング表示用の名前生成.
	private void RankingObjectGeneration(getRequestAndroid.data_android toShow,int num){
		GameObject ranking;
		if (num < 10) {
			ranking = (GameObject)GameObject.Instantiate (higerranker, this.transform.position, this.transform.rotation);
			ranking.GetComponent<HigherRankDisplay> ().Initialize (num);
		} else {
			ranking = (GameObject)GameObject.Instantiate (ranker, this.transform.position, this.transform.rotation);
			ranking.GetComponent<RankingDisplay> ().Initialize (num);
		}
		ranking.transform.parent = this.transform;
		ranking.transform.localScale = scaleNum;
		ranking.transform.localPosition = new Vector3(RANKING.MoziInitPosX,m_rankPosY,(float)0.0f);
		m_rankPosY = m_rankPosY - RANKING.MoziInterval;
	}
	
	public void ChangeScene(int num){
		int rank_num = m_RankData.IsRankingNum;
		if((rank_num-1) == num){
			Application.LoadLevel("RankingAvatarTown");
		}
	}


	public void StartMove(int num){
		int rank_num = m_RankData.IsRankingNum;
		if((rank_num-1) == num){
			m_move_flg = true;
		}
	}

}
