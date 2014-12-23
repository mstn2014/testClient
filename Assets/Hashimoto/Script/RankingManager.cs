using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour {

	getRequestAndroid getreq;
	RankingData m_RankData;
	FadeMgr		m_fade;

	bool	m_move_flg;
	private GameObject ranker;
	private GameObject higerranker;
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
		ranker = Resources.Load ("Prefab/Ranking") as GameObject;
		higerranker = Resources.Load ("Prefab/HigerRanking") as GameObject;
		m_fade = GameObject.Find("/UI Root (2D)/Camera/Anchor/Panel/Fade").GetComponent<FadeMgr>();
		getreq = GameObject.Find("/getRequestObject").GetComponent<getRequestAndroid>();
		GameObject obj = GameObject.Find ("RankingData");
		m_RankData = obj.GetComponent<RankingData>();

		m_rankPosY = RANKING.MoziInitPosY;
		scaleNum = new Vector3(RANKING.MoziScall,RANKING.MoziScall,RANKING.MoziScall);	
		m_move_flg = false;

		m_fade.Fadein ();
	}

	// Update is called once per frame
	void Update () {
		if (m_fade.IsEndFadeOut) {
			Application.LoadLevel("RankingAvatarTown");		
		}
	}

	// ランキング追加
	public void DataSet(int num,getRequestAndroid.data_android toShow){
		m_RankData.AddRanking (toShow);
		int rankNum = m_RankData.IsRankingNum;

		RankingObjectGeneration(toShow,rankNum);
		m_RankData.IsRankingNum = ++num;
		m_RankData.AddRankingNum (m_RankData.IsRankingNum);
	}

	// ランキング表示用の名前生成.
	private void RankingObjectGeneration(getRequestAndroid.data_android toShow,int num){
		GameObject ranking;
		if (num < 5) {	// 特殊演出をする数分
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
		if((rank_num-1) == num){	// 最後の1人目が消えたら
			m_fade.Fadeout();
		}
	}


	public void StartMove(int num){
		int rank_num = m_RankData.IsRankingNum;
		if((rank_num-1) == num){	// 最後の1人目が生成されたら
			m_move_flg = true;
		}
	}

}
