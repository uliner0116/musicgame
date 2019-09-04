using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
public class gameovercanvas : MonoBehaviour
{

    // Use this for initialization
    public Text finalscore;
    public Text finalcombo;
    public Text HighScore;
    public Text perfectP;
    public Text greatP;
    public Text goodP;
    public Text badP;
    public Text missP;
    public Slider ScoreStrip;
    public Slider HighScoreStrip;

    int score;
    int combo;
    float prefectpersent;
    float greattpersent;
    float goodpersent;
    float badpersent;
    float misspersent;
    int noteQuantity;
    int maxScore;
    int myBestScore=500;
    int songNumber;
    string songName;
    void Start()
    {
        
        
        score = GameObject.FindObjectOfType<Game.SceneController>().score;
        songName = GameObject.FindObjectOfType<Game.SceneController>().songName;
        songNumber = songList.GetSongNumber(songName);
        load();
        Debug.Log("myBestScore:" + myBestScore);
        if (myBestScore == 0)
        {
            updateMaxScore(score);
        }
        load();
        //級距%數
        noteQuantity = GameObject.FindObjectOfType<Game.SceneController>().noteQuantity;
        prefectpersent = ((float)GameObject.FindObjectOfType<Game.SceneController>().perfectNum / (float)noteQuantity) * 100;       
        greattpersent = ((float)GameObject.FindObjectOfType<Game.SceneController>().greatNum / (float)noteQuantity) * 100;
        goodpersent = ((float)GameObject.FindObjectOfType<Game.SceneController>().goodNum / (float)noteQuantity) * 100;
        badpersent = ((float)GameObject.FindObjectOfType<Game.SceneController>().badNum / (float)noteQuantity) * 100;
        misspersent = ((float)GameObject.FindObjectOfType<Game.SceneController>().missNum / (float)noteQuantity) * 100;

        //分數條
        maxScore = GameObject.FindObjectOfType<Game.SceneController>().maxScore;//本曲上限分數 
        //顯示分數條
        ScoreStrip.maxValue = maxScore;
        ScoreStrip.value = score;
        //顯示
        finalscore.text = string.Format("Score: {0}", score);
        Debug.Log("Score:"+ score);
        combo = GameObject.FindObjectOfType<Game.SceneController>().maxCombo;
        
        finalcombo.text = string.Format("Combo: {0}", combo);
        Debug.Log("Combo:" + combo);
        string perfectText = "Perfect:" + prefectpersent.ToString("#0.0") + "%";
        Debug.Log("perfectText:" + perfectText);
      //  HighScore.text = string.Format("HighScore: {0}", myBestScore);
        perfectP.text = string.Format(perfectText);
        greatP.text = string.Format("Great:"+ greattpersent.ToString("#0.0") + "%");
        goodP.text = string.Format("Good:"+ goodpersent.ToString("#0.0") + "%");
        badP.text = string.Format("Bad:"+ badpersent.ToString("#0.0") + "%");
        missP.text = string.Format("Miss:"+ misspersent.ToString("#0.0") + "%");

        //最高紀錄分數條
        HighScoreStrip.maxValue = maxScore;
        HighScoreStrip.value = myBestScore;


      //  Debug.Log("HighScore:" + myBestScore);
        Debug.Log("Perfect:" + prefectpersent.ToString("#0.0"));
        Debug.Log("Great:" + greattpersent.ToString("#0.0"));
        Debug.Log("Good:" + goodpersent.ToString("#0.0"));
        Debug.Log("Bad:" + badpersent.ToString("#0.0"));
        Debug.Log("Miss:" + misspersent.ToString("#0.0"));

        updateMaxScore(score);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void updateMaxScore(int score)
    {
        songState mySong = new songState();
        mySong.name = songName;
        mySong.score = score;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(mySong);
        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, songName));
        file.Write(saveString);
        file.Close();
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
        myBestScore = loadData.score;
    }
}