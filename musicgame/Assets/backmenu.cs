using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Common;
using Common.Data;
using System.Threading;
using System.Text.RegularExpressions;

public class backmenu : MonoBehaviour {
    public string bmenu;
    // Use this for initialization
    void Start () {
        GetComponent<Button>().onClick.AddListener(() => {
            ClickEvent();
        });
    }
    void ClickEvent()
    {
        //生產canvasPrefab
        Application.LoadLevel(bmenu);
    }
    
}
