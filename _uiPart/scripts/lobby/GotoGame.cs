using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GotoGame : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// 给按钮添加事件
		Button btn = gameObject.GetComponent<Button>();
        btn.onClick.AddListener(loadGame);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void loadGame() {
		GameObject.Find("ChatInputField").GetComponent<ChatController>().socketClient.Close();
		// 跳转到游戏主体
	    SceneManager.LoadScene("game");
	}
}
