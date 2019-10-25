using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test1 : MonoBehaviour {

    // Use this for initialization
    GameObject father_gameObject;   //宣告一個GameObject(用來放取得的父物件)。
    string songName;
    int listNumber = 0;
    int isDone;
    string[] songList = new string[]{
        "butterfly" ,"Don't say lazy" ,"Im sorry" ,"LATATA" ,"LOVE" ,"Mirotic" ,"Oh!" ,"One Night In 北京" ,"PON PON PON" ,"Roly Poly" ,"SORRY SORRY" ,"Trouble Maker" ,"Tunak Tunak Tun" ,
        "YES or YES" ,"三國戀" ,"千年之戀" ,"不得不愛" ,"月牙灣" ,"回レ! 雪月花" ,"我不配" ,"我還年輕 我還年輕" ,"牡丹江" ,"東區東區" ,"直感" ,"星空" ,"夏祭り" ,"恋","恋は渾沌の隷也" ,
        "恋愛サーキュレーション" ,"夠愛" ,"將軍令" ,"華陽炎" ,"極楽浄土" ,"憂愁" ,"憨人" ,"樹枝孤鳥" ,"Burn It Down","Counting Stars","Good Time","I Really Like You","Maps","One More Night",
        "Poker Face","Thunder","What Ive Done","What Makes You Beautiful"
    };
    private string txtName;

    void Start()
    {    //一開始就執行。
        load();
        Debug.Log("isDone " + isDone);
        //宣告的物件 = 取得本身的父物件。
        if (isDone == 0)
        {
            for (int i = 1; i <= songList.Length; i++)
            {
                listNumber = i;
                songName = "song" + listNumber.ToString("D3");
                Debug.Log("do list songName: " + songName);
                txtName = songName + " Audio";

                //StreamReader file = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, txtName));

                //if (file == null)
                // {
                // Debug.Log("file null");
                updatevolumeState();
                //}

                // StreamReader file1 = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, songName));


                // if (file1 == null)
                // {
                // Debug.Log("file1 null");
                updateMaxScore(1000);
                //}          
            }
            updateinitialization();
        }
        load();
        Debug.Log("isDone " + isDone);
    }
    public class initialization
    {
        public int isDone;
    }
    public void load()
    {
        string loadJson;
        //讀取json檔案並轉存成文字格式
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, "initialization");
        StreamReader file = new StreamReader(filePath);
        loadJson = file.ReadToEnd();
        file.Close();


        //新增一個物件類型為playerState的變數 loadData
        initialization loadData = new initialization();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<initialization>(loadJson);

        //驗證用，將sammaru的位置變更為json內紀錄的位置
        isDone = loadData.isDone;
    }
    private void updateinitialization(){
        initialization initialization = new initialization();
        initialization.isDone = 1;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(initialization);
        //將字串saveString存到硬碟中
        System.IO.StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, "initialization"));
        file.Write(saveString);
        file.Close();
    }
 
    void updateMaxScore(int score)
        {
            songState mySong = new songState();
            mySong.name = songName;
            mySong.score = score;
            //將myPlayer轉換成json格式的字串
            string saveString = JsonUtility.ToJson(mySong);
        //將字串saveString存到硬碟中
#if UNITY_EDITOR
        System.IO.StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, songName));
#elif UNITY_ANDROID
         System.IO.StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, songName));

#endif

            file.Write(saveString);
            file.Close();
        }
    void updatevolumeState()
    {
        volumeState mySong1 = new volumeState();
        mySong1.volume = 1;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(mySong1);
        //將字串saveString存到硬碟中
        var mySongName = songName + " Audio";
#if UNITY_EDITOR
        System.IO.StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, mySongName));
#elif UNITY_ANDROID
         System.IO.StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.persistentDataPath, mySongName));

#endif
        file.Write(saveString);
        file.Close();
    }
    public class songState
    {
        public string name;
        public int score;
    }
    public class volumeState
    {
        public float volume;
    }
   

    // Update is called once per frame
    void Update () {
		
	}
}
