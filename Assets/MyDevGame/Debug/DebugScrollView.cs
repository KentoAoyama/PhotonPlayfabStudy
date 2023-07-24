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

    [SerializeField]
    private Button _resetButton;

    void Awake()
    {
        Application.logMessageReceived += LoggedCb;

        if (_resetButton != null)
        {
            _resetButton.onClick.AddListener(ResetLog);
        }
    }

    public void LoggedCb(string logstr, string stacktrace, LogType type)
    {
        _debugText.text += logstr;
        _debugText.text += "\n\n";

        _debugWindow.verticalNormalizedPosition = 0;
    }

    private void ResetLog()
    {
        _debugText.text = string.Empty;
    }
}
