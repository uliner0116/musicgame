using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListControll : MonoBehaviour
{

    public RectTransform[] pages;
    public Vector2[] targetPosition;

    int current = 0;
    bool locked = false;
    Vector2 sampleSize;

    void Start()
    {
        sampleSize = GetComponent<RectTransform>().sizeDelta;
    }


    void Update()
    {
       // if (Input.GetKeyDown(KeyCode.RightArrow))
       // {
        //    Next();
       // }
       // else if (Input.GetKeyDown(KeyCode.LeftArrow))
       // {
       //     Previous();
       // }
        Refresh();
    }

    public void Next()
    {
        if (locked)
            return;

        targetPosition[current].x = 1920f;

        current++;
        if (current >= pages.Length)
            current = 0;

        pages[current].anchoredPosition = new Vector2(-1920f, 0);
        targetPosition[current].x = 0;
        StartCoroutine("Lock");
    }
    public void Previous()
    {
        if (locked)
            return;

        targetPosition[current].x = -1920f;

        current--;
        if (current < 0)
            current = pages.Length - 1;

        pages[current].anchoredPosition = new Vector2(1920f, 0);
        targetPosition[current].x = 0;
        StartCoroutine("Lock");
    }

    void Refresh()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            Vector3 pos = pages[i].anchoredPosition;
            pos = Vector3.Lerp(pos, targetPosition[i], 0.2f);
            pages[i].anchoredPosition = pos;
        }
    }

    IEnumerator Lock()
    {
        locked = true;
        yield return new WaitForSeconds(0.2f);
        locked = false;
    }

}
