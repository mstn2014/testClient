using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WWWKit;
using MiniJSON;

public class getRequestAndroid : MonoBehaviour {

	    // サーバから受け取るデータ構造
    public class data_android : Object
    {
        public string name;
        public long id, score;
    }

    private MonoBehaviour mMonoBehaviour;

  	WWWClientManager cm;
    List<data_android> containerList;
    //public string ipaddr;       // インスペクター上で設定すること
    int startNum = 0;           // ランキングの初期値
    data_android[] dt;

    int xpos = 200;

    RankingManager rm;

    bool datasend_flg;

    // Use this for initialization
    void Start()
    {
        cm = new WWWClientManager(this);
        containerList = new List<data_android>();
        getMessage();

        rm = GameObject.Find("/UI Root (2D)/Camera/Anchor/Panel").GetComponent<RankingManager>();
        datasend_flg = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            startNum += 20;
            if (startNum > containerList.Count) startNum -= 20;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            startNum -= 20;
            if (startNum < 0) startNum = 0;
        }

        int count = (containerList.Count - startNum) > 20 ? 20 : (containerList.Count - startNum);
        dt = new data_android[count];
        containerList.CopyTo(startNum, dt, 0, count);
    }

    //--------------------------------------------------------
    // UIButtonMessageからのメッセージを受ける関数
    //--------------------------------------------------------
    void getMessage()
    {
        string url = "http://mstn2014-osaka.herokuapp.com/users/score";
        cm.GET(url, "ReceiveRequest");
    }

    void ReceiveRequest(WWW www)
    {
        string json = www.text;
        var scoreInfo = Json.Deserialize(json) as Dictionary<string, object>;
        int i = 1;
        containerList.Clear();
        foreach (object ob in scoreInfo)
        {
            Dictionary<string, object> num = (Dictionary<string, object>)scoreInfo[i.ToString()];
            long id = (long)num["id"];
            string name = (string)num["name"];
            long score = (long)num["score"];
            data_android data1 = new data_android();
            data1.id = id;
            data1.name = name;
            data1.score = score;
            containerList.Add(data1);
            i++;
        }
        
    }

    void ReceiveError()
    {
        Debug.Log("GET出来ませんでした。");
    }

    //--------------------------------------------------------
    // ランキングの描画
    //--------------------------------------------------------
    void OnGUI()
    {
        /*GUI.Label(new Rect(xpos, 0, 100, 100), "rank");
        GUI.Label(new Rect(xpos + 30, 0, 100, 100), "id");
        GUI.Label(new Rect(xpos + 60, 0, 100, 100), "name");
        GUI.Label(new Rect(xpos + 120, 0, 100, 100), "score");*/
        if(datasend_flg != true){
            DataSend();
        }
    }

    private void drawsingleline(int pos, data_android toShow)
    {
        /*pos++;
        GUI.Label(new Rect(xpos, pos * 20, 100, 100), (startNum + pos).ToString());
        GUI.Label(new Rect(xpos + 30, pos * 20, 100, 100), toShow.id.ToString());
        GUI.Label(new Rect(xpos + 60, pos * 20, 100, 100), toShow.name);
        GUI.Label(new Rect(xpos + 120, pos * 20, 100, 100), toShow.score.ToString());*/
    }

    private void drawTable()
    {
       /*if (containerList.Count == 0) return;

        int j = 0;
        foreach (data_android thecont in dt)
        {
            drawsingleline(j, thecont);
            j++;
        }*/
    }

    public void DataSend()
    {
        if (containerList.Count == 0) return;

        int j = 0;
         foreach (data_android thecont in dt)
        {
            rm.DataSet(j, thecont);
            j++;
            datasend_flg = true;
        }
    }
}
