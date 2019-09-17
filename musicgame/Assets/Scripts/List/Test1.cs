using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Test1 : MonoBehaviour {

    // Use this for initialization
    GameObject father_gameObject;   //宣告一個GameObject(用來放取得的父物件)。
    string songName;
    int listNumber = 0;
    string[] songList = new string[]{
            "butterfly" ,"Don't say lazy" ,"Im sorry" ,"LATATA" ,"LOVE" ,"Mirotic" ,"Oh!" ,"One Night In 北京" ,"PON PON PON" ,"Roly Poly" ,"SORRY SORRY" ,"Trouble Maker" ,"Tunak Tunak Tun" ,
             "YES or YES" ,"三國戀" ,"千年之戀" ,"不得不愛" ,"月牙灣" ,"回レ! 雪月花" ,"我不配" ,"我還年輕 我還年輕" ,"牡丹江" ,"東區東區" ,"直感" ,"星空" ,"夏祭り" ,"恋" ,"恋は渾沌の隷也" ,
             "恋愛サーキュレーション" ,"夠愛" ,"將軍令" ,"華陽炎" ,"極楽浄土" ,"憂愁" ,"憨人" ,"樹枝孤鳥"
        };

    void Start()
    {    //一開始就執行。

       
        //宣告的物件 = 取得本身的父物件。

        for(int i=1;i<= songList.Length; i++)
        {
                listNumber = i;
                songName = "song" + listNumber.ToString("D3");
                Debug.Log("do list songName: " + songName);

            updateMaxScore(0);
        }

    }
 void updateMaxScore(int score)
        {
            songState mySong = new songState();
            mySong.name = songName;
            mySong.score = score;
            //將myPlayer轉換成json格式的字串
            string saveString = JsonUtility.ToJson(mySong);
        //將字串saveString存到硬碟中
        System.IO.StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, songName));
            file.Write(saveString);
            file.Close();
        }
    public class songState
    {
        public string name;
        public int score;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
