using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using UnityEditor.Sprites ;
public class Level : MonoBehaviour
{

    public GameObject page;
    public void Active_Text()
    {
        if (!page.activeInHierarchy)
        { page.SetActive(true); }
        else
        { page.SetActive(false); }
    }

}