using UnityEngine;
using UnityEngine.UI;
using System;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using TMPro;

public class DayCount : MonoBehaviour
{
    //public Button Button;
    public int count = 0;
    public TextMeshProUGUI dateText;

    void Start()
    {
        //Button.onClick.AddListener(CountUp);
    }

    public void CountUp()
    {
        count++;
        dateText.text = count.ToString(); 
    }
}
