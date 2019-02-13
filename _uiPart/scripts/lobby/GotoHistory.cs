using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoHistory : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// 给按钮添加事件
		Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(loadHistory);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void loadHistory() {
		GameObject.Find("ChatInputField").GetComponent<ChatController>().socketClient.Close();
		// 跳转到历史战绩
	    SceneManager.LoadScene("history");
	}
}
