using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    // 記得加這行

public class ChangeNote : MonoBehaviour
{
    public Text noteNumberText;
    int NoteNumber = 1;
    public string name;

    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(noteChangeClick);
    }

    void Update()
    {
     
    }
    public void noteChangeClick()
    {
        NoteNumber = int.Parse(noteNumberText.text);
        if (string.Compare(name, "right") == 0 )
        {
           
            NoteNumber++;
            if (NoteNumber > 4)
            {
                NoteNumber = 1;
            }
            noteNumberText.text = "" + NoteNumber; ;
           
        }else if (string.Compare(name, "left") == 0)
        {
  
            NoteNumber--;
            if (NoteNumber <1)
            {
                NoteNumber = 4;
            }
            noteNumberText.text = "" + NoteNumber; ;
        }
    }
}
