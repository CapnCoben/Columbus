using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class DayNightCycle : MonoBehaviour
{
    [Header("Calendar References")]
    private float clockTime;
    public string Clock;
    public TextMeshProUGUI ClockText;
    public string Calendar;
    public TextMeshProUGUI CalendarText;
    private DateTime thisDate = new DateTime(1492, 10, 12);
    private float timeMultiplier = 1;
    [Header("Sun Light")]
    [SerializeField]
    [Range(0, 1)]
    public float currentTime = 0;
    [SerializeField]
    private Light sun;
    private float intensity; //brightness of the sun light
    [Header("Clock")]
    [Range(1, 23)]
    public float timeH;
    [Range(0, 59)]
    public float timeM;
    [Range(0, 19)]
    private float timeS;

    private float roundedTime;

    private string dayPhase;
    private void Start()
    {
        roundedTime = Mathf.Round(clockTime * 1) / 10;
        timeM = Mathf.Round(roundedTime / 60);
        timeS = roundedTime % 60;


    }

    private void Update()
    {
        currentTime += (Time.deltaTime / 450) * timeMultiplier;
        if(currentTime >=24)
        {
            currentTime = 0;
        }
        Console.WriteLine(thisDate);
        Calendar = thisDate.ToLongDateString();
        clockTime += Time.deltaTime;
        CalendarText.text = Calendar;
        Clock = timeH.ToString("00") + ":" + timeM.ToString("00") + dayPhase;
        ClockText.text = Clock;
        ClockCount();
        AdjustSunRot();
        SunIntensity();
        Debug.Log(Time.realtimeSinceStartup);

    }

    private void ClockCount()
    {
        timeS++;
        if(timeS>19)
        {
            timeS = 0;
            timeM++;
            if(timeM> 59)
            {
                timeM = 0;
                timeH++;
            }
            if(timeH > 23)
            {
                timeH = 0;
                thisDate.AddDays(1);
            }
            if (timeH >= 12)
            {
                dayPhase = " PM";
            }
            else
            {
                dayPhase = " AM";
            }
            return;
        }
 
    }

    private void AdjustSunRot() // rotate sun daily
    {
        sun.transform.localRotation = Quaternion.Euler((currentTime * 360)-90, -170, 0f);
    }

   private void SunIntensity()
    {


        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
    }
}

