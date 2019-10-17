using UnityEngine;
using System.Collections;

public class CommentMesh : MonoBehaviour
{
    private string nowText;
    private string newText;
    private float time = 0;
    private float destoryTime = 4f;
    private TextMesh textMesh;

    private void Awake()
    {
        textMesh = GetComponent<TextMesh>();
    }

    private void Update()
    {
        newText = textMesh.text;
        if (nowText == newText)
        {
            time += Time.deltaTime;
            textMesh.characterSize = 1.4f + (time / 1.3f);
            if (time >= 0.1)
            {
                var alfa = textMesh.color.a - destoryTime * Time.deltaTime;
                textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, alfa);
                textMesh.gameObject.SetActive(!System.Convert.ToBoolean((int)(alfa * 1000) >> -1));
            }
        }
        else
        {
            OnEnable();
        }
    }

    private void OnEnable()
    {

        time = 0f;
        textMesh.characterSize = 1.4f;
        nowText = newText;
        textMesh.color = new Color(textMesh.color.r, textMesh.color.g, textMesh.color.b, 1f);
    }

}

