using UnityEngine;
using System.Collections;

public class HigherRankDisplay : MonoBehaviour {
	UILabel Rank,Name,Score;
	UIPanel Panel;
	GameObject	m_panelObj;		// aいじる用
	int object_number;

	getRequestAndroid.data_android data;
	RankingManager rm;
	RankingData m_RankData;
	
	// 定数呼び出し
	RankingSetting	RANKING;

	// Use this for initialization
	IEnumerator Start () {
		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		
		Rank = this.transform.FindChild("Panel/Rank").GetComponent<UILabel> ();
		Name = this.transform.FindChild("Panel/Name").GetComponent<UILabel> ();
		Score= this.transform.FindChild("Panel/Score").GetComponent<UILabel> ();
		m_panelObj = this.transform.FindChild ("Panel").gameObject;
		Panel = this.transform.FindChild("Panel").GetComponent<UIPanel> ();
		
		GameObject obj = GameObject.Find ("RankingData");
		m_RankData = obj.GetComponent<RankingData>();

		Panel.alpha = 0.0f;

		rm = GameObject.Find("/UI Root (2D)/Camera/Anchor/Panel").GetComponent<RankingManager>();
		data = m_RankData.getRankData(object_number);
		RankDataDisplay((object_number+1).ToString(),data.name,data.score.ToString());

		// 初期演出
		float waittime = 0.3f * (float)object_number;
		yield return new WaitForSeconds(waittime);
		iTween.MoveFrom (gameObject, iTween.Hash ("x", RANKING.MoveMent_X, "time", RANKING.MoveMent_Time, "easetype", iTween.EaseType.easeOutQuad));
		StartCoroutine(Fadein());
	}

	// Update is called once per frame
	void Update () {
		bool move_flg = rm.IsMove;

		if(move_flg){
			this.transform.Translate (0.0f, RANKING.MoveScroll, 0.0f);
			if (this.transform.localPosition.y >= RANKING.HideLine) {
				if (Panel.alpha > 0f) {
					Panel.alpha -= RANKING.HideAlpha;
				}
				if (Panel.alpha <= 0f) {
					// デストロイ
					Destroy (gameObject);
					rm.ChangeScene (object_number);	
				}
			} else if (this.transform.localPosition.y >= RANKING.DispLine) {
				if (Panel.alpha < 1f) {
					Panel.alpha += RANKING.DispAlpha;
				}
			}

		}
	}

	// 表示用のaいじり
	private IEnumerator Fadein(){
		int i;
		while (Panel.alpha < 1.0f) {
			Panel.alpha += RANKING.MoveMent_Alpha;
			yield return null;
		}
		yield return new WaitForSeconds (3f);
		rm.StartMove (object_number);
	}

	public void Initialize(int num){
		object_number = num;
	}
	
	private void RankDataDisplay(string num, string name, string score){
		Rank.text = num;
		Name.text = name;
		Score.text = score;
	}
}
