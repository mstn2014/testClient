using UnityEngine;
using System.Collections;

public class Model : MonoBehaviour {
	enum MODEL_STATE{set, stand, walk, dance};
	private MODEL_STATE m_state;

	private Animator	m_anim;
	private NavMeshAgent m_navi;
	private int			m_animCount, m_nowCount;
	private AnimatorStateInfo	m_animState;	// モデルのステータス

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
		if (StartUp_flg) {
			switch (m_state) {
			case 0:
				int action = Random.Range(0, 10);
				switch(action){
				case 1:		// 立ち
					m_animCount = Random.Range (RANKING.MIN_ANIM_SECOND, RANKING.MAX_ANIM_SECOND);
					m_animCount *= 60;	// 大体60fps
					m_anim.SetInteger("DanceType",0);
					m_state = MODEL_STATE.stand;
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
					move_z = Random.Range(0f, (length.y/2f));
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
					m_navi.SetDestination(move_point);

					m_animCount = Random.Range (RANKING.MIN_ANIM_SECOND, RANKING.MAX_ANIM_SECOND);
					m_animCount *= 60;	// 大体60fps
					m_anim.SetInteger("DanceType",1);
					m_state = MODEL_STATE.walk;
					break;

				case 5:		// ダンス
				case 6:
					action = Random.Range(2,7);
					m_anim.SetInteger("DanceType", action);
					m_anim.SetTrigger("nowDance");


					m_state = MODEL_STATE.dance;
					break;

				case 7:		// 特殊1: 相手を見つけて合言葉&ポーズ
				case 8:
					// 判定 対象を一人だけ見つける
					// 近づく
					// アニメーション開始
					break;
				case 9:		// 特殊2: みんなでダンス
				case 0:
					// 判定 相手を見つける範囲攻撃
					// 整列
					// ダンスを決める
					// ダンス開始


					break;
				}
			break;

			case MODEL_STATE.stand:
				m_nowCount++;
				if(m_nowCount > m_animCount){
					m_nowCount = 0;
					m_anim.SetInteger("DanceType",7);
					m_state = MODEL_STATE.set;
				}
				break;

			case MODEL_STATE.walk:
				m_nowCount++;
				if(m_nowCount > m_animCount){
					m_navi.Stop();
					m_nowCount = 0;
					m_anim.SetInteger("DanceType",7);
					m_state = MODEL_STATE.set;
				}
				break;

			case MODEL_STATE.dance:
				// モデルのステータスを取得
				m_animState = m_anim.GetCurrentAnimatorStateInfo(0);		// ダンスの切り替え
				if(m_animState.nameHash == Animator.StringToHash("Base Layer.EndCheck")){
					m_anim.SetInteger("DanceType",7);
					m_state = MODEL_STATE.set;
				}
				break;
			}
		}
	}

	public void Init(Vector3 start_point, Vector3 end_point, Vector2 len){
		start_nearZ = start_point;
		end_farZ = end_point;
		length = len;

		StartUp_flg = true;
	}
}
