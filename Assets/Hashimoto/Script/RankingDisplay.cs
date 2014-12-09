using UnityEngine;
using System.Collections;

public class RankingDisplay : MonoBehaviour {

	UILabel Rank,Name,Score;
	UIPanel Panel;
	int object_number;

	bool DataSet_flg;

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
		Panel=this.transform.FindChild("Panel").GetComponent<UIPanel> ();

		GameObject obj = GameObject.Find ("RankingData");
		m_RankData = obj.GetComponent<RankingData>();

		rm = GameObject.Find("/UI Root (2D)/Camera/Anchor/Panel").GetComponent<RankingManager>();
		data = m_RankData.getRankData(object_number);
		RankDataDisplay((object_number+1).ToString(),data.id.ToString(),data.name,data.score.ToString());

		Panel.alpha = 0.0f;

		yield return new WaitForSeconds (5f);
		rm.StartMove (object_number);
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

	public void Initialize(int num){
		object_number = num;
	}

	private void RankDataDisplay(string num, string id, string name, string score){
		Rank.text = num;
		Name.text = name;
		Score.text = score;
	}
}
