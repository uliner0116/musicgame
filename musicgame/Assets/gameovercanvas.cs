using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class gameovercanvas : MonoBehaviour
{

    // Use this for initialization
    public Text finalscore;
    public Text finalcombo;
    public int score;
    public int combo;
    void Start()
    {
        score = GameObject.FindObjectOfType<Game.SceneController>().Score;
        finalscore.text = string.Format("Score: {0}", score);
        combo = GameObject.FindObjectOfType<Game.SceneController>().Combo;
        finalcombo.text = string.Format("Combo: {0}", combo);
    }

    // Update is called once per frame
    void Update()
    {

    }
}