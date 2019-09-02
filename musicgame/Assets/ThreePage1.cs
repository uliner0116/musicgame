﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class ThreePage1 : MonoBehaviour
{
    public GameObject[] images;
    int currentIndex;
    public float DistancesNum = 250f;
    public float min = 0.8f;
    public float max = 1.4f;

    public float speed = 0.2f;
    public float Alpha = 0.8f;


    void Start()
    {
        InitRectTranform();
    }
    public void InitRectTranform()
    {
        currentIndex = images.Length / 2;
        for (int i = currentIndex + 1; i < images.Length; i++)
        {
            images[i].GetComponent<RectTransform>().anchoredPosition = new Vector3((i - currentIndex) * DistancesNum, -400f, 0);
        }
        int num = 0;
        for (int i = currentIndex; i >= 0; i--)
        {
            images[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-num * DistancesNum, -400f, 0);
            num++;
        }
        foreach (var item in images)
        {
            if (item != images[currentIndex])
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

    public void OnLeftButtonClick()
    {

        if (currentIndex < images.Length - 1)
        {
            foreach (GameObject item in images)
            {
                (item.GetComponent<RectTransform>()).DOAnchorPosX(item.GetComponent<RectTransform>().anchoredPosition.x - DistancesNum, speed);
            }
            images[currentIndex].GetComponent<Image>().color = new Color(1, 1, 1, Alpha);
            images[currentIndex].GetComponent<RectTransform>().DOScale(min, speed);
            currentIndex += 1;
            images[currentIndex].GetComponent<RectTransform>().DOScale(max, speed);
            images[currentIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            StartCoroutine("Lock");
        }
        else if(currentIndex >= images.Length-1)
        {
            currentIndex = 0;
            for (int i = currentIndex + 1; i < images.Length; i++)
            {
                images[i].GetComponent<RectTransform>().anchoredPosition = new Vector3((i - currentIndex) * DistancesNum, -400f, 0);
            }
            int num = 0;
            for (int i = currentIndex; i >= 0; i--)
            {
                images[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-num * DistancesNum, -400f, 0);
                num++;
            }
            foreach (var item in images)
            {
                if (item != images[currentIndex])
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
    }

    public void OnRightButtonClick()
    {


        if (currentIndex > 0)
        {
            foreach (GameObject item in images)
            {
                (item.GetComponent<RectTransform>()).DOAnchorPosX(item.GetComponent<RectTransform>().anchoredPosition.x + DistancesNum, speed);
            }
            images[currentIndex].GetComponent<RectTransform>().DOScale(min, speed);
            images[currentIndex].GetComponent<Image>().color = new Color(1, 1, 1, Alpha);
            currentIndex -= 1;
            images[currentIndex].GetComponent<RectTransform>().DOScale(max, speed);
            images[currentIndex].GetComponent<Image>().color = new Color(1, 1, 1, 1f);
            StartCoroutine("Lock");
        }
        else if(currentIndex <= 0)
        {
            currentIndex = images.Length - 1;
            for (int i = currentIndex + 1; i < images.Length; i++)
            {
                images[i].GetComponent<RectTransform>().anchoredPosition = new Vector3((i - currentIndex) * DistancesNum, -400f, 0);
            }
            int num = 0;
            for (int i = currentIndex; i >= 0; i--)
            {
                images[i].GetComponent<RectTransform>().anchoredPosition = new Vector3(-num * DistancesNum, -400f, 0);
                num++;
            }
            foreach (var item in images)
            {
                if (item != images[currentIndex])
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
    }
}