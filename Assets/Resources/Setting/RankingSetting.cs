using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingSetting : ScriptableObject {
	// カメラの状態
	[System.Serializable]
	public class Camera_Condition
	{
		public Vector3 pos;
		public Vector3 angle;
	}

	[Header("ランキング文字関連")]
	public float	HideLine;
	public float	DispLine;
	public float	MoveScroll;
	public float	HideAlpha;
	public float	DispAlpha;

	public float	MoziScall;
	public int		MoziInitPosX;
	public int		MoziInitPosY;
	public int		MoziInterval;

	public float	MoveMent_X;		// 出演時アニメーション関係
	public float	MoveMent_Time;
	public float	MoveMent_Alpha;

	[Header("町関係")]
	public float	TOWN_SECOND;	// 町の時間
	public int		BLOCK_NUM;		// ステージ分割数(生成場所で密度が極端にならないように)
	public int		MODEL_NUM;		// 生成するモデルの数
	public float	MODELPOS_Y;		// モデルのY座標
	public float	RANKSCALL;		// ラベルのサイズ
	public float	RANKPOS_COM;	// ラベルの座標補正
	public int		MIN_ANIM_SECOND;// 最低アニメーション時間
	public int		MAX_ANIM_SECOND;// 最高アニメーション時間
	public float	MIN_MOVERANGE;	// モデルの最小移動距離
	public float	MAX_MOVERANGE;	// モデルの最大移動距離
	public int		WAITTIME;		// アニメーション切り替えのための待ち時間
	public float	DANCEEVENT_PROBABILITY;	// みんなでダンスの確率
	public float	DANCERANGE_X;	// みんなでダンスの距離X
	public float	DANCERANGE_Z;	// みんなでダンスの距離Z
	public int		CAMERAMOVECOUNT;// カメラの移動回数
	public List<RankingSetting.Camera_Condition>	CAMERAMOVELIST;	// カメラが移動する場所
	public int		CAMERAMOVEPOINTNUM;								// カメラの移動ポイント数
}
