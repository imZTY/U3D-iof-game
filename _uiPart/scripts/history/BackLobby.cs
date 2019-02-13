using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BackLobby : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// 给按钮添加事件
		Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(loadLobby);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void loadLobby() {
		// 跳转到游戏大厅
	    SceneManager.LoadScene("lobby");
	}
}
