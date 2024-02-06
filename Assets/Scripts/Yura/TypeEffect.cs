using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TypeEffect : MonoBehaviour
{
    public int CharperSeconds;
    public string targetMsg;
    public GameObject EndCursor;
    TextMeshProUGUI msgText;
    int index;
    float interval;



    private void Awake()
    {
        msgText = GetComponent<TextMeshProUGUI>();
    }

    public void SetMsg(string msg)
    {
        targetMsg = msg;
        EffectStart();

    }


    void EffectStart()
    {
        msgText.text = "";
        index = 0;
        EndCursor.SetActive(false);

        interval = 0.5f / CharperSeconds;
        Invoke("Effecting", interval);
    }
    void Effecting()
    {
        if (msgText.text == targetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += targetMsg[index];
        index++;
        Invoke("Effecting", interval);
    }
    void EffectEnd()
    {
        EndCursor.SetActive(true);
    }
}