using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingSetting : ScriptableObject {

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
	public float	SCROLL_SECOND;	// スクロールする時間
	public int		BLOCK_NUM;		// ステージ分割数(生成場所で密度が極端にならないように)
	public int		MODEL_NUM;		// 生成するモデルの数
	public float	MODELPOS_Y;		// モデルのY座標
	public float	RANKSCALL;		// ラベルのサイズ
	public float	RANKPOS_COM;	// ラベルの座標補正
	public int		MIN_ANIM_SECOND;// 最低アニメーション時間
	public int		MAX_ANIM_SECOND;// 最高アニメーション時間
	public float	MIN_MOVERANGE;	// モデルの最小移動距離
	public float	MAX_MOVERANGE;	// モデルの最大移動距離
}
