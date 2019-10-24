using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class songVolume : MonoBehaviour {

    public AudioSource audioBgm;
    int listNumber = 0;
    string[] songList = new string[]{
        "butterfly" ,"Don't say lazy" ,"Im sorry" ,"LATATA" ,"LOVE" ,"Mirotic" ,"Oh!" ,"One Night In 北京" ,"PON PON PON" ,"Roly Poly" ,"SORRY SORRY" ,"Trouble Maker" ,"Tunak Tunak Tun" ,
        "YES or YES" ,"三國戀" ,"千年之戀" ,"不得不愛" ,"月牙灣" ,"回レ! 雪月花" ,"我不配" ,"我還年輕 我還年輕" ,"牡丹江" ,"東區東區" ,"直感" ,"星空" ,"夏祭り" ,"恋" ,"恋は渾沌の隷也" ,
        "恋愛サーキュレーション" ,"夠愛" ,"將軍令" ,"華陽炎" ,"極楽浄土" ,"憂愁" ,"憨人" ,"樹枝孤鳥" ,"Burn It Down" ,"Counting Stars" ,"Good Time" ,"I Really Like You" ,"Maps" ,"One More Night" ,
        "Poker Face" ,"Thunder" ,"What Ive Done" ,"What Makes You Beautiful"
    };
    // Use this for initialization
    void Start () {
        this.GetComponent<Button>().onClick.AddListener(SettingChangeClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SettingChangeClick()
    {
        setVolume();
    }
    void setVolume()
    {
        string name;
        name = transform.name;
        for (int i = 1; i <= songList.Length; i++)
        {
            if (string.Compare(songList[i-1], name) == 0)
            {
                listNumber = i;
                name = "song" + listNumber.ToString("D3");
                Debug.Log("songName: " + name);
                break;
            }
        }      
        string txtName;
        txtName = name + " Audio";
        Debug.Log("txtName " + txtName);
        volumeState myVlume = new volumeState();
        myVlume.volume = audioBgm.volume;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(myVlume);
        //將字串saveString存到硬碟中
        #if UNITY_EDITOR
        string filePath = System.IO.Path.Combine(Application.streamingAssetsPath, txtName);
        Debug.Log("filePath:" + filePath);
#elif UNITY_ANDROID
            string filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", txtName);

#endif

        StreamWriter file = new StreamWriter(filePath);
        file.Write(saveString);
        file.Close();
    }
    public class volumeState
    {
        public float volume;
    }
}
