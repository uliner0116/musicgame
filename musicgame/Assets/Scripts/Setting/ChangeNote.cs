using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;    // 記得加這行

public class ChangeNote : MonoBehaviour
{

    int NoteNumber = 0;

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            NoteNumber = NoteNumber + 1;    // 也能簡寫為 a++; 或 a+=1;
            GetComponent<Text>().text = "" + NoteNumber;    // 前面加空字串，是為了把 整數a 轉為 字串。
        }
    }
}
