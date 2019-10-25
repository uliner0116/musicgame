using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class TestScore : MonoBehaviour
{
    string[] songList = new string[]{
        "butterfly" ,"Don't say lazy" ,"Im sorry" ,"LATATA" ,"LOVE" ,"Mirotic" ,"Oh!" ,"One Night In 北京" ,"PON PON PON" ,"Roly Poly" ,"SORRY SORRY" ,"Trouble Maker" ,"Tunak Tunak Tun" ,
        "YES or YES" ,"三國戀" ,"千年之戀" ,"不得不愛" ,"月牙灣" ,"回レ! 雪月花" ,"我不配" ,"我還年輕 我還年輕" ,"牡丹江" ,"東區東區" ,"直感" ,"星空" ,"夏祭り" ,"恋","恋は渾沌の隷也" ,
        "恋愛サーキュレーション" ,"夠愛" ,"將軍令" ,"華陽炎" ,"極楽浄土" ,"憂愁" ,"憨人" ,"樹枝孤鳥" ,"Burn It Down","Counting Stars","Good Time","I Really Like You","Maps","One More Night",
        "Poker Face","Thunder","What Ive Done","What Makes You Beautiful"
    };
    public string songName;
    public Text maxScore;
    int listNumber = 0;
    bool isFull = true;
    int myBestScore = 0;

    public class songState
    {
        public string name;
        public int score;
    }
    public void load()
    {
        string loadJson;
        //讀取json檔案並轉存成文字格式
#if UNITY_EDITOR
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, songName);
        Debug.Log("filePath:" + filePath);
#elif UNITY_ANDROID
            //string filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", songName);
         StreamReader file = new StreamReader(System.IO.Path.Combine(Application.persistentDataPath, songName));

#endif

#if UNITY_EDITOR
        StreamReader file = new StreamReader(filePath);
        if(file != null)
        {
        loadJson = file.ReadToEnd();
        file.Close();
        isFull = false;
        }
        
#elif UNITY_ANDROID
        if(file != null){
                loadJson = file.ReadToEnd();
                file.Close();
            isFull = false;
            }
            /*WWW reader = new WWW (filePath);
            while (!reader.isDone) {
            }
            loadJson = reader.text;*/
#endif

        //新增一個物件類型為playerState的變數 loadData
        songState loadData = new songState();

        //使用JsonUtillty的FromJson方法將存文字轉成Json
        loadData = JsonUtility.FromJson<songState>(loadJson);

        //驗證用，將sammaru的位置變更為json內紀錄的位置
        myBestScore = loadData.score;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= songList.Length; i++)
        {
            if (string.Compare(songList[i - 1], songName) == 0)
            {
                listNumber = i;
                songName = "song" + listNumber.ToString("D3");
                Debug.Log("songName: " + songName);
                break;
            }
        }
        load();
        if(isFull == false)
        {
            maxScore.text = string.Format("maxScore:" + myBestScore);
        }
        else
        {
            maxScore.text = "null";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
