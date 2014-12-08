using UnityEngine;
using System.Collections;

public class Rank_ver2 : MonoBehaviour {

	private GameObject	m_bg;
	private GameObject	m_label;

	private Camera		m_camera;
	// Use this for initialization
	void Start () {
		m_bg = transform.FindChild ("BG").gameObject as GameObject;
		m_label = transform.FindChild ("Label").gameObject as GameObject;

		m_bg.transform.localScale = new Vector3 (2f,0.5f,1f);

		m_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		// lookatでカメラのほうを見るようにする
		m_camera = Camera.main;
		Vector3 look_at = (m_camera.transform.position - m_bg.transform.position);

		//Vector3 look_at =  new Vector3(-m_camera.transform.position.x + m_bg.transform.position.x,-m_camera.transform.position.y + m_bg.transform.position.y,m_bg.transform.position.z);//Vector3 look_at = (m_camera.transform.position - transform.position);
		m_bg.transform.LookAt(look_at);
	}


	void OnGUI(){
		GUI.Label (new Rect (0,0,100,30), "test");
	}
}
