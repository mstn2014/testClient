using UnityEngine;
using System.Collections;
using System.Collections.Generic; 
using WWWKit;
using MiniJSON;

public class postRequest : MonoBehaviour {
    WWWClientManager cm;
    //public string ipaddr;

	// Use this for initialization
	void Start () {
        cm = new WWWClientManager(this);
	}
	
	// Update is called once per frame
	void Update () {
	}

    void postMessage()
    {
        string url = "mstn2014-osaka.herokuapp.com/users/regist";
        // ポストするキーはstring型で送る
        Dictionary<string, string> post = new Dictionary<string, string>();

        // ランダムな名前を生成
        string rName = "ABCDEFGHIJKLNMOPQRSTUVWYZ";
        string nName = string.Empty;
        for (int i = 0; i < 3; i++)
        {
            int rnd = Random.Range(0,25);
            nName += rName.Substring(rnd,1);
        }

        post.Add("name", nName);
        int score = Random.Range(0,101);        // UnityEngine.Randam.Range(int,int)は第二引数の数字は含まない。この場合0~100になる。
        post.Add("score", score.ToString());
        cm.POST(url, post, "ReceivePost");
        Debug.Log(nName.ToString() + " " + score.ToString() + " " + "POSTリクエストを送信しました。");
    }

    void ReceivePost(WWW www)
    {
        Debug.Log(www.text);
    }
}
