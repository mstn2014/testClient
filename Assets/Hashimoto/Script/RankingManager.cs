using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankingManager : MonoBehaviour {

	getRequestAndroid getreq;

	public GameObject prefab;
	int scaleNum,rank_num;	
	float rank_xpos,rank_ypos,diff;
	Vector3 BaseLine,MaxLine;

	List<getRequestAndroid.data_android> containerList;

	// Use this for initialization
	void Start () {
		getreq = GameObject.Find("/getRequestObject").GetComponent<getRequestAndroid>();

		scaleNum = 160;
		rank_xpos = (float)60.0f;
		rank_ypos = (float)85.0f;
		diff = (float)25.0f;
		rank_num = 0;
		containerList = new List<getRequestAndroid.data_android>();														
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void DataSet(int num,getRequestAndroid.data_android toShow){
		containerList.Add(toShow);

		RankingObjectGeneration(toShow,rank_num);
		rank_num++;
	}


	private void RankingObjectGeneration(getRequestAndroid.data_android toShow,int num){
		GameObject ranking = (GameObject)GameObject.Instantiate(prefab, this.transform.position, this.transform.rotation );
		ranking.transform.parent = this.transform;
		ranking.transform.localScale = new Vector3(scaleNum,scaleNum,scaleNum);
		ranking.transform.localPosition = new Vector3(rank_xpos,rank_ypos,(float)0.0f);
		rank_ypos = rank_ypos - diff;
		ranking.GetComponent<RankingDisplay>().Initialize(num);
	}

	public getRequestAndroid.data_android DataSend(int num){
		return containerList[num];
	}

	public void ChangeScene(int num){
		print(rank_num);
		if((rank_num-1) == num){
			Application.LoadLevel("RankingAvatarTown");
		}
	}

}
