using System.Collections;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool _preview { get { return preview; } protected set { preview = value; } }
    public bool _randomTiming { get { return randomTiming; } }

    public TextMesh comment;

    [HideInInspector]
    private int Count = 0;
    [SerializeField]
    private bool preview;

    [SerializeField]
    private bool randomTiming;

    void Awake()
    {
        comment.text = "";
        _preview = preview;
    }



    public int toTimingID(string key)
    {
        var ret = new System.Collections.Generic.Dictionary<string, int>()
        {
            {"perfect", 1 },
            {"great", 2 },
            {"nice", 3 },
            {"bad", 0 }
        };
        return ret[key];
    }

    public void Comment(int com)
    {
        switch (com)
        {
            case 1:
                Count++;
                comment.text = "<color=\"#FEFB58\">Perfect!!</color>";
                comment.gameObject.SetActive(true);
                break;
            case 2:
                Count++;
                comment.text = "<color=\"#5BFE6F\">Great!!</color>";
                comment.gameObject.SetActive(true);
                break;
            case 3:
                Count++;
                comment.text = "Nice!!" ;
                comment.gameObject.SetActive(true);
                break;
            case 0 :
                Count++;
                comment.text = "Bad!!" ;
                comment.gameObject.SetActive(true);
                break;
            default:
                Count = 0;
                comment.text = "<color=\"red\">Miss</color>" ;
                comment.gameObject.SetActive(true);
                break;
        }
    }
}
