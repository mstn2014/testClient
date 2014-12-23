using UnityEngine;
using System.Collections;

public class FadeMgr : MonoBehaviour {
	private UIWidget	m_sprite;
	private Color		m_color;
	private float		m_oldColorA;		// 古いアルファ
	private bool		m_fadeoutFlg;

	public bool IsEndFadeOut{
		get{return m_fadeoutFlg;}
	}

	// Use this for initialization
	void Awake () {
		m_sprite = GetComponent<UIWidget>();
		m_fadeoutFlg = false;
	}

	// Update is called once per frame
	void Update () {}

	// フェードイン
	public void Fadein(){
		iTween.ValueTo (gameObject, iTween.Hash ("from", 1, "to", 0, "time", 2f, "easetype", iTween.EaseType.easeOutQuad, "onupdate", "ValueChange"));
	}

	// フェードアウト
	public void Fadeout(){
		iTween.ValueTo (gameObject, iTween.Hash ("from", 0, "to", 1, "time", 2f, "easetype", iTween.EaseType.easeInOutQuad,"onupdate","ValueChange"));
	}

	void ValueChange(float value){
		m_color = m_sprite.color;
		m_oldColorA = m_color.a;
		m_color.a = value;
		m_sprite.color = m_color;
		if ((value - m_oldColorA) > 0 && m_color.a >= 1f) {
			m_fadeoutFlg = true;		
		}
	}
}
