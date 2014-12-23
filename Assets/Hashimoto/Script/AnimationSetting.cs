using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimationSetting : MonoBehaviour {

	private GameObject	m_model;
	private GameObject	m_Rank;
	private GameObject	m_panel;
	private TownManager m_townMgr;
	private RankingData	m_RankData;

	private List<GameObject>	ModelS;	// 生成するモデルを格納するためのリスト
	// 定数呼び出し
	RankingSetting	RANKING; 
	
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {}

	//======================================================
	// @brief:キャラの配置＆アニメーション設定.
	//------------------------------------------------------
	// @author:T.Shingaki
	// @param:start_nearX :キャラ設置スタート地点&ニアX	end_farX :キャラ設置終了&ファーX
	// @return:none
	//======================================================
	public void SetAnimation(Vector3 start_nearZ, Vector3 end_farZ){
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		m_model = Resources.Load ("Model/RankModel") as GameObject;
		// 生成するモデルの読み込み
		ModelS = new List<GameObject>();
		ModelS.Add (Resources.Load("Model/RankModel0") as GameObject);
		ModelS.Add (Resources.Load("Model/RankModel1") as GameObject);
		ModelS.Add (Resources.Load("Model/RankModel2") as GameObject);
		ModelS.Add (Resources.Load("Model/RankModel3") as GameObject);
		ModelS.Add (Resources.Load("Model/RankModel4") as GameObject);

		m_Rank = Resources.Load ("Prefab/Rank") as GameObject;
		m_panel = GameObject.Find ("Panel");
		m_townMgr = GetComponent<TownManager> ();
		GameObject obj = GameObject.Find ("RankingData");
		m_RankData = obj.GetComponent<RankingData>();

		getRequestAndroid.data_android work_box;
		int rank_num, select_num, disp_num;	// ランキング数格納用と選ばれたランキング格納用
		GameObject work_model, work_rank;
		Vector2 length;		// xの長さとzの長さを保持	*yがzの代わり
		Vector3 work_pos, work_pos2;
		int 	i, create_point, create_round;
		UILabel work_label;
		Camera	camera;
		Model	modelScript;
		Rank_ver2 infoScript;

		length.x = (end_farZ.x - start_nearZ.x);	// 幅(横)
		length.y = (end_farZ.z - start_nearZ.z);	// 幅(縦)
		create_point = 0;							// 生成カウント
		create_round = 0;							// 生成周回数
		camera = Camera.main;

		for (i=0; i<RANKING.MODEL_NUM; i++) {
			// 生成するキャラのランキングを取得
			rank_num = m_RankData.IsRankingNum;
			if (rank_num == 0)	return;								// データがない場合終了
			select_num = Random.Range (0, rank_num);
			work_box = m_RankData.getRankData (select_num);			// データの取り出し
			disp_num = m_RankData.getRankingNum(select_num);		// 順位の取り出し
			m_RankData.DeleteRanking (select_num);					// 取り出したデータの削除
			m_RankData.DeleteRankingNum(select_num);				// 取り出した順位の削除
			m_RankData.IsRankingNum = (rank_num-1);					// 消した分の添え字を下げる

			// モデルの生成
			work_model = Instantiate (ModelS[(int)work_box.costume]) as GameObject;
			// 生成したモデルの格納
			m_townMgr.AddModel(work_model);

			////// モデルの設定 //////
			// 配置する座標を決める
			work_pos = work_model.transform.localPosition;
			work_pos.x = start_nearZ.x + (length.x/RANKING.BLOCK_NUM*create_point)+
										((length.x/RANKING.BLOCK_NUM)/(RANKING.MODEL_NUM/RANKING.BLOCK_NUM)*create_round);
			create_point++;
			if(create_point>=RANKING.BLOCK_NUM){
				create_point = 0;
				create_round++;
			}
			work_pos.y = RANKING.MODELPOS_Y;
			work_pos.z = start_nearZ.z + (Random.Range(0f, (length.y)));
			work_model.transform.position = work_pos;

			// スクリプトの起動
			modelScript = work_model.GetComponent<Model>();
			infoScript = work_model.transform.FindChild("LabelPos").gameObject.GetComponent<Rank_ver2>();
			infoScript.SetInfo(disp_num.ToString()+"位 "+work_box.name.ToString());
			modelScript.Init(start_nearZ, end_farZ, length);

		}
	}

}
