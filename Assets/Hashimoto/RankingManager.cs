using UnityEngine;
using System.Collections;

public class RankingManager : MonoBehaviour {

	getRequestAndroid getreq;

	public GameObject prefab;
	int scaleNum;	
	float rank_xpos,rank_ypos,diff;
	int rank_num;
	GameObject[] rank_save;

	// Use this for initialization
	void Start () {
		getreq = GameObject.Find("/getRequestObject").GetComponent<getRequestAndroid>();

		rank_num = 10;
		scaleNum = 160;
		rank_xpos = (float)60.0f;
		rank_ypos = (float)174.0f;
		diff = (float)36.0f;
		rank_save = new GameObject[rank_num];
		for(int i=0; i<rank_num; i++){
			GameObject ranking = (GameObject)GameObject.Instantiate(prefab, this.transform.position, this.transform.rotation );
			ranking.transform.parent = this.transform;
			ranking.transform.localScale = new Vector3(scaleNum,scaleNum,scaleNum);
			ranking.transform.localPosition = new Vector3(rank_xpos,rank_ypos,(float)0.0f);
			rank_ypos = rank_ypos - diff; 
			rank_save[i] = ranking;
		}
		//getreq.DataSend();															
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void SendDataDisp(int rank, getRequestAndroid.data_android toShow){
		RankingDisplay rd = rank_save[rank].GetComponent<RankingDisplay>();
		rd.RankDisplay((rank+1).ToString());
		rd.IdDisplay(toShow.id.ToString());
		rd.NameDisplay(toShow.name);
		rd.ScoreDisplay(toShow.score.ToString());
	}
}
