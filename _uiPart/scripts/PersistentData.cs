using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/**
@author Tianyi
@time 2019/01/30 14:29

持久数据类，数据不会因场景跳转而销毁
*/
public class PersistentData : MonoBehaviour {

	// 此端的玩家名称
	public string userName="";

	// 此端的玩家 id
	public int id=0;

	// socket 服务主机的 ip
	public string host = "118.24.117.188"; 

	// 聊天室 socket 的服务端口
	public int ChatPort = 7788;

	// 对战的 socket 服务端口
	public int FightPort = 8877;

	// 原本想记录缓存的消息记录，但没有用上
	public string refresh_chat="";

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}

	// Update is called once per frame
	void Update () {
		//DontDestroyOnLoad(gameObject); // 放上面也行
	}


}