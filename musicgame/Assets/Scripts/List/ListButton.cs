using Common;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ListButton : MonoBehaviour {

    [SerializeField]
    AudioManager audioManager;
    //點擊時呼叫
    public void OnPathClick()
    {
        if(Path.GetExtension(transform.name) == ".mp3" ||
            Path.GetExtension(transform.name) == ".aif" ||
            Path.GetExtension(transform.name) == ".wav" ||
            Path.GetExtension(transform.name) == ".ogg")
        {
            string link = "file://" + transform.name;
            WWW www = new WWW(link);
            Debug.Log("www:" + www.url);

            //if()
        }
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
