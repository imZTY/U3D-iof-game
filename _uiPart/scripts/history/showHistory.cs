using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class showHistory : MonoBehaviour {

	public GameObject Row_Prefab;//表头预设

	// Use this for initialization
	void Start () {
		for (int i = 0; i < 3; i++)//添加并修改预设的过程，将创建10行
        {
            //在Table下创建新的预设实例
            GameObject table = GameObject.Find("Canvas/his_TextTable/table_history");
            GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
            row.name = "row" + (i + 1);
            row.transform.SetParent(table.transform);
            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
            //设置预设实例中的各个子物体的文本内容
            row.transform.FindChild("Time").GetComponent<Text>().text = DateTime.Now.ToString();
            row.transform.FindChild("Valid").GetComponent<Text>().text = "无效";
            row.transform.FindChild("Point").GetComponent<Text>().text = "5 / 7";
            row.transform.FindChild("Score").GetComponent<Text>().text = "10";
            row.transform.FindChild("Model").GetComponent<Text>().text = "经典模式";
            row.transform.FindChild("Detail").GetComponent<Text>().text = "[{[\"13\",\"2:20:22\"]},{[\"42\",\"3:02:43\"]}]";
        }
        StartCoroutine(GetHistory("https://zengtianyi.top/iof/record/single/player?id=" + 1));
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Get
	IEnumerator GetHistory(string url) 
	{
		WWW www = new WWW (url);
		yield return www;
		
		if (www.error != null) 
		{
			Debug.Log("error is :"+ www.error);
		} 
		else 
		{
			// Debug.Log("request result :" + www.text);
			// 处理json数据
			string[][] perRow = dealResult(www.text);
			// 遍历显示每一行数据
			for (int i = 0; i < perRow.Length; i++)
			{
				//在Table下创建新的预设实例
	            GameObject table = GameObject.Find("Canvas/his_TextTable/table_history");
	            GameObject row = GameObject.Instantiate(Row_Prefab, table.transform.position, table.transform.rotation) as GameObject;
	            row.name = "row" + (i + 1);
	            row.transform.SetParent(table.transform);
	            row.transform.localScale = Vector3.one;//设置缩放比例1,1,1，不然默认的比例非常大
	            //设置预设实例中的各个子物体的文本内容
	            row.transform.FindChild("Time").GetComponent<Text>().text = perRow[i][0];
	            row.transform.FindChild("Valid").GetComponent<Text>().text = perRow[i][1];
	            row.transform.FindChild("Point").GetComponent<Text>().text = perRow[i][2];
	            row.transform.FindChild("Score").GetComponent<Text>().text = perRow[i][3];
	            row.transform.FindChild("Model").GetComponent<Text>().text = perRow[i][4];
	            row.transform.FindChild("Detail").GetComponent<Text>().text = perRow[i][5];
			}
		}
	}

	private string[][] dealResult(string input){
		string data_head = "\"data\":[{";
		string data_tail = "}]}";

		// 截获data数据模块
		int IndexofA = input.IndexOf(data_head);
		int IndexofB = input.IndexOf(data_tail, IndexofA + data_head.Length);
		string data = input.Substring(IndexofA + data_head.Length, IndexofB - IndexofA - data_head.Length);
		Debug.Log(data);

		// 分割data数据模块
		string[] list = Regex.Split(data,"[^\\]]},{[^\\[]",RegexOptions.IgnoreCase); /* 注意！这里会导致首尾缺失一个 " 需要适当处理 */
		string[][] rt = new string[list.Length][];

		// Debug.Log(list.Length + "\n" + list[0]);
		// 遍历处理，提取data子内容
		for (int i = 0; i < list.Length; i++)
		{
			rt[i] = dealDTO(list[i] + "\""); // 补上末尾缺失的 "
		}

		return rt;
	}

	private string[] dealDTO(string data){
		// 结束标志
		string str_stringLast = "\"";
        string str_intLast = ",";
        string str_jsonListLast = "]\",";

  		// Time
  		string time_head = "\"dateStr\":\"";
        int time_IndexofA = data.IndexOf(time_head);
		int time_IndexofB = data.IndexOf(str_stringLast, time_IndexofA + time_head.Length);
		string time = data.Substring(time_IndexofA + time_head.Length, time_IndexofB - time_IndexofA - time_head.Length);
		
		// Valid
		string valid_head = "\"valid\":";
        int valid_IndexofA = data.IndexOf(valid_head);
		int valid_IndexofB = data.IndexOf(str_intLast, valid_IndexofA + valid_head.Length);
		string valid = data.Substring(valid_IndexofA + valid_head.Length, valid_IndexofB - valid_IndexofA - valid_head.Length);

		// Point
		string point_son = "\"finishPoint\":";
        int son_IndexofA = data.IndexOf(point_son);
		int son_IndexofB = data.IndexOf(str_intLast, son_IndexofA + point_son.Length);
		string son = data.Substring(son_IndexofA + point_son.Length, son_IndexofB - son_IndexofA - point_son.Length);
		string point_mom = "\"wholePoint\":";
        int mom_IndexofA = data.IndexOf(point_mom);
		int mom_IndexofB = data.IndexOf(str_intLast, mom_IndexofA + point_mom.Length);
		string mom = data.Substring(mom_IndexofA + point_mom.Length, mom_IndexofB - mom_IndexofA - point_mom.Length);
		string point = son + " / " + mom;

		// Score
		string score_head = "\"score\":";
        int score_IndexofA = data.IndexOf(score_head);
		int score_IndexofB = data.IndexOf(str_intLast, score_IndexofA + score_head.Length);
		string score = data.Substring(score_IndexofA + score_head.Length, score_IndexofB - score_IndexofA - score_head.Length);

		// Model
		string model_head = "\"model\":";
        int model_IndexofA = data.IndexOf(model_head);
		int model_IndexofB = data.IndexOf(str_intLast, model_IndexofA + model_head.Length);
		string model_code = data.Substring(model_IndexofA + model_head.Length, model_IndexofB - model_IndexofA - model_head.Length);
		string model = null;
		if(model_code == "0")
		{
			model = "经典模式";
		}else{
			model = "其他模式";
		}

		// Detail
		string detail_head = "\"detailJson\":\"";
        int detail_IndexofA = data.IndexOf(detail_head);
		int detail_IndexofB = data.IndexOf(str_jsonListLast, detail_IndexofA + detail_head.Length) + 1;
		Debug.Log(data + "\n" + detail_IndexofA + "  " + detail_IndexofB);
		string detail = data.Substring(detail_IndexofA + detail_head.Length, detail_IndexofB - detail_IndexofA - detail_head.Length);

		return new string[] { time, valid, point, score, model, detail };
	}
}
