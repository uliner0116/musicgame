using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapHome : MonoBehaviour
{
    TapGetter tapGetter;
    Dictionary<GameObject, CheckTiming> toCheckTiming = new Dictionary<GameObject, CheckTiming>();

    private void Awake()
    {
        tapGetter = GetComponent<TapGetter>();
    }

    private void Start()
    {
        //Line增加的時候需要追加
        toCheckTiming.Add(GameObject.Find("NodeLine1"), GameObject.Find("NodeLine1/TapPosition").GetComponent<CheckTiming>());
        toCheckTiming.Add(GameObject.Find("NodeLine2"), GameObject.Find("NodeLine2/TapPosition").GetComponent<CheckTiming>());
        toCheckTiming.Add(GameObject.Find("NodeLine3"), GameObject.Find("NodeLine3/TapPosition").GetComponent<CheckTiming>());
    }

    // Update is called once per frame
    void Update()
    {
        Dictionary<GameObject, TouchPhase> tapinfo = tapGetter.OnTouchPhase();
        foreach(var key in tapinfo.Keys)
        {
            Debug.Log("Touch home");
            if(key.name == "TapObject")
            {
                var line = key.transform.parent.gameObject;
                toCheckTiming[line].Tap();
            }
        }
    }
}
