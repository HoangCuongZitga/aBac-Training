using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class TimeRemaing : MonoBehaviour
{
    [SerializeField] private int dateTimeOverString;

    private Text text;
    private DateTime timeRemaining;

    // properties for counting per second
    private int interval = 1;
    private float nextTime = 0;

    void Start()
    {
        // determined the end of Date
        DateTime theNextDay = DateTime.Today.AddDays(1);
        timeRemaining = theNextDay.AddHours(-DateTime.Now.Hour);
        text = this.transform.GetComponent<Text>();
    }

    void Update()
    {
        if (Time.time >= nextTime)
        {
            //update date countdown per second
            text.text = CountDownTime();

            nextTime += interval;
        }
    }


    string CountDownTime()
    {
        timeRemaining = timeRemaining.AddSeconds(-1);
        return timeRemaining.ToString("HH:mm:ss");
    }
}