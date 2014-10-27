using UnityEngine;
using System.Collections;

public class Ranking : MonoBehaviour {

	UILabel Rank,Id,Name,Score;

	// Use this for initialization
	void Start () {
		Rank = this.transform.FindChild("Panel/Rank").GetComponent<UILabel> ();
		Id	 = this.transform.FindChild("Panel/ID").GetComponent<UILabel> ();
		Name = this.transform.FindChild("Panel/Name").GetComponent<UILabel> ();
		Score= this.transform.FindChild("Panel/Score").GetComponent<UILabel> ();
	}
	
	// Update is called once per frame
	void Update () {
		Rank.text = "1";
		Id.text = "1893";
		Name.text = "ABCD";
		Score.text= "100095";
	}

}
