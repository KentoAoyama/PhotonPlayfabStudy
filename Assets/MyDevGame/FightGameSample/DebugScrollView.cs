using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScrollView : MonoBehaviour
{
    [SerializeField] 
    private Text _debugText;

    [SerializeField] 
    private ScrollRect _debugWindow;

    void Awake()
    {
        Application.logMessageReceived += LoggedCb;
    }


    public void LoggedCb(string logstr, string stacktrace, LogType type)
    {
        _debugText.text += logstr;
        _debugText.text += "\n\n";

        _debugWindow.verticalNormalizedPosition = 0;
    }
}
