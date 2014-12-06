using UnityEngine;
using System.Collections;

public class Rank : MonoBehaviour {

	private GameObject	m_model;
	private Model		m_modScript;

	private Camera		m_camera;
	private Vector3		start_nearZ, end_farZ;
	private Vector2		length;

	bool StartUp_flg;

	// 定数呼び出し
	RankingSetting	RANKING; 
	// Use this for initialization
	void Start () {}
	
	// Update is called once per frame
	void Update () {
		Vector3 work_pos, work_pos2;
		work_pos = m_model.transform.position;
		if (StartUp_flg) {
			// 深度に合わせて大きさを変える
			transform.localScale = new Vector3(RANKING.RANKSCALL+(RANKING.RANKSCALL*(end_farZ.z-work_pos.z)/ length.y),RANKING.RANKSCALL+(RANKING.RANKSCALL*(end_farZ.z-work_pos.z)/ length.y),RANKING.RANKSCALL+(RANKING.RANKSCALL*(end_farZ.z-work_pos.z)/ length.y));
			// 座標
			work_pos2 = m_model.transform.FindChild("LabelPos").transform.position;
			work_pos2 = m_camera.WorldToViewportPoint(work_pos2);
			// 深度に合わせて高さを微調整
			transform.localPosition = new Vector3(work_pos2.x*1280-1280/2, work_pos2.y*800-880/2+(RANKING.RANKPOS_COM*(work_pos.z-start_nearZ.z)/ length.y), work_pos2.z);
		
		}
	}

	public void Init(GameObject pair, Vector3 start_point, Vector3 end_point, Vector2 len){
		// 対となるモデルのデータ&求めたデータを引き継ぐ
		m_model = pair.gameObject as GameObject;
		m_modScript = pair.GetComponent<Model>();
		start_nearZ = start_point;
		end_farZ = end_point;
		length = len;

		RANKING = Resources.Load<RankingSetting> ("Setting/RankingSetting");
		m_camera = Camera.main;
		StartUp_flg = true;
	}
}
