using UnityEngine;
using System.Collections;

public class Model : MonoBehaviour {
	enum MODEL_STATE{set, active};
	private MODEL_STATE m_state;

	private Animator	m_anim;
	private NavMeshAgent m_navi;
	private int			m_animCount, m_nowCount;

	private Vector3 start_nearZ, end_farZ;
	private Vector2	length;
	private bool	StartUp_flg;
	// 定数呼び出し
	RankingSetting	RANKING; 
	// Use this for initialization
	void Start () {
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		m_state = MODEL_STATE.set;
		m_anim = GetComponent (typeof(Animator)) as Animator;
		m_navi = GetComponent (typeof(NavMeshAgent)) as NavMeshAgent;

		m_nowCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*
			行動の変更の確認
		 */
		if (StartUp_flg) {
			switch (m_state) {
			case MODEL_STATE.set:
				m_anim.Play ("player_stand001");

				int action = Random.Range(0, 11);
				switch(action){
				case 1:		// 立ち
					m_state = MODEL_STATE.active;
					break;
				case 2:		// 歩き
				case 3:
				case 4:
					// 移動目標
					float move_x;
					float move_z;
					Vector3 move_point;

					move_point = transform.position;

					// 乱数で移動距離産出
					move_x = Random.Range(RANKING.MIN_MOVERANGE, RANKING.MAX_MOVERANGE);
					move_z = Random.Range(0, length.y);
					// マイナス?プラス?
					if(0==Random.Range(0,2)){
						move_x *= -1;
					}
					if(0==Random.Range(0,2)){
						move_z *= -1;
					}
					// 座標超えてない?
					if((move_point.x+move_x)<start_nearZ.x || (move_point.x+move_x)>end_farZ.x){
						move_x *= -1;
					}
					if((move_point.z+move_z)<start_nearZ.z || (move_point.z+move_z)>end_farZ.z){
						move_z *= -1;
					}
					move_point.x += move_x;
					move_point.z += move_z;
					//move_point.x += 1;
					//move_point.z -= 1;
					m_navi.SetDestination(move_point);
					m_anim.Play ("player_walk");
					m_state = MODEL_STATE.active;
					break;

				case 5:		// ダンス
				case 6:
					action = Random.Range(0,5);
					switch(action){
					case 0:
						m_anim.Play ("player_bonodori");	// 盆踊り
						break;
					case 1:
						m_anim.Play ("player_fla");			// フラミング
						break;
					case 2:
						m_anim.Play ("player_hula");		// フラメンコ
						break;
					case 3:
						m_anim.Play ("player_belly");		// ベリー
						break;
					case 4:
						m_anim.Play ("player_samba");		// サンバ
						break;

					}
					m_state = MODEL_STATE.active;
					break;

				case 7:		// 特殊1: 相手を見つけて合言葉&ポーズ
				case 8:
					// 判定 対象を一人だけ見つける
					// 近づく
					// アニメーション開始
					break;
				case 9:		// 特殊2: みんなでダンス
				case 10:
					// 判定 相手を見つける範囲攻撃
					// 整列
					// ダンスを決める
					// ダンス開始


					break;
				}
				m_animCount = Random.Range (RANKING.MIN_ANIM_SECOND, RANKING.MAX_ANIM_SECOND);
				m_animCount *= 60;	// 大体60fps
			break;

			case MODEL_STATE.active:

				break;
			}
			// カウント
			m_nowCount ++;
			if (m_nowCount >= m_animCount) {
				m_navi.Stop();
				m_nowCount = 0;
				m_state = MODEL_STATE.set;
			}
					/*
	 * アニメーションの変更処理
	 * 歩き.走り.踊り
	 * 特殊
	 * 踊り.合言葉
	 * 
	 * 対象を発見	コントタクトをとる
	 * 対象に自分のgameobjectと
	 */
		}
	}

	public void Init(Vector3 start_point, Vector3 end_point, Vector2 len){
		start_nearZ = start_point;
		end_farZ = end_point;
		length = len;

		StartUp_flg = true;
	}
}
