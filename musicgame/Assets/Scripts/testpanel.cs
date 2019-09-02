using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class testpanel : MonoBehaviour
{

    public RectTransform[] pages;
    public Vector2[] targetPosition;
    int current = 0;
    bool locked = false;
    Vector2 sampleSize;

    public float min = 0.8f;
    public float max = 1.4f;

    public float speed = 0.2f;
    public float Alpha = 0.8f;

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
        foreach (var item in pages)
        {
            if (item != pages[current])
            {
                item.GetComponent<RectTransform>().localScale = new Vector3(min, min);
                item.GetComponent<Image>().color = new Color(1, 1, 1, Alpha);
            }
            else
            {
                item.GetComponent<RectTransform>().localScale = new Vector3(max, max);
            }
        }
    }

    public void Next()
    {
        if (locked)
            return;

        targetPosition[current].x = 250f;

        current++;
        if (current >= pages.Length)
            current = 0;

       //  pages[current].GetComponent<RectTransform>().anchoredPosition = new Vector2(-250f, 0);
        if (current < pages.Length)
        {
            foreach (var item in pages)
            {
                pages[current].GetComponent<RectTransform>().anchoredPosition = new Vector2(-250f, 0);
            }
           // pages[current].GetComponent<Image>().color = new Color(1, 1, 1, Alpha);
            //pages[current].GetComponent<RectTransform>().DOScale(min, speed);
            //current += 1;
            //pages[current].GetComponent<RectTransform>().DOScale(max, speed);
            //pages[current].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
        }
        targetPosition[current].x = 0;
        StartCoroutine("Lock");

    }
    public void Previous()
    {
        if (locked)
            return;

        targetPosition[current].x = -250f;

        current--;
        if (current < 0)
            current = pages.Length - 1;

        pages[current].anchoredPosition = new Vector2(250f, 0);
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
