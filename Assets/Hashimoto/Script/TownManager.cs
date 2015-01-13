using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TownManager : MonoBehaviour {
	
	private Camera		m_camera;
	private int			m_cameraNowPoint;	// カメラの現在のポイント
	private GameObject	m_ground;
	private Vector3		m_startPos_nearZ;	// カメラのスタート地点(X座標)とモデル配置のニア(Z座標)
	private Vector3		m_endPos_farZ;		// カメラのエンド地点(X座標)とモデルの配置のファー(Z座標)
	private Vector2		length;				// 長さ
	private float			m_townTime;			// 町の時間
	private float			m_nowcount;			// 現在の時間

	private FadeMgr		m_fade;				// フェードインフェードアウト

	private AnimationSetting m_setAnim;		// キャラの配置&アニメーション設定
	private List<GameObject> MODELS;		// 生成したモデルを格納
	private int				 model_num;		// モデルの数

	private List<Model> DANCE;				// ダンスするモデルのスクリプト格納
	private int			dance_num;			// ダンスしている人数
	private bool		m_danceFlg;			// みんなでダンスのフラグ

	// 定数呼び出し
	RankingSetting	RANKING; 

	// Use this for initialization
	void Start () {
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		m_fade = GameObject.Find("/UI Root (2D)/Camera/Anchor/Panel/Fade").GetComponent<FadeMgr>();
		m_camera = Camera.main;
		m_ground = GameObject.Find ("Ground").gameObject;
		m_setAnim = GetComponent<AnimationSetting>();

		//////////////////// ToDo: カメラをスクロールではなく首ふりで
		// カメラをMOVEPOINT0へ
		m_startPos_nearZ	= m_ground.transform.FindChild ("StartingPoint_nearZ").transform.position;
		m_endPos_farZ	= m_ground.transform.FindChild ("EndPoint_farZ").transform.position;
		//Vector3 work_vec = m_camera.transform.position;
		//work_vec.x = m_startPos_nearZ.x + (Mathf.Abs(m_endPos_farZ.x - m_startPos_nearZ.x)/2);
		m_camera.transform.position = RANKING.CAMERAMOVELIST[0].pos;
		m_camera.transform.eulerAngles = RANKING.CAMERAMOVELIST [0].angle;
		m_cameraNowPoint = 0;

		// 時間をfpsに変更
		m_townTime = 0.0f;	// 60fpsをかける
		if (RANKING.TOWN_SECOND <= 0)		Debug.Log ("時間が正しくありません");
		m_nowcount = 0.0f;
		// 移動できる範囲を求める
		length.x = m_endPos_farZ.x - m_startPos_nearZ.x;
		length.y = m_endPos_farZ.z - m_startPos_nearZ.z;

		MODELS = new List<GameObject>();
		model_num = 0;
		// モデルの生成
		m_setAnim.SetAnimation (m_startPos_nearZ, m_endPos_farZ);

		DANCE = new List<Model>();
		m_danceFlg = false;

		m_fade.Fadein ();
	}
	
	// Update is called once per frame
	void Update () {
		//////////////////// ToDo: カメラをスクロールではなく首ふりで
		// 一定時間ごとにカメラを移動
		// count=0のときも移動してしまう
		if ((m_nowcount >= (RANKING.TOWN_SECOND / RANKING.CAMERAMOVECOUNT)) ){
			int rand = Random.Range(0, RANKING.CAMERAMOVEPOINTNUM);
			while(m_cameraNowPoint == rand){	// 被り防止
				rand = Random.Range(0, RANKING.CAMERAMOVEPOINTNUM);
			}
			iTween.MoveTo(m_camera.gameObject, iTween.Hash("x",RANKING.CAMERAMOVELIST[rand].pos.x, 
			                                    "y",RANKING.CAMERAMOVELIST[rand].pos.y,
			                                    "z",RANKING.CAMERAMOVELIST[rand].pos.z,
			                                    "speed",3.0f,
			                                    "easetype",iTween.EaseType.linear));
			iTween.RotateTo(m_camera.gameObject, iTween.Hash("x",RANKING.CAMERAMOVELIST[rand].angle.x, 
			                                      "y",RANKING.CAMERAMOVELIST[rand].angle.y,
			                                      "z",RANKING.CAMERAMOVELIST[rand].angle.z,
			                                      "speed",3.0f,
			                                      "easetype",iTween.EaseType.linear));

			m_cameraNowPoint = rand;
            m_nowcount = 0.0f;
		}
		/// 
		/// 
		///////////////////////////////////////////////////////
		// 時間が来た時終了
		if(m_townTime >= RANKING.TOWN_SECOND){
			m_nowcount = 0;
			m_fade.Fadeout();
		}
		if (m_fade.IsEndFadeOut) {
			GameObject obj = GameObject.Find("RankingData");
			Destroy(obj);
			Application.LoadLevel("RankingAndroid");
		}
		// みんなでダンスの判定
		if (Random.value <= RANKING.DANCEEVENT_PROBABILITY && m_danceFlg == false) {
			// ToDo:関数へ
			// 座標を決めて
			Vector3 as_pos;
			Vector2 center;
			Model	set_script;
			int		dance;
			center.x = m_startPos_nearZ.x;
			center.y = m_startPos_nearZ.z;
			center.x += Random.Range(0f, length.x);
			center.y += Random.Range(0f, length.y);
			dance = Random.Range(2,7);
			dance_num = 0;
			for(int i=0; i<model_num; i++){
				// 座標を見て範囲内であればコントロールを得る
				as_pos = MODELS[i].transform.position;
				if((center.x+RANKING.DANCERANGE_X)>as_pos.x &&
				   (center.x-RANKING.DANCERANGE_X)<as_pos.x &&
				   (center.y+RANKING.DANCERANGE_Z)>as_pos.z &&
				   (center.y-RANKING.DANCERANGE_Z)<as_pos.z){

					set_script = MODELS[i].GetComponent<Model>();
					if(set_script.Isbusy == false){
						Debug.Log("格納");
						DANCE.Add(set_script);
						set_script.ActInit();
						set_script.Isbusy = true;
						set_script.AnimChenge(dance);
						set_script.Isstate = Model.MODEL_STATE.ope;
						dance_num++;
					}

				}
			}
			Debug.Log("ok");
			m_danceFlg = true;
		}
		if (m_danceFlg == true) {
            if (DANCE.Count != 0)
            {
                // 最初のステータスを見て
                AnimatorStateInfo info;
                Animator anim;
                anim = DANCE[0].GetComponent(typeof(Animator)) as Animator;
                info = anim.GetCurrentAnimatorStateInfo(0);
                if (info.nameHash == Animator.StringToHash("Base Layer.EndCheck"))
                {
                    for (int i = 0; i < dance_num; i++)
                    {
                        DANCE[i].ActInit();
                    }
                    DANCE.Clear();
                    DANCE.TrimExcess();
                    m_danceFlg = false;

                    Debug.Log("初期化");
                }
            }
            else
            {
                m_danceFlg = false;

                Debug.Log("初期化");
            }
		}

		m_nowcount += Time.deltaTime;
        m_townTime += Time.deltaTime;
	}

	// 生成したモデルの格納
	public void AddModel(GameObject model){
		MODELS.Add (model);
		model_num++;
	}
}
