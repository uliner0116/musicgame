using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour {

    // Use this for initialization
    GameObject father_gameObject;   //宣告一個GameObject(用來放取得的父物件)。

    void Start()
    {    //一開始就執行。

        father_gameObject = gameObject.transform.parent.gameObject;
        father_gameObject = father_gameObject.transform.parent.gameObject;
        father_gameObject = father_gameObject.transform.parent.gameObject;

        //宣告的物件 = 取得本身的父物件。



        Debug.Log(father_gameObject.name);   //Debug父物件的名稱。

    }

    // Update is called once per frame
    void Update () {
		
	}
}
