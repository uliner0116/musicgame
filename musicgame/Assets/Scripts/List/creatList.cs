using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class creatList : MonoBehaviour
{

    public Button Btn_Prefab;
    public string FloderPath = @"C:\";

    List<string> FolderList;


    Animator animator;
    // Use this for initialization
    void Start()
    {
        FloderPath = "jar:file://" + Application.dataPath + "!/assets";
        Debug.Log(FloderPath);
        if (Application.platform == RuntimePlatform.Android)
        {
            FloderPath = "jar:file://" + Application.dataPath + "!/assets";
            //FloderPath = Resources.Load("Audios/cAudio").ToString();
            ShowFolderWindow();
        }
        if (Application.platform == RuntimePlatform.WindowsPlayer)
        {
            FloderPath = @"C:\";
            Debug.Log("windows");
            ShowFolderWindow();
        }
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            FloderPath = @"C:\";
            Debug.Log("windows");
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
        Debug.Log(FolderList.Count);
        //先清空sctowView
        for (int i = 0; i < transform.Find("Content").childCount; i++)
        {
            Destroy(transform.Find("Content").GetChild(i).gameObject);
        }

        //展示資料夾
        for (int i = 0; i < FolderList.Count; i++)
        {
            //ScroView的排列
            Vector3 nextBtnPos = new Vector3(200, -Btn_Prefab.GetComponent<RectTransform>().rect.height * i - Btn_Prefab.GetComponent<RectTransform>().rect.height / 2, 0);

            //產生按鈕
            Button path_btn = Instantiate(Btn_Prefab, transform.Find("Content"));
            path_btn.transform.position = nextBtnPos;
            //設定位置
            path_btn.transform.localPosition = nextBtnPos;

            path_btn.name = FolderList[i].ToString();
            //文字
            path_btn.GetComponentInChildren<TextMeshProUGUI>().text = Path.GetFileName(FolderList[i].ToString());
            Debug.Log(FolderList[i].ToString());

        }

        transform.Find("Content").GetComponent<RectTransform>().sizeDelta = new Vector2(transform.Find("Content").GetComponent<RectTransform>().sizeDelta.x,
                                                                                        Btn_Prefab.GetComponent<RectTransform>().rect.height * (FolderList.Count + 1));


    }

    // Update is called once per frame
    void Update()
    {

    }
}

