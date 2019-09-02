using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ListControll : MonoBehaviour
{

    public RectTransform[] pages;
    public Vector2[] targetPosition;
    public AudioSource audioBgm;
    int current = 10;
    bool locked = false;
    Vector2 sampleSize;

    void Start()
    {      
        audioBgm.Play(30);
        audioBgm.volume = 0.5f;
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
        audioBgm.clip = Resources.Load<AudioClip>("Audios/cAudio/" + pages[current].name);
        targetPosition[current].x = 0;
        StartCoroutine("Lock");
        audioBgm.Play(30);
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
        audioBgm.clip = Resources.Load<AudioClip>("Audios/cAudio/" + pages[current].name);
        targetPosition[current].x = 0;
        StartCoroutine("Lock");
        audioBgm.Play(30);
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
