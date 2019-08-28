using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class songVolume : MonoBehaviour {

    public AudioSource audioBgm;
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
        string txtName;
        txtName = name + " Audio";
        Debug.Log("txtName " + txtName);
        volumeState myVlume = new volumeState();
        myVlume.volume = audioBgm.volume;
        //將myPlayer轉換成json格式的字串
        string saveString = JsonUtility.ToJson(myVlume);
        //將字串saveString存到硬碟中
        StreamWriter file = new StreamWriter(System.IO.Path.Combine(Application.streamingAssetsPath, txtName));
        file.Write(saveString);
        file.Close();
    }
    public class volumeState
    {
        public float volume;
    }
}
