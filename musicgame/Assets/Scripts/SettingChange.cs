using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingChange : MonoBehaviour
{

    // public GameObject MusicPlayer;
    public AudioSource audioBgm;
    public AudioSource audioNote;
    public Text noteNumberText;
    int noteNumber = 0;
    public AudioSource[] notePlayer;

    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(SettingChangeClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SettingChangeClick()
    {
        setVolume("audioBgm", audioBgm);
        setVolume("audioNote", audioNote);
        setNote("notePlay");
     //   SceneManager.LoadScene("Menu");
    }

    void setVolume(string Name, AudioSource audio)
    {
        volumeState myVlume = new volumeState();
        myVlume.volume = audio.volume;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(myVlume);
        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, Name));
        file.Write(saveString);
        file.Close();
    }
    void setNote(string Name)
    {
        noteState myNote = new noteState();
        noteNumber = int.Parse(noteNumberText.text);
        int playNumber = noteNumber - 1;
        myNote.noteAudio = notePlayer[playNumber].clip;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(myNote);
        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, Name));
        file.Write(saveString);
        file.Close();
    }
    public class volumeState
    {
        public float volume;
    }
    public class noteState
    {
        public AudioClip noteAudio = null;
    }
}
