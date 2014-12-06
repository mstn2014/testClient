using UnityEngine;
using System.Collections;

public class TownManager : MonoBehaviour {
	
	private Camera		m_camera;
	private GameObject	m_ground;
	private Vector3		m_startPos_nearZ;	// カメラのスタート地点(X座標)とモデル配置のニア(Z座標)
	private Vector3		m_endPos_farZ;		// カメラのエンド地点(X座標)とモデルの配置のファー(Z座標)
	private float		m_scrollSpeed;

	private AnimationSetting m_setAnim;		// キャラの配置&アニメーション設定

	// 定数呼び出し
	RankingSetting	RANKING; 

	// Use this for initialization
	void Start () {
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		m_camera = Camera.main;
		m_ground = GameObject.Find ("Ground").gameObject;
		m_setAnim = GetComponent<AnimationSetting>();

		// カメラをstart地点へ
		m_startPos_nearZ	= m_ground.transform.FindChild ("StartingPoint_nearZ").transform.position;
		m_endPos_farZ	= m_ground.transform.FindChild ("EndPoint_farZ").transform.position;
		Vector3 work_vec = m_camera.transform.position;
		work_vec.x = m_startPos_nearZ.x;
		m_camera.transform.position = work_vec;
		// スクロールスピードを決める
		m_scrollSpeed = (m_endPos_farZ.x - m_startPos_nearZ.x) / RANKING.SCROLL_SECOND / 60;	// 60fpsで割る
		if (RANKING.SCROLL_SECOND <= 0)		Debug.Log ("スクロール時間が正しくありません");


		// モデルの生成
		m_setAnim.SetAnimation (m_startPos_nearZ, m_endPos_farZ);
		/*
			モデルの生成と設定
			モデルのシチュエーションを決めるスクリプト作成
			(アニメーションの設定は用意したやつから行うものによっては多人数で行う)
		 */

		/* モデルの配置
			カメラを地面の始点へ			ok
			シーン切り替えは（終点ー始点＝距離）	ok?
			// 手前と奥を設定したら配置も楽？
			終点についたらシーン切り替え

		*/
	}
	
	// Update is called once per frame
	void Update () {
		// カメラの移動
		m_camera.transform.position += new Vector3(m_scrollSpeed,0,0);
		// カメラが終端に来た時シーン変更
		if(m_camera.transform.position.x >= m_endPos_farZ.x){
			Application.LoadLevel("RankingAndroid");
			
			GameObject obj = GameObject.Find("RankingData");
			Destroy(obj);
		}
	}
}
