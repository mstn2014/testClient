using UnityEngine;
using System.Collections;

public class AnimationSetting : MonoBehaviour {

	private GameObject	m_model;
	private GameObject	m_Rank;
	private GameObject	m_panel;
	private RankingData	m_RankData;

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
		m_Rank = Resources.Load ("Prefab/Rank") as GameObject;
		m_panel = GameObject.Find ("Panel");
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
		Rank	rankScript;

		length.x = (end_farZ.x - start_nearZ.x);	// 幅(横)
		length.y = (end_farZ.z - start_nearZ.z);	// 幅(縦)
		create_point = 0;							// 生成カウント
		create_round = 0;							// 生成周回数
		camera = Camera.main;

		for (i=0; i<RANKING.MODEL_NUM; i++) {
			// 生成するキャラのランキングを取得
			rank_num = m_RankData.IsRankingNum;
			if (rank_num == 0)	return;
			select_num = Random.Range (0, rank_num);
			work_box = m_RankData.getRankData (select_num);			// データの取り出し
			disp_num = m_RankData.getRankingNum(select_num);		// 順位の取り出し
			m_RankData.DeleteRanking (select_num);					// 取り出したデータの削除
			m_RankData.DeleteRankingNum(select_num);				// 取り出した順位の削除
			m_RankData.IsRankingNum = (rank_num-1);					// 消した分の添え字を下げる


			work_model = Instantiate (m_model) as GameObject;		// モデルとランク表示の生成
			work_rank = NGUITools.AddChild(m_panel, m_Rank);

			////////// モデルの基本設定 //////////
			/*	ラベル関連移動
			label = work_model.transform.FindChild ("Panel").gameObject.transform.FindChild ("Rank").gameObject;
			label.transform.localEulerAngles = labelSlope;
			work_label = label.GetComponent<UILabel> ();

			work_label.text = disp_num.ToString () + "位" + work_box.name;
// ラベルの座標と文字を決める.gameobjectにラベルのobjを渡して個人個人に管理してもらう
			*/
			////// モデルの設定 //////
			// 配置する座標を決める
			work_pos = work_model.transform.localPosition;
			work_pos.x = start_nearZ.x + (length.x/RANKING.BLOCK_NUM*create_point)+
										((length.x/RANKING.BLOCK_NUM)/(RANKING.MODEL_NUM/RANKING.BLOCK_NUM)*create_round);	// ToDo:等間隔に置いてるだけ
			create_point++;
			if(create_point>=RANKING.BLOCK_NUM){
				create_point = 0;
				create_round++;
			}
			work_pos.y = RANKING.MODELPOS_Y;
			work_pos.z = start_nearZ.z + (Random.Range(0f, (length.y)));
			if(i==1)work_pos.z = end_farZ.z;
			work_model.transform.position = work_pos;

			modelScript = work_model.GetComponent<Model>();
			/*
				ToDo:衣装を変えたり
			*/
			// 座標.スクリプト
			////// ラベルの設定 //////
			// 深度に合わせて大きさを変える
			work_rank.transform.localScale = new Vector3(RANKING.RANKSCALL+(RANKING.RANKSCALL*(end_farZ.z-work_pos.z)/ length.y),RANKING.RANKSCALL+(RANKING.RANKSCALL*(end_farZ.z-work_pos.z)/ length.y),RANKING.RANKSCALL+(RANKING.RANKSCALL*(end_farZ.z-work_pos.z)/ length.y));
			// 座標
			work_pos2 = work_model.transform.FindChild("LabelPos").transform.position;
			work_pos2 = camera.WorldToViewportPoint(work_pos2);
			// 深度に合わせて高さを微調整
			work_rank.transform.localPosition = new Vector3(work_pos2.x*1280-1280/2, work_pos2.y*800-880/2+(RANKING.RANKPOS_COM*(work_pos.z-start_nearZ.z)/ length.y), work_pos2.z);
			// 順位
			work_label = work_rank.transform.FindChild("Label").gameObject.GetComponent<UILabel>() as UILabel;
			work_label.text = disp_num.ToString () + "位" + work_box.name;

			rankScript = work_rank.GetComponent<Rank>();
			// スクリプトの起動
			rankScript.Init(work_model, start_nearZ, end_farZ, length);


			// モデルとラベルのgameobjectを交換
		}
	}

}
