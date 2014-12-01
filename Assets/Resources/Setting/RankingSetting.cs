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

	public float	MoveMent_X;
	public float	MoveMent_Time;
	public float	MoveMent_Alpha;

}
