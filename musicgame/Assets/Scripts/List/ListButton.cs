using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListButton : MonoBehaviour
{
    int line;
    string songName;
    string level;
    string lineName;
    TextAsset songTxt;
    GameObject father_gameObject;   //宣告一個GameObject(用來放取得的父物件)。
    //歌名對照表
    int listNumber=0;
    string[] songList = new string[]{
        "butterfly" ,"Don't say lazy" ,"Im sorry" ,"LATATA" ,"LOVE" ,"Mirotic" ,"Oh!" ,"One Night In 北京" ,"PON PON PON" ,"Roly Poly" ,"SORRY SORRY" ,"Trouble Maker" ,"Tunak Tunak Tun" ,
        "YES or YES" ,"三國戀" ,"千年之戀" ,"不得不愛" ,"月牙灣" ,"回レ! 雪月花" ,"我不配" ,"我還年輕 我還年輕" ,"牡丹江" ,"東區東區" ,"直感" ,"星空" ,"夏祭り" ,"恋は渾沌の隷也" ,
        "恋愛サーキュレーション" ,"夠愛" ,"將軍令" ,"華陽炎" ,"極楽浄土" ,"憂愁" ,"憨人" ,"樹枝孤鳥" , "恋"
    };

    //點擊時呼叫
    public void OnPathClick()
    {
        
        //取得這首歌的名稱
        father_gameObject = gameObject.transform.parent.gameObject;
        father_gameObject = father_gameObject.transform.parent.gameObject;
        //line在兩層後 設定line相關資訊
        lineName = father_gameObject.name;
        if (string.Compare(lineName, "Line3") == 0)
        {
            line = 3;
        } else if (string.Compare(lineName, "Line6") == 0)
        {
            line = 6;
        }
        Debug.Log("line: " + line);
        father_gameObject = father_gameObject.transform.parent.gameObject;
        //song在三層後 設定音樂名稱
        songName = father_gameObject.name;
        for (int i=1;i<= songList.Length; i++)
        {
           if( string.Compare(songList[i-1], songName) == 0)
            {
                listNumber = i;
                songName = "song"+listNumber.ToString("D3");
                Debug.Log("songName: " + songName);
                break;
            }
        }

        level = transform.name;
        Debug.Log("level!" + level);
        
        // Debug.Log(Path.GetExtension("E:\\user\\Desktop\\musicgame\\musicgame\\musicgame\\Assets\\Audios\\cAudio\\Don't say lazy-cut.mp3"));      
        // Game.SceneController.songDataAsset = Resources.Load<TextAsset>(noteTxt);

        /*if (Path.GetExtension(transform.name) == ".mp3" ||
            Path.GetExtension(transform.name) == ".aif" ||
            Path.GetExtension(transform.name) == ".wav" ||
            Path.GetExtension(transform.name) == ".ogg")
        {*/
        Debug.Log("in");
        //string audio = "file://" + transform.name;
        //string audio = "Audios/cAudio/回レ! 雪月花cut.mp3";
        //WWW www = new WWW(audio);
        //Debug.Log("www:" + www.url);
        //string noteTxt = "file://" + transform.name;
        //string noteTxt = "notetxt/回レ! 雪月花.txt";

        //if()
        //while (!www.isDone) { }
        //Game.SceneController.songDataAsset= Resources.Load<TextAsset>(noteTxt);
        //songData.audio = www.GetAudioClip();
        songData.audio = Resources.Load<AudioClip>("Audios/cAudio/" + songName);
        Debug.Log("audio: " + songData.audio);
        if (line == 3)
        {
            songTxt = Resources.Load<TextAsset>("3linetxt/" + songName + "/" + songName + "-" + level);
            if (songTxt == null)
            {
                Debug.Log("not found this level!");
                //this.gameObject.SetActive(false);
                //按化處理
            }
            else {
                songData.Line3SongDataAsset = Resources.Load<TextAsset>("3linetxt/" + songName + "/" + songName + "-" + level);
                Debug.Log("Line3SongDataAsset:" + songData.Line3SongDataAsset.name);
                // Debug.Log("notetxt:" + songData.);
            }

        } else if (line == 6)
        {
            songTxt = Resources.Load<TextAsset>("6linetxt/" + songName + "/" + songName + "-" + level);
            if (songTxt == null)
            {
                Debug.Log("not found this level!");
                //this.gameObject.SetActive(false);
                //按化處理
            }
            else
            {
                songData.Line6SongDataAsset = Resources.Load<TextAsset>("6linetxt/" + songName + "/" + songName + "-" + level);
                Debug.Log("Line6SongDataAsset:" + songData.Line6SongDataAsset.name);
            }
        }
        Debug.Log("audio:" + songData.audio.name);

        //轉跳sence
        if (songTxt != null)
        {
            if (line == 3)
            {
                SceneManager.LoadScene("Game 3ver");
            }
            else if (line == 6)
            {
                SceneManager.LoadScene("Game 6ver");
            }
        }

        //}
    }
    // Use this for initialization
    void Start()
    {
        /*
        //取得這首歌的名稱
        father_gameObject = gameObject.transform.parent.gameObject;
        father_gameObject = father_gameObject.transform.parent.gameObject;
        //line在兩層後 設定line相關資訊
        lineName = father_gameObject.name;
        if (string.Compare(lineName, "Line3") == 0)
        {
            line = 3;
        }
        else if (string.Compare(lineName, "Line6") == 0)
        {
            line = 6;
        }
        Debug.Log("line: " + line);
        father_gameObject = father_gameObject.transform.parent.gameObject;
        //song在三層後 設定音樂名稱
        songName = father_gameObject.name;
        Debug.Log("songName" + songName);

        level = transform.name;
        Debug.Log("level!" + level);
        if (line == 3)
        {
            songTxt = Resources.Load<TextAsset>("3linetxt/" + songName + "/" + songName + "-" + level);
        }else if (line == 6)
        {
            songTxt = Resources.Load<TextAsset>("6linetxt/" + songName + "/" + songName + "-" + level);
        }
        if (songTxt == null)
        {
            this.gameObject.SetActive(false);
        }
        */
            this.GetComponent<Button>().onClick.AddListener(OnPathClick);

    }

            // Update is called once per frame
            void Update()
    {

    }
}