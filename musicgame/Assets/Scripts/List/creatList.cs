using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;



public class creatList : MonoBehaviour {

    public string FloderPath = @"C:\";

    List<string> FolderList;
   

    Animator animator;
	// Use this for initialization
	void Start () {
		if(Application.platform == RuntimePlatform.Android)
        {

            FloderPath = "file:///android_asset/cAudio";
            ShowFolderWindow();
        }
    }
    public void ShowFolderWindow()
    {


        FolderList = Directory.GetDirectories(FloderPath).ToList();

        List<string> MP3List = Directory.GetFiles(FloderPath, "*.mp3").ToList();
        List<string> WAVList = Directory.GetFiles(FloderPath, "*.wav").ToList();
        List<string> OGGList = Directory.GetFiles(FloderPath, "*.ogg").ToList();
        List<string> AifList = Directory.GetFiles(FloderPath, "*.aif").ToList();
        //合併
        FolderList.AddRange(MP3List);
        FolderList.AddRange(WAVList);
        FolderList.AddRange(OGGList);
        FolderList.AddRange(AifList);

        //先清空sctowView
        for(int i = 0; i < transform.Find("Content").childCount; i++)
        {
            Destroy(transform.Find("Content").GetChild(i).gameObject);
        }

        //展示資料夾
        for(int i = 0; i < FolderList.Count; i++){
            //ScroView的排列
           // Vector3 nextBtnPos = new Vector3(0,- BtnPrefab.)
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}
