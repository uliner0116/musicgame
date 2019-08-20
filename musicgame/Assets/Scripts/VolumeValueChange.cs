using UnityEngine;

public class VolumeValueChange : MonoBehaviour
{

    // Reference to Audio Source component
    public AudioSource audio;

    // Music volume variable that will be modified
    // by dragging slider knob
    private float Volume = 1f;
    // Use this for initialization
    void Start()
    {

        // Assign Audio Source component to control it
        //audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        // Setting volume option of Audio Source to be equal to musicVolume
        audio.volume = Volume;
        //audioNote.volume = NoteVolume;
    }

    // Method that is called by slider game object
    // This method takes vol value passed by slider
    // and sets it as musicValue
    public void SetVolume(float vol)
    {
        Volume = vol;
    }

    /*public void SetNoteVolume(float vol)
    {
        NoteVolume = vol;
    }*/
}