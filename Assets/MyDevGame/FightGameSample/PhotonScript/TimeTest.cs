using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SendTime();
    }

    private static void SendTime()
    {
        var currentTime = DateTime.Now;

        string senderTimeStr = $"{currentTime}:{currentTime.Millisecond}";
        Debug.Log(senderTimeStr);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SendTime();
        }
    }
}
