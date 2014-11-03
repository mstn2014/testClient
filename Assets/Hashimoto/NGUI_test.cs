using UnityEngine;
using System.Collections;

public class NGUI_test : MonoBehaviour {


	private UILabel label;
	private GameObject Panel_parent;
	private GameObject fontPrefab;

	// Use this for initialization
	void Start () {
		Panel_parent = GameObject.Find ("UI Root (2D)/Camera/Anchor/Panel");
		label = new GameObject("label").AddComponent<UILabel> ();
		label.transform.parent = Panel_parent.transform;
		fontPrefab = (GameObject)GameObject.Instantiate(Resources.Load("SEGA Font",typeof (GameObject)));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void UIFont_test(){
		//label.GetComponent<UIFont> ().
		label.text = "セガ";
	}

}
