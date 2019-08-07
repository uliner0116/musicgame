using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListButton : MonoBehaviour
{

    //點擊時呼叫
    public void OnPathClick()
    {
        Debug.Log("click!" + transform.name);
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
        songData.audio = Resources.Load<AudioClip>("Audios/cAudio/"+ transform.name);
        songData.songDataAsset = Resources.Load<TextAsset>("notetxt/"+ transform.name+"/"+ transform.name);
        Debug.Log("audio:" + songData.audio.name);
        Debug.Log("songDataAsset:" + songData.songDataAsset.name);
        SceneManager.LoadScene("Game");
        //}
    }
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OnPathClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
}