using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    int money = 0;
    Text text_money; 

    private void Awake()
    {
        text_money = GetComponent<Text>(); 
    }
    public void AddPoint()
    {
        money += 1;
        UpdateTextUi();
    }
    public void UpdateTextUi()
    {
        text_money.text = money.ToString();
    }
}
