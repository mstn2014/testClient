using UnityEngine;
using System.Collections;

public class RankingDisplay : MonoBehaviour {

	UILabel Rank,Id,Name,Score;
	UIPanel Panel;
	int object_number;

	bool DataSet_flg;

	float move_num;

	getRequestAndroid.data_android data;
	RankingManager rm;

	float max_line;
	float base_line;
	float alpha;

	// Use this for initialization
	void Start () {
		Rank = this.transform.FindChild("Panel/Rank").GetComponent<UILabel> ();
		Id	 = this.transform.FindChild("Panel/ID").GetComponent<UILabel> ();
		Name = this.transform.FindChild("Panel/Name").GetComponent<UILabel> ();
		Score= this.transform.FindChild("Panel/Score").GetComponent<UILabel> ();
		Panel=this.transform.FindChild("Panel").GetComponent<UIPanel> ();

		DataSet_flg = false;

		move_num = 0.003f;
		max_line = 95.0f;
		base_line= -149.0f;
		alpha = 0.03f;


		if(object_number > 9){
			Panel.alpha = 0.0f;
		}
	}
	

	// Update is called once per frame
	void Update () {
		if(DataSet_flg != true){
			DataSet_flg = true;
			rm = GameObject.Find("/UI Root (2D)/Camera/Anchor/Panel").GetComponent<RankingManager>();
			data = rm.DataSend(object_number);
			RankDataDisplay((object_number+1).ToString(),data.id.ToString(),data.name,data.score.ToString());
		}

		this.transform.Translate(0.0f,move_num,0.0f);
		if(this.transform.localPosition.y >= max_line)
		{
			if(Panel.alpha >= 0){
				Panel.alpha  -= alpha;
			}else if(Panel.alpha == 0){
				// デストロイ
				Destroy(this);
			}

		}else
		if(this.transform.localPosition.y >= base_line){
			if(Panel.alpha >= 0){
				Panel.alpha  += alpha;
			}
		}

	}

	public void Initialize(int num){
		object_number = num;
	}

	private void RankDataDisplay(string num, string id, string name, string score){
		Rank.text = num;
		Id.text = id;
		Name.text = name;
		Score.text = score;
	}


}
