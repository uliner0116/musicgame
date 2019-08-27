using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playNote : MonoBehaviour
{
    public Text noteNumberText;
    int noteNumber = 0;
    public AudioSource[] notePlayer;
    // Use this for initialization
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(notePlayClick);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void notePlayClick()
    {
        noteNumber = int.Parse(noteNumberText.text);
        Debug.Log("noteNumber"+ noteNumber);
        playNotes();
    }
    void playNotes()
    {
        int playNumber = noteNumber - 1;
        notePlayer[playNumber].Play();
    }
}
