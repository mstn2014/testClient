using UnityEngine;
using System.Collections;

public class RankingManager : MonoBehaviour {

	public GameObject prefab;

	int scaleNum;	
	float xpos,ypos,diff;

	// Use this for initialization
	void Start () {
		scaleNum = 160;
		xpos = (float)60.0f;
		ypos = (float)174.0f;
		diff = (float)36.0f;
		for(int i=0; i<10; i++){
			GameObject ranking = (GameObject)GameObject.Instantiate(prefab, this.transform.position, this.transform.rotation );
			ranking.transform.parent = this.transform;
			ranking.transform.localScale = new Vector3(scaleNum,scaleNum,scaleNum);
			ranking.transform.localPosition = new Vector3(xpos,ypos,(float)0.0f);
			ypos = ypos - diff; 
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
