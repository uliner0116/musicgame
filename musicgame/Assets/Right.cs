using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Right : MonoBehaviour
{

    void Start()
    {
        //按下按鈕時，呼叫ClickEvent()
        this.GetComponent<Button>().onClick.AddListener(ClickEvent);
    }

    void ClickEvent()
    {
        
    }
}
