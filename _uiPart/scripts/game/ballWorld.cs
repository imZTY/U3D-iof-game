using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ballWorld : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// 给按钮添加事件
		Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(enterWorld);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void enterWorld(){
		// 跳转到滚球世界
	    SceneManager.LoadScene("ballWorld");
	}
}
