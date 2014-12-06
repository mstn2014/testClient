using UnityEngine;
using System.Collections;

public class Model : MonoBehaviour {
	enum MODEL_STATE{set, stand, walk, bon, fla, hula, belly, samba, special1, special2, special3};
	private MODEL_STATE m_state;

	private Animator	m_anim;
	private int			m_animCount, m_nowCount;

	// 定数呼び出し
	RankingSetting	RANKING; 
	// Use this for initialization
	void Start () {
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		m_state = MODEL_STATE.set;
		m_anim = GetComponent (typeof(Animator)) as Animator;

		m_nowCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*
			行動の変更の確認
		 */
		switch (m_state) {
		case MODEL_STATE.set:
			m_anim.Play("player_stand001");

			m_animCount = Random.Range(RANKING.MIN_ANIM_SECOND, RANKING.MAX_ANIM_SECOND);
			m_animCount *= 60;	// 大体60fps
			// ToDo:アニメーションを決める


		break;
		case MODEL_STATE.stand:
			m_anim.Play("player_stand002");
		break;
		
		case MODEL_STATE.walk:
			m_anim.Play("player_walk");
			break;

		case MODEL_STATE.bon:
			// 盆踊り
			m_anim.Play("player_bonodori");
		break;

		case MODEL_STATE.fla:
			// フラミンゴ
			m_anim.Play("player_fla");
		break;

		case MODEL_STATE.hula:
			// フラメンコ
			m_anim.Play("player_hula");
		break;

		case MODEL_STATE.belly:
			// ベリー
			m_anim.Play("player_belly");
		break;

		case MODEL_STATE.samba:
			// サンバ
			m_anim.Play("player_samba");
		break;

		case MODEL_STATE.special1:
			// 合言葉&ポーズ
		break;

		case MODEL_STATE.special2:
			// ダンス
		break;

		case MODEL_STATE.special3:
			// 
		break;
		}
		// カウント
		m_nowCount ++;
		if (m_nowCount >= m_animCount) {
			m_nowCount = 0;
			m_state = MODEL_STATE.set;
		}
		/*
		 * 移動自体はprefabごと
		 * 回転に関してはmodelのみ
		 * 移動に関しては移動目標に対して移動
		 * 向きを変える処理.itween.lookupdate
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
