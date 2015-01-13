using UnityEngine;
using System.Collections;

public class Rank_ver2 : MonoBehaviour
{
    private Camera m_camera;
    UILabel m_info;
    RankingData m_data;

    // Use this for initialization
    void Awake()
    {
        m_camera = Camera.main;
        GameObject go = Instantiate(Resources.Load<GameObject>("Prefab/PlayerInfo")) as GameObject;
        m_data = GameObject.Find("RankingData").GetComponent<RankingData>();
        go.transform.parent = GameObject.Find("Anchor").transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localRotation = Quaternion.identity;
        go.transform.localScale = Vector3.one;
        go.transform.GetChild(0).localPosition = new Vector3(48, 48, 1);
        m_info = go.GetComponentInChildren<UILabel>();
        m_info.text = "None";
    }

    // Update is called once per frame
    void Update()
    {
        // ここにキャラの頭上の情報を表示する処理を書く
        float FontScaleRate = 0.05f;
        float SetFontSize = 48.0f;
        float distance = Vector3.Distance(transform.position, m_camera.transform.position);
        Vector3 work = m_camera.WorldToScreenPoint(transform.position);
        SetFontSize /= (1.0f + distance * FontScaleRate);
        //distance = Mathf.Clamp(1.0f/distance, 0.2f, 1.0f);
        m_info.transform.localPosition = new Vector3(work.x - Screen.width * 0.5f, work.y - Screen.height * 0.5f + distance*FontScaleRate*10.0f, 1);
        
        m_info.transform.localScale = new Vector3(SetFontSize,SetFontSize,1);
        m_info.transform.parent.localPosition = new Vector3(0, 0, work.z);
    }

    public void SetInfo(string info,int colorNo)
    {
        m_info.text = info;

        switch (colorNo)
        {
            case 0: m_info.color = Color.red; break;
            case 1: m_info.color = Color.blue; break;
            case 2: m_info.color = Color.green; break;
            case 3: m_info.color = Color.cyan; break;
            case 4: m_info.color = Color.magenta; break;
        }
        
    }
}
