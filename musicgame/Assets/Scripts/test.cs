using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class test : MonoBehaviour {
    string songName= "千年之戀";
    int score;
   
    void updateMaxScore(int score)
    {
        songState mySong = new songState();
        mySong.name = "千年之戀";
        mySong.score = 0;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(mySong);
        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, mySong.name));
        file.Write(saveString);
        file.Close();
    }
    // Use this for initialization
    void Start () {
        //songName = GameObject.FindObjectOfType<Game.SceneController>().songName;
        //score = GameObject.FindObjectOfType<Game.SceneController>().score;
        //updateMaxScore(this.score);
        load();
        Debug.Log("myBestScore:" + score);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public class songState
    {
        public string name;
        public int score;
    }
    public void load()
    {
        //讀取json檔案並轉存成文字格式
        StreamReader file = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, songName));
        string loadJson = file.ReadToEnd();
        file.Close();

        //新增一個物件類型為playerState的變數 loadData
        songState loadData = new songState();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<songState>(loadJson);

        //驗證用，將sammaru的位置變更為json內紀錄的位置
        score = loadData.score;
    }
}
