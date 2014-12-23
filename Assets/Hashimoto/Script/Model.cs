using UnityEngine;
using System.Collections;

	
public class Model : MonoBehaviour {
	public enum MODEL_STATE{set, stand, walk, dance, special, ope};
	private MODEL_STATE m_state;

	private Animator	m_anim;
	private NavMeshAgent m_navi;
	private int			m_animCount, m_nowCount;
	private AnimatorStateInfo	m_animState;	// モデルのステータス
	private GameObject	m_friend;		// 合言葉&ポーズしてくれるモデル
	private Model		m_friendScript;	// 対象のスクリプト
	private bool		m_busy_flg;		// 応答できるかどうか

	private Vector3 start_nearZ, end_farZ;
	private Vector2	length;
	private bool	StartUp_flg;
	// 定数呼び出し
	RankingSetting	RANKING; 

	// プロパティ
	public bool Isbusy{
		get{return m_busy_flg;}
		set{m_busy_flg = value;}
	}
	public MODEL_STATE Isstate{
		set{m_state = value;}
	}
	// Use this for initialization
	void Start () {
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		m_state = MODEL_STATE.set;
		m_anim = GetComponent (typeof(Animator)) as Animator;
		m_navi = GetComponent (typeof(NavMeshAgent)) as NavMeshAgent;

		m_nowCount = 0;
		m_busy_flg = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (StartUp_flg) {
			switch (m_state) {
			case MODEL_STATE.set:
				// モデルのステータスを取得
				m_animState = m_anim.GetCurrentAnimatorStateInfo(0);		// stay状態じゃないとき抜ける
				if(m_animState.nameHash != Animator.StringToHash("Base Layer.stay")){
					break;
				}
				int action = Random.Range(0, 10);
				switch(action){
				case 0:
				case 1:		// 立ち
					m_animCount = Random.Range (RANKING.MIN_ANIM_SECOND, RANKING.MAX_ANIM_SECOND);
					m_animCount *= 60;	// 大体60fps
					m_anim.SetInteger("DanceType",7);
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

				case 7:	
				case 5:		// ダンス
				case 6:
					action = Random.Range(2,7);
					m_anim.SetInteger("DanceType", action);

					m_busy_flg = true;
					m_state = MODEL_STATE.dance;
					break;


				case 8:		// 特殊1: 相手を見つけて合言葉&ポーズ
				case 9:
					// レイヤー８の"model"にだけレイキャストする
					int layerMask = 1 << 8;
					// 判定 対象を一人だけ見つける
					RaycastHit hit;
					if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, length.y, layerMask)){
						m_friend = hit.transform.gameObject;
						m_friendScript = m_friend.GetComponent<Model>();
						// 相手のフラグ確認
						if(m_friendScript.Isbusy == false){
							// 近づく場所を決める
							Vector3 access = (transform.position - m_friend.transform.position);
							access = m_friend.transform.position + access/2;
							// 近づく処理
							transform.LookAt(access);
							m_friend.transform.LookAt(access);
							m_navi.SetDestination(access);
							m_friendScript.NavSet(access);
							// 状態の変更
							m_friendScript.Isbusy = true;	// ほかから干渉を受けないように
							m_busy_flg = true;
							m_friendScript.AnimChenge(1);	// アニメーションを歩きへ
							m_anim.SetInteger("DanceType",1);
							m_friendScript.Isstate = MODEL_STATE.ope;
							m_state = MODEL_STATE.special;
						}
					}
					break;
				}
			break;

			case MODEL_STATE.stand:
				// アニメーションの割り込み判定
				if(m_busy_flg == true){
					m_nowCount = 0;
					break;
				}
				m_nowCount++;
				if(m_nowCount > m_animCount){
					ActInit();
				}
				break;

			case MODEL_STATE.walk:
				// アニメーションの割り込み判定
				if(m_busy_flg == true){
					m_nowCount = 0;
					break;
				}
				m_nowCount++;
				if(m_nowCount > m_animCount){
					ActInit();
				}
				break;

			case MODEL_STATE.dance:
				// モデルのステータスを取得
				m_animState = m_anim.GetCurrentAnimatorStateInfo(0);		// ダンスの切り替え
				if(m_animState.nameHash == Animator.StringToHash("Base Layer.EndCheck")){
					//m_anim.SetTrigger("EndCheck");
					ActInit();
				}
				break;

			case MODEL_STATE.special:
				// 近づきすぎないようにする
				Vector3 poscheck;			// 対象のモデルとの距離
				poscheck = (transform.position - m_friend.transform.position);
				poscheck.x =  Mathf.Abs(poscheck.x);
				poscheck.z =  Mathf.Abs(poscheck.z);
				if(poscheck.x < 3 && poscheck.z < 3){
					m_navi.Stop();				// 移動の終了
					m_friendScript.NavStop();
					m_friendScript.AnimChenge(8);// アニメーションの変更
					m_anim.SetInteger("DanceType", 8);

					// ここでポーズのアニメーション
					// 終わったら
				}
				// モデルのステータスを取得
				m_animState = m_anim.GetCurrentAnimatorStateInfo(0);		// ダンスの切り替え
				if(m_animState.nameHash == Animator.StringToHash("Base Layer.EndCheck")){
					//m_anim.SetTrigger("EndCheck");
					Debug.Log("ポーズ&合言葉");
					ActInit();
					m_friendScript.ActInit();
				}

				break;

			case MODEL_STATE.ope:
				// 操作される側 何もしない
				return;
				break;
			}
		}
	}

	// 共通の行動の初期化
	public void ActInit(){
		m_navi.Stop();
		m_nowCount = 0;
		m_anim.SetInteger("DanceType",0);
		m_busy_flg = false;
		m_state = MODEL_STATE.set;
	}

	// 任意の場所に移動する
	public void NavSet(Vector3 point){
		m_navi.SetDestination (point);
	}
	public void NavStop(){
		m_navi.Stop ();
	}

	// アニメーションの変更
	public void AnimChenge(int i){
		m_anim.SetInteger ("DanceType", i);
	}

	// 生成時求めたいろいろな値を引き継ぐ
	public void Init(Vector3 start_point, Vector3 end_point, Vector2 len){
		start_nearZ = start_point;
		end_farZ = end_point;
		length = len;

		StartUp_flg = true;
	}
}
