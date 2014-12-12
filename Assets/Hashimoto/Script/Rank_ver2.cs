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
        m_info.text = "Test";
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 work = m_camera.WorldToScreenPoint(transform.position);
        m_info.transform.localPosition = new Vector3(work.x - Screen.width * 0.5f, work.y - Screen.height * 0.5f + 20.0f, 0);
        m_info.transform.parent.localPosition = new Vector3(0, 0, work.z);
    }

    public void SetInfo(string info)
    {
        m_info.text = info;
    }
}
