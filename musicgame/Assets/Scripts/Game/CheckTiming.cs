using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.CameraEditor;

public class CheckTiming : MonoBehaviour{ 

    public Settings settings;
    public TapLight tapLight;

    public int perfectCheck;
    public int greatCheck;
    public int niceCheck;
    public int badCheck;


    public Dictionary<string, List<GameObject>> Timing = new Dictionary<string, List<GameObject>>()
    {
        { "perfect",new List<GameObject>() },
        { "great",new List<GameObject>() },
        { "nice",new List<GameObject>() },
        { "bad",new List<GameObject>() },
        { "DestoryPoint",new List<GameObject>() }
    };
    // Start is called before the first frame update
    void Awake()
    {
        tapLight = gameObject.transform.Find("NodeLight").GetComponent<TapLight>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Timing[nice].Count" + Timing["nice"].Count);
        Debug.Log("Timing[perfect].Count" + Timing["perfect"].Count);
        Debug.Log("Timing[great].Count" + Timing["great"].Count);
        Debug.Log("Timing[bad].Count" + Timing["bad"].Count);
        Debug.Log("Timing[DestoryPoint].Count" + Timing["DestoryPoint"].Count);
        if ( (settings._preview && !settings._randomTiming && Timing["perfect"].Count != 0) ||
            ( (settings._randomTiming && settings._preview) && ( (Timing["nice"].Count != 0 && Random.Range(0, 100) >= 70) || (Timing["perfect"].Count != 0 && Random.Range(0, 100) >= 80) || (Timing["great"].Count != 0 && Random.Range(0, 100) >= 75) || (Timing["bad"].Count != 0 && Random.Range(0, 100) >= 99) ) ) )
        {
            Debug.Log("in tap");
            Tap();
        }
        if (Timing["DestoryPoint"].Count != 0)
        {
            Debug.Log("DestoryPoint !=0");
            GameObject obj = Timing["DestoryPoint"][0];
            Destroy(obj);
            Timing["perfect"].Remove(obj);
            Timing["great"].Remove(obj);
            Timing["nice"].Remove(obj);
            Timing["bad"].Remove(obj);
            Timing["DestoryPoint"].Remove(obj);
            settings.Comment(0);
        }
        Test();
    }
    private void Test()
    {
        perfectCheck = Timing["perfect"].Count;
        greatCheck = Timing["great"].Count;
        niceCheck = Timing["nice"].Count;
        badCheck = Timing["bad"].Count;
    }

    public void Tap()
    {
        Debug.Log("Tap");
        if (tapLight.isActiveAndEnabled)
        {
            tapLight.Tap();
            Debug.Log("tapLight.isActiveAndEnabled=true");
        }
        else
        {
            tapLight.enabled = true;
            Debug.Log("tapLight.enabled = true;");
        }

        if (Timing["perfect"].Count != 0)
        {
            notesDelete("perfect");
        }
        else if(Timing["great"].Count != 0)
        {
            notesDelete("great");
        }
        else if (Timing["nice"].Count != 0)
        {
            notesDelete("nice");
        }
        else if (Timing["bad"].Count != 0 || Timing["DesotryPoint"].Count != 0)
        {
            notesDelete("bad");
            /*if(Timing["DesotryPoint"].Count != 0)
            {
                Destroy(Timing["DesotryPoint"][0]);
                Timing["DesotryPoint"].Remove(Timing["DesotryPoint"][0]);
            }*/
        }
    }

    private void notesDelete(string key)
    {
        settings.Comment(settings.toTimingID(key));
        try
        {
            // Destroy(Timing[key][0]);
            //Timing[key].Remove(Timing[key][0]);
            Destroy(Timing[key][0]);
            GameObject obj = Timing[key][0];
            Timing["perfect"].Remove(obj);
            Timing["great"].Remove(obj);
            Timing["nice"].Remove(obj);
            Timing["bad"].Remove(obj);
            Timing["DestoryPoint"].Remove(obj);
        }
        catch (System.NullReferenceException e)
        {
            Debug.LogError("Null Error" + e);
        }
    }
}
