using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
* 独立弹窗未实现
*/
public class Begin : MonoBehaviour {

	GameObject Panel;

	// Use this for initialization
	void Start () {
		Panel = GameObject.Find("Panel");
		Panel.SetActive(false);
		// 给按钮添加事件
		Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(test);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void test()
	{
		Debug.Log(GameObject.Find("data").GetComponent<PersistentData>().host);
		Panel.SetActive(true);
	}
}
